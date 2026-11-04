using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using Gdi=System.Drawing;
using Color=Microsoft.Xna.Framework.Color;
using Rectangle=Microsoft.Xna.Framework.Rectangle;
using Keys=Microsoft.Xna.Framework.Input.Keys;
using ButtonState=Microsoft.Xna.Framework.Input.ButtonState;

bool smoke=args.Contains("--smoke-all");
Console.WriteLine("Browser arguments: "+string.Join(" | ",args));
using var mutex=new Mutex(true,@"Local\IndieGames.ShaderDemoBrowser",out bool owner);
if(!owner&&!smoke)return;
using var browser=new Browser(args);browser.Run();

sealed class Browser:Game {
 const int Side=310;
 readonly GraphicsDeviceManager graphics;readonly string[] arguments;
 SpriteBatch batch=null!;FontAtlas font=null!;Texture2D white=null!,checker=null!,glow=null!,animation=null!;BasicEffect basic=null!;
 RenderTarget2D scene=null!,preview=null!; readonly List<Entry> entries=[];List<Entry> shown=[];int selected,scroll,technique;
 KeyboardState oldKeys;MouseState oldMouse;float clock,strength=.7f;bool paused;string query="",error="";
 int smokeFrame,smokeIndex,uiFrames;readonly List<object> smokeResults=[];string? smokeFolder;
 Entry Current=>shown[Math.Clamp(selected,0,shown.Count-1)];
 sealed record Entry(string Name,string Source,byte[] Code){public Effect? Effect;public VertexDeclaration? Declaration;public string Group=>Name.StartsWith("DeadKings.")?"Dead.Kings":Name.StartsWith("AdamantAnts.")||Name.StartsWith("Bloom")||Name=="GaussianBlur"?"Adamant Ants":"Shared repairs";public bool Mesh=>Source.Contains("VertexShader = compile");}
 public Browser(string[] args){arguments=args;graphics=new GraphicsDeviceManager(this){PreferredBackBufferWidth=1380,PreferredBackBufferHeight=900,GraphicsProfile=GraphicsProfile.HiDef};Window.Title="Shader Demo Browser";Window.AllowUserResizing=true;IsMouseVisible=true;IsFixedTimeStep=false;}
 byte[] Resource(string name){using var stream=Assembly.GetExecutingAssembly().GetManifestResourceStream(name)??throw new Exception("Missing shader resource "+name);using var memory=new MemoryStream();stream.CopyTo(memory);return memory.ToArray();}
 protected override void LoadContent(){
  Console.WriteLine("Initializing shader demo content");
  batch=new SpriteBatch(GraphicsDevice);font=new FontAtlas(GraphicsDevice);white=new Texture2D(GraphicsDevice,1,1);white.SetData(new[]{Color.White});
  checker=new Texture2D(GraphicsDevice,128,128);glow=new Texture2D(GraphicsDevice,128,128);var cells=new Color[128*128];var lights=new Color[cells.Length];
  for(int y=0;y<128;y++)for(int x=0;x<128;x++){bool on=((x/16+y/16)&1)==0;cells[y*128+x]=on?new Color(55+x,125+y/2,210):new Color(18,40+x/2,65+y/2);float a=MathF.Pow(MathF.Max(0,1-Vector2.Distance(new Vector2(x,y),new Vector2(64))/64),2);lights[y*128+x]=new Color(1f,1f,1f,a);}
  checker.SetData(cells);glow.SetData(lights);basic=new BasicEffect(GraphicsDevice){TextureEnabled=true,Texture=checker,VertexColorEnabled=true};
  animation=new Texture2D(GraphicsDevice,128,128,false,SurfaceFormat.Vector4);var poses=Enumerable.Range(0,128*128).Select(i=>((i%128)%3) switch{0=>Vector4.UnitX,1=>Vector4.UnitY,_=>Vector4.UnitZ}).ToArray();animation.SetData(poses);
  foreach(string file in Directory.GetFiles(Path.Combine(AppContext.BaseDirectory,"Effects"),"*.fxb").Order()){
   string name=Path.GetFileNameWithoutExtension(file);entries.Add(new Entry(name,System.Text.Encoding.UTF8.GetString(Resource("ShaderSources."+name+".fx")),File.ReadAllBytes(file)));
  }
  shown=entries.ToList();if(entries.Count!=43)throw new Exception("Expected 43 supported effects, found "+entries.Count);
  int only=Array.IndexOf(arguments,"--only");if(only>=0&&only+1<arguments.Length)shown=entries.Where(e=>e.Name==arguments[only+1]).ToList();
  int index=Array.IndexOf(arguments,"--effect");if(index>=0&&index+1<arguments.Length){int found=shown.FindIndex(e=>e.Name==arguments[index+1]);if(found>=0)selected=found;}
  index=Array.IndexOf(arguments,"--smoke-all");if(index>=0){smokeFolder=index+1<arguments.Length?arguments[index+1]:Path.Combine(AppContext.BaseDirectory,"demo-smoke");Directory.CreateDirectory(smokeFolder);graphics.SynchronizeWithVerticalRetrace=false;}
 }
 void Select(int index){if(shown.Count==0)return;Current.Effect?.Dispose();Current.Effect=null;selected=Math.Clamp(index,0,shown.Count-1);technique=0;error="";}
 bool Press(Keys key,KeyboardState state)=>state.IsKeyDown(key)&&oldKeys.IsKeyUp(key);
 protected override void Update(GameTime time){
  if(!paused)clock+=(float)time.ElapsedGameTime.TotalSeconds;
  if(smokeFolder!=null)return;
  var keys=Keyboard.GetState();var mouse=Mouse.GetState();bool click=mouse.LeftButton==ButtonState.Pressed&&oldMouse.LeftButton==ButtonState.Released;
  if(Press(Keys.Escape,keys)){if(query.Length>0){query="";Filter();}else Exit();}
  if(Press(Keys.Down,keys)||Press(Keys.Right,keys))Select(selected+1);if(Press(Keys.Up,keys)||Press(Keys.Left,keys))Select(selected-1);
  if(Press(Keys.Space,keys))paused=!paused;if(Press(Keys.Tab,keys)){technique++;error="";}
  if(smokeFolder==null){
   foreach(var key in keys.GetPressedKeys().Where(k=>oldKeys.IsKeyUp(k))){if(key>=Keys.A&&key<=Keys.Z){query+=(char)('a'+(int)key-(int)Keys.A);Filter();}else if(key==Keys.Back&&query.Length>0){query=query[..^1];Filter();}}
   if(mouse.X<Side){scroll=Math.Clamp(scroll-(mouse.ScrollWheelValue-oldMouse.ScrollWheelValue)/120,0,Math.Max(0,shown.Count-VisibleRows));if(click&&mouse.Y>=112&&mouse.Y<112+VisibleRows*28){int item=scroll+(mouse.Y-112)/28;if(item<shown.Count)Select(item);}}
   if(click&&mouse.X>Side+20&&mouse.X<Side+130&&mouse.Y>65&&mouse.Y<100)paused=!paused;
   if(click&&mouse.X>Side+140&&mouse.X<Side+300&&mouse.Y>65&&mouse.Y<100){technique++;error="";}
   if(mouse.LeftButton==ButtonState.Pressed&&mouse.Y>Height-63&&mouse.Y<Height-31&&mouse.X>Side+150)strength=Math.Clamp((mouse.X-Side-160f)/Math.Max(1,Width-Side-220),0,1);
  }
  oldKeys=keys;oldMouse=mouse;
 }
 int Width=>GraphicsDevice.PresentationParameters.BackBufferWidth;int Height=>GraphicsDevice.PresentationParameters.BackBufferHeight;int VisibleRows=>Math.Max(1,(Height-155)/28);
 void Filter(){shown=entries.Where(e=>(e.Name+" "+e.Group).Contains(query,StringComparison.OrdinalIgnoreCase)).ToList();selected=0;scroll=0;error="";}
 void Targets(){int w=Math.Max(64,Width-Side-40),h=Math.Max(64,Height-285);if(preview?.Width==w&&preview.Height==h)return;scene?.Dispose();preview?.Dispose();scene=new RenderTarget2D(GraphicsDevice,w,h,false,SurfaceFormat.Color,DepthFormat.Depth24);preview=new RenderTarget2D(GraphicsDevice,w,h,false,SurfaceFormat.Color,DepthFormat.Depth24);}
 void Scene(){
  GraphicsDevice.SetRenderTarget(scene);GraphicsDevice.Clear(new Color(17,28,46));batch.Begin(SpriteSortMode.Deferred,BlendState.NonPremultiplied,SamplerState.LinearWrap,DepthStencilState.None,RasterizerState.CullNone,null);
  batch.Draw(checker,new Rectangle(0,0,scene.Width,scene.Height),new Rectangle(0,0,scene.Width/2,scene.Height/2),Color.White);
  for(int i=0;i<8;i++){float angle=clock*.45f+i*MathHelper.TwoPi/8;int x=(int)(scene.Width*(.5+.32*Math.Sin(angle))),y=(int)(scene.Height*(.5+.3*Math.Cos(angle*1.2)));batch.Draw(glow,new Rectangle(x-75,y-75,150,150),i%2==0?new Color(255,170,70):new Color(80,220,255));}
  font.Draw(batch,"LIVE SHADER PREVIEW",new Vector2(scene.Width*.2f,scene.Height*.35f),Color.White,1.7f);font.Draw(batch,"Synthetic test scene - original game assets are not required",new Vector2(24,scene.Height-40),Color.White);batch.End();
 }
 protected override void Draw(GameTime time){
  Targets();Scene();
  if(shown.Count>0){var entry=Current;try{
   entry.Effect??=new Effect(GraphicsDevice,entry.Code);var fx=entry.Effect;fx.CurrentTechnique=fx.Techniques[technique%fx.Techniques.Count];
   GraphicsDevice.SetRenderTarget(preview);GraphicsDevice.Clear(entry.Mesh?new Color(75,95,120):new Color(12,20,35));SetParameters(fx,entry);
   if(entry.Mesh)DrawMesh(fx,entry);else{GraphicsDevice.Textures[1]=scene;GraphicsDevice.SamplerStates[1]=SamplerState.LinearClamp;batch.Begin(SpriteSortMode.Immediate,BlendState.Opaque,SamplerState.LinearClamp,DepthStencilState.None,RasterizerState.CullNone,fx);batch.Draw(scene,new Rectangle(0,0,preview.Width,preview.Height),Color.White);batch.End();}
   error="";
  }catch(Exception e){error=e.Message;try{batch.End();}catch{}GraphicsDevice.SetRenderTarget(preview);GraphicsDevice.Clear(new Color(70,25,35));}}
  GraphicsDevice.SetRenderTarget(null);GraphicsDevice.Clear(new Color(15,23,39));Ui();
  int capture=Array.IndexOf(arguments,"--capture");
  if(capture>=0&&capture+1<arguments.Length&&++uiFrames>=3){var pixels=new Color[Width*Height];GraphicsDevice.GetBackBufferData(pixels);using var image=new Texture2D(GraphicsDevice,Width,Height);image.SetData(pixels);using var file=File.Create(arguments[capture+1]);image.SaveAsPng(file,Width,Height);Exit();}
  if(smokeFolder!=null&&++smokeFrame>=3){
   var pixels=new Color[preview.Width*preview.Height];preview.GetData(pixels);int unique=pixels.Where((_,i)=>i%17==0).Select(c=>c.PackedValue).Distinct().Count();
   using(var file=File.Create(Path.Combine(smokeFolder,Current.Name+".png")))preview.SaveAsPng(file,preview.Width,preview.Height);
   smokeResults.Add(new{effect=Current.Name,error,uniqueColors=unique,visible=unique>1});Console.WriteLine($"DEMO {Current.Name}: {unique} colors {error}");
   if(++smokeIndex>=shown.Count){File.WriteAllText(Path.Combine(smokeFolder,"results.json"),JsonSerializer.Serialize(smokeResults,new JsonSerializerOptions{WriteIndented=true}));Exit();}else{Select(smokeIndex);smokeFrame=0;}
  }
 }
 void Ui(){
  batch.Begin(SpriteSortMode.Deferred,BlendState.NonPremultiplied,SamplerState.LinearClamp,DepthStencilState.None,RasterizerState.CullNone,null);Box(new Rectangle(0,0,Side,Height),new Color(24,34,52));
  font.Draw(batch,"Shader Demo Browser",new Vector2(18,17),Color.White,1.3f);font.Draw(batch,$"{entries.Count} supported effects",new Vector2(18,49),new Color(153,178,206));
  Box(new Rectangle(14,78,Side-28,28),new Color(34,47,67));font.Draw(batch,query.Length==0?"Type to filter...":query,new Vector2(22,81),query.Length==0?new Color(150,166,186):Color.White);
  for(int row=0;row<VisibleRows&&row+scroll<shown.Count;row++){int i=row+scroll;int y=112+row*28;if(i==selected)Box(new Rectangle(8,y,Side-16,27),new Color(37,99,235));font.Draw(batch,Trim(shown[i].Name,31),new Vector2(17,y+3),Color.White);}
  font.Draw(batch,"Arrows: choose   Tab: technique",new Vector2(16,Height-31),new Color(153,178,206),.85f);
  if(shown.Count>0){var e=Current;int left=Side+20;
   font.Draw(batch,Trim(e.Name,65),new Vector2(left,18),Color.White,1.25f);font.Draw(batch,e.Group+" | "+(e.Mesh?"Mesh / particle scene":"Image post-process"),new Vector2(left,47),new Color(153,178,206));
   Box(new Rectangle(left,70,110,30),new Color(37,99,235));font.Draw(batch,paused?"Resume":"Pause",new Vector2(left+15,74),Color.White);
   Box(new Rectangle(left+120,70,160,30),new Color(55,65,81));font.Draw(batch,"Next technique",new Vector2(left+130,74),Color.White);
   batch.Draw(preview,new Rectangle(left,112,preview.Width,preview.Height),Color.White);
   int y=Height-160;string status=error.Length>0?"Preview error: "+error:(e.Effect?.CurrentTechnique.Name??"Loading")+" | "+(e.Effect?.Parameters.Count??0)+" parameters";
   font.Draw(batch,Trim(status,100),new Vector2(left,y),error.Length>0?new Color(255,155,155):new Color(153,213,180));
   font.Draw(batch,"Synthetic demo; game-specific textures and models can change the appearance.",new Vector2(left,y+28),new Color(153,178,206),.9f);
   if(e.Source.Contains("standard interpolation"))font.Draw(batch,"FNA compatibility: standard interpolation replaces centroid at MSAA edges.",new Vector2(left,y+51),new Color(255,190,100),.85f);
   font.Draw(batch,"Demo intensity",new Vector2(left,Height-58),Color.White);int x=left+140,w=Math.Max(10,Width-x-40);Box(new Rectangle(x,Height-52,w,6),new Color(55,65,81));Box(new Rectangle(x,Height-52,(int)(w*strength),6),new Color(96,165,250));
  }else font.Draw(batch,"No matching effects. Backspace or Escape clears the filter.",new Vector2(Side+30,160),Color.White);
  batch.End();
 }
 string Trim(string text,int n)=>text.Length>n?text[..(n-3)]+"...":text;
 void Box(Rectangle area,Color color)=>batch.Draw(white,area,color);
 void SetParameters(Effect fx,Entry entry){
  float aspect=preview.Width/(float)preview.Height;var world=Matrix.CreateRotationY(clock*.35f)*Matrix.CreateRotationX(.15f);var view=Matrix.CreateLookAt(new Vector3(0,1.2f,4),Vector3.Zero,Vector3.Up);var projection=Matrix.CreatePerspectiveFieldOfView(.8f,aspect,.1f,100);
  foreach(var p in fx.Parameters){string n=p.Name.ToLowerInvariant();try{
   if(p.ParameterType==EffectParameterType.Texture){p.SetValue(n.Contains("animation")?animation:n.Contains("shadow")?white:n.Contains("reflection")?scene:checker);continue;}
   if(p.ParameterClass==EffectParameterClass.Matrix){Matrix value=n.Contains("projection")&&!n.Contains("view")?projection:n=="view"||n=="xview"||n=="reflectionview"?view:n=="viewproj"||n.Contains("worldviewproj")||n=="wvp"?world*view*projection:n.Contains("inverse")?Matrix.Transpose(Matrix.Invert(world)):world;if(p.Elements.Count>0)p.SetValue(Enumerable.Repeat(Matrix.Identity,p.Elements.Count).ToArray());else p.SetValue(value);continue;}
   if(p.Elements.Count>0){if(n=="sampleweights")p.SetValue(Enumerable.Repeat(1f/p.Elements.Count,p.Elements.Count).ToArray());else if(n=="sampleoffsets")p.SetValue(Enumerable.Range(0,p.Elements.Count).Select(i=>new Vector2((i-p.Elements.Count/2)*2f/preview.Width,0)).ToArray());else if(n=="instancedata"){var data=new Vector4[p.Elements.Count];data[0]=new Vector4(-1.2f,0,2.4f,.25f*MathF.Sin(clock));p.SetValue(data);}continue;}
   if(p.ParameterType!=EffectParameterType.Single)continue;
   if(p.ColumnCount==1){float? value=n is "time" or "ftimer" or "timeraslow" or "currenttime"?clock:n.Contains("saturation")||n.Contains("intensity")||n=="xlightstrength"||n=="brite"?1f:n is "fade" or "fader" or "trans" or "intense"?strength:n=="bloomthreshold"?.4f:n=="waterline"?.45f:n=="duration"?3f:n=="endvelocity"?1f:n=="xambient"?.15f:n=="xconeangle"?.8f:n=="xconedecay"?4f:n=="lineradius"?.08f:n=="blurthreshold"?.6f:null;if(value.HasValue)p.SetValue(value.Value);}
   else if(p.ColumnCount==2){Vector2? v=n=="screensresolution"||n=="screenresolution"?new Vector2(preview.Width,preview.Height):n=="viewportscale"?new Vector2(1/aspect,1):n is "startsize" or "endsize"?new Vector2(.15f,.3f):n=="rotatespeed"?new Vector2(.3f,1):null;if(v.HasValue)p.SetValue(v.Value);}
   else if(p.ColumnCount==3){Vector3? v=n.Contains("lightdirection")||n=="xconedirection"?Vector3.Normalize(new Vector3(-.5f,-1,-.5f)):n=="xlightposition"?new Vector3(1,3,1):n.Contains("camera")||n.Contains("eyeposition")?new Vector3(0,1.2f,4):n.Contains("ambient")?new Vector3(.25f):n.Contains("diffuse")||n.Contains("lightcolor")?new Vector3(.8f):n.Contains("emissive")?new Vector3(.08f):n=="gravity"?new Vector3(0,.03f,0):null;if(v.HasValue)p.SetValue(v.Value);}
   else if(p.ColumnCount==4){Vector4? v=n.StartsWith("uv")?new Vector4(1,0,1,0):n=="diff"?new Vector4(.8f):n=="amb"?new Vector4(.2f):n.Contains("color")?new Vector4(.3f,.8f,1,1):null;if(v.HasValue)p.SetValue(v.Value);}
  }catch(Exception e){throw new Exception(p.Name+": "+e.Message);}}
  if(entry.Name=="RoundLine"){fx.Parameters["viewProj"].SetValue(Matrix.CreateOrthographicOffCenter(-2,2,-1.5f,1.5f,0,1));fx.Parameters["lineColor"].SetValue(new Vector4(.2f,.8f,1,1));}
  if(entry.Name=="DeadKings.InstancerStain"){fx.Parameters["Diff"].SetValue(new Vector4(.8f,.8f,.8f,-10));fx.Parameters["Amb"].SetValue(new Vector4(.2f,.2f,.2f,10));}
  if(entry.Name=="DeadKings.InstEffect"){fx.Parameters["BoneDelta"].SetValue(1/128f);fx.Parameters["RowDelta"].SetValue(1/128f);}
 }
 void DrawMesh(Effect fx,Entry entry){
  GraphicsDevice.BlendState=BlendState.Opaque;GraphicsDevice.DepthStencilState=DepthStencilState.Default;GraphicsDevice.RasterizerState=RasterizerState.CullNone;
  for(int i=0;i<16;i++){GraphicsDevice.Textures[i]=checker;GraphicsDevice.SamplerStates[i]=SamplerState.LinearWrap;}
  var vertices=new List<DemoVertex>();
  if(entry.Name=="RoundLine"){
   DemoVertex V(float end,float side){var v=DemoVertex.Make(new Vector3(0,side,0),Vector3.UnitZ,new Vector2(end,side));v.Normal=new Vector4(side,0,0,0);v.Tex0=new Vector4(1,end,0,0);v.Tex1=Vector4.Zero;return v;}
   vertices.AddRange(new[]{V(0,-1),V(1,-1),V(1,1),V(0,-1),V(1,1),V(0,1)});
  }else if(entry.Name.Contains("ParticleEffect")){
   for(int i=0;i<40;i++){float angle=i*2.4f;Vector3 position=new(MathF.Cos(angle)*(.2f+i*.025f),MathF.Sin(angle)*.7f,0);DemoVertex V(float x,float y){var v=DemoVertex.Make(new Vector3(x,y,0),new Vector3(.02f,.02f,0),Vector2.Zero);v.Position1=new Vector4(position,1);v.Color0=new Vector4(.2f,.4f,.6f,.8f);v.Tex0=new Vector4(clock-.3f-i%8*.15f,0,0,0);return v;}vertices.AddRange(new[]{V(-1,-1),V(1,-1),V(1,1),V(-1,-1),V(1,1),V(-1,1)});}
  }else{
   const int rings=16,slices=28;
   DemoVertex V(int r,int s){float lat=r*MathHelper.Pi/rings,lon=s*MathHelper.TwoPi/slices;var normal=new Vector3(MathF.Sin(lat)*MathF.Cos(lon),MathF.Cos(lat),MathF.Sin(lat)*MathF.Sin(lon));return DemoVertex.Make(normal,normal,new Vector2(s/(float)slices,r/(float)rings));}
   for(int r=0;r<rings;r++)for(int s=0;s<slices;s++)vertices.AddRange(new[]{V(r,s),V(r+1,s),V(r+1,s+1),V(r,s),V(r+1,s+1),V(r,s+1)});
  }
  entry.Declaration??=Declaration(entry);
  foreach(var pass in fx.CurrentTechnique.Passes){pass.Apply();GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,vertices.ToArray(),0,vertices.Count/3,entry.Declaration);}
 }
 VertexDeclaration Declaration(Entry entry){
  var semantics=new HashSet<string>(StringComparer.OrdinalIgnoreCase);
  foreach(Match shader in Regex.Matches(entry.Source,@"VertexShader\s*=\s*compile\s+vs_\w+\s+(\w+)\(")){
   var function=Regex.Match(entry.Source,@"\b"+shader.Groups[1].Value+@"\s*\(\s*(\w+)\s+\w+\s*(?::\s*(\w+))?\s*\)");
   if(function.Groups[2].Success)semantics.Add(function.Groups[2].Value);
   else{var structure=Regex.Match(entry.Source,@"struct\s+"+function.Groups[1].Value+@"\s*\{([^}]+)\}",RegexOptions.Singleline);foreach(Match m in Regex.Matches(structure.Groups[1].Value,@":\s*(\w+)"))semantics.Add(m.Groups[1].Value);}
  }
  string Semantic(VertexElement e)=>(e.VertexElementUsage switch{VertexElementUsage.TextureCoordinate=>"TEXCOORD",VertexElementUsage.BlendIndices=>"BLENDINDICES",VertexElementUsage.BlendWeight=>"BLENDWEIGHT",var u=>u.ToString().ToUpperInvariant()})+(e.UsageIndex==0?"":e.UsageIndex.ToString());
  var elements=DemoVertex.Declaration.GetVertexElements().Where(e=>semantics.Contains(Semantic(e))||semantics.Contains(Semantic(e)+"0")).ToArray();
  if(elements.Length==0)throw new Exception("Could not identify vertex inputs for this demo");
  return new VertexDeclaration(Marshal.SizeOf<DemoVertex>(),elements);
 }
 protected override void UnloadContent(){foreach(var e in entries)e.Effect?.Dispose();scene?.Dispose();preview?.Dispose();font.Dispose();white.Dispose();checker.Dispose();glow.Dispose();animation.Dispose();batch.Dispose();basic.Dispose();}
}

[StructLayout(LayoutKind.Sequential)]
struct DemoVertex:IVertexType{
 public Vector4 Position,Position1,Normal,Color0,Color1,Tex0,Tex1,Tex2,Tex3,Blend0,Blend1,Blend2,Blend3,Blend4,Fog,Indices,Fog1,Fog2,Fog3,Fog4,Fog5;
 public static readonly VertexDeclaration Declaration=new(Enumerable.Range(0,21).Select(i=>new VertexElement(i*16,VertexElementFormat.Vector4,i switch{0 or 1=>VertexElementUsage.Position,2=>VertexElementUsage.Normal,3 or 4=>VertexElementUsage.Color,>=5 and <=8=>VertexElementUsage.TextureCoordinate,>=9 and <=13=>VertexElementUsage.BlendWeight,14 or >=16=>VertexElementUsage.Fog,_=>VertexElementUsage.BlendIndices},i switch{1=>1,4=>1,>=5 and <=8=>i-5,>=9 and <=13=>i-9,>=16=>i-15,_=>0})).ToArray());
 VertexDeclaration IVertexType.VertexDeclaration=>Declaration;
 public static DemoVertex Make(Vector3 position,Vector3 normal,Vector2 uv)=>new(){Position=new Vector4(position,1),Position1=new Vector4(position,1),Normal=new Vector4(normal,0),Color0=Vector4.One,Color1=Vector4.One,Tex0=new Vector4(uv,0,1),Tex1=new Vector4(uv,0,1),Blend0=Vector4.UnitX,Blend1=Vector4.UnitY,Blend2=Vector4.UnitZ,Blend3=Vector4.UnitW,Blend4=new Vector4(1,0,1,0),Fog=Vector4.One,Fog1=Vector4.One,Fog2=Vector4.One,Fog5=Vector4.One};
}

sealed class FontAtlas:IDisposable{
 readonly Texture2D texture;readonly float[] widths=new float[96];
 public FontAtlas(GraphicsDevice device){using var image=new Gdi.Bitmap(16*26,6*30,Gdi.Imaging.PixelFormat.Format32bppArgb);using var g=Gdi.Graphics.FromImage(image);g.Clear(Gdi.Color.Transparent);g.TextRenderingHint=Gdi.Text.TextRenderingHint.AntiAliasGridFit;using var font=new Gdi.Font("Segoe UI",14,Gdi.FontStyle.Regular,Gdi.GraphicsUnit.Pixel);using var format=Gdi.StringFormat.GenericTypographic;format.FormatFlags|=Gdi.StringFormatFlags.MeasureTrailingSpaces;for(int i=0;i<95;i++){string s=((char)(i+32)).ToString();g.DrawString(s,font,Gdi.Brushes.White,new Gdi.PointF(i%16*26+2,i/16*30+2),format);widths[i]=MathF.Max(1,g.MeasureString(s,font,26,format).Width);}using var stream=new MemoryStream();image.Save(stream,Gdi.Imaging.ImageFormat.Png);stream.Position=0;texture=Texture2D.FromStream(device,stream);}
 public void Draw(SpriteBatch batch,string text,Vector2 at,Color color,float scale=1){float x=at.X;foreach(char c in text){int i=c-32;if(i<0||i>=95)i='?'-32;batch.Draw(texture,new Vector2(MathF.Round(x),MathF.Round(at.Y)),new Rectangle(i%16*26+1,i/16*30+1,24,28),color,0,Vector2.Zero,scale,SpriteEffects.None,0);x+=widths[i]*scale;}}
 public void Dispose()=>texture.Dispose();
}
