using Maximinus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using var test = new ShaderTest(args[0], args[1]);
test.RunOneFrame();
if (!test.Passed) throw new Exception("Rendering test did not execute.");

sealed class ShaderTest : Game
{
    const int Width = 960, Height = 720;
    readonly string output;
    readonly GraphicsDeviceManager graphics;
    public bool Passed { get; private set; }

    public ShaderTest(string contentRoot, string output)
    {
        this.output = output;
        graphics = new GraphicsDeviceManager(this) {
            PreferredBackBufferWidth = Width, PreferredBackBufferHeight = Height,
            GraphicsProfile = GraphicsProfile.HiDef, SynchronizeWithVerticalRetrace = false
        };
        Content.RootDirectory = contentRoot;
        IsFixedTimeStep = false;
    }

    protected override void LoadContent()
    {
        var renderer = new RoundLineManager();
        renderer.Init(GraphicsDevice, Content);
        string[] techniques = { "Standard", "NoBlur", "Tubular", "Glow", "AnimatedLinear", "AnimatedRadial" };
        foreach (var name in techniques)
            if (!renderer.TechniqueNames.Contains(name)) throw new Exception("Missing technique: " + name);
        using var target = new RenderTarget2D(GraphicsDevice, Width, Height, false, SurfaceFormat.Color, DepthFormat.None);
        GraphicsDevice.SetRenderTarget(target);
        GraphicsDevice.Clear(new Color(12, 18, 26));
        GraphicsDevice.DepthStencilState = DepthStencilState.None;
        GraphicsDevice.RasterizerState = RasterizerState.CullNone;
        Matrix projection = Matrix.CreateOrthographicOffCenter(0, Width, Height, 0, 0, 1);
        for (int i = 0; i < techniques.Length; i++)
        {
            float y = 50 + i * 55;
            renderer.Draw(new RoundLine(70, y, 410, y), 18, new Color(70, 220, 255), projection, 0.75f, techniques[i]);
            renderer.Draw(new RoundLine(480, y - 10, 650, y + 10), 18, new Color(255, 170, 70), projection, 0.75f, techniques[i]);
            renderer.Draw(new RoundLine(740, y, 740, y), 18, new Color(160, 240, 120), projection, 0.75f, techniques[i]);
        }
        renderer.Draw(new RoundLine(70, 425, 350, 425), 16, new Color(255, 0, 0, 128), projection, 0, "Standard");
        renderer.Draw(new RoundLine(480, 425, 700, 425), 12, Color.White, projection, 0, "AnimatedLinear");
        renderer.Draw(new RoundLine(480, 465, 700, 465), 12, Color.White, projection, 1, "AnimatedLinear");
        renderer.Draw(new RoundLine(780, 405, 850, 480), 10, Color.White, projection, 0, "Standard");
        var batch = new List<RoundLine>();
        for (int i = 0; i < 401; i++)
        {
            float x = 20 + i % 40 * 22, y = 535 + i / 40 * 13;
            batch.Add(new RoundLine(x, y, x + 10, y));
        }
        int before = renderer.NumLinesDrawn;
        renderer.Draw(batch, 2, Color.Cyan, projection, 0, "Standard");
        if (renderer.NumLinesDrawn - before != 401) throw new Exception("Batch count mismatch.");
        GraphicsDevice.SetRenderTarget(null);
        var pixels = new Color[Width * Height];
        target.GetData(pixels);
        Color Pixel(int x, int y) => pixels[y * Width + x];
        void Require(bool condition, string message) { if (!condition) throw new Exception(message); }
        Require(Pixel(200, 50).G > 200, "Horizontal line center did not render.");
        Require(Pixel(55, 50).G > 150, "Rounded start cap did not render.");
        Require(Pixel(49, 50).G == 18, "Rounded cap extends outside its radius.");
        Require(Pixel(740, 50).G > 200, "Zero-length line did not render as a disc.");
        Require(Pixel(280, 425).R > 110 && Pixel(280, 425).R < 160, "Transparency is not correctly premultiplied.");
        Require(Math.Abs(Pixel(600, 425).R - Pixel(600, 465).R) > 30, "Animated technique ignores time.");
        foreach (int i in new[] { 0, 199, 200, 399, 400 })
            Require(Pixel(25 + i % 40 * 22, 535 + i / 40 * 13).G > 200, "Missing line across batch boundary: " + i);
        using var stream = File.Create(output);
        target.SaveAsPng(stream, Width, Height);
        Passed = true;
        Console.WriteLine("PASS: effect loading, six techniques, caps, discs, alpha, time animation, and 401-line batching.");
        if (File.Exists(Path.Combine(Content.RootDirectory, "Effects", "spotlight.xnb"))) TestSpotlight();
        if (File.Exists(Path.Combine(Content.RootDirectory, "Effects", "Blur.xnb"))) TestBlur();
        if (File.Exists(Path.Combine(Content.RootDirectory, "Effects", "ParticleEffect.xnb"))) TestParticles();
    }

    void TestSpotlight()
    {
        var effect = Content.Load<Effect>("Effects/spotlight");
        foreach (var name in new[] { "xWorld", "xView", "xProjection", "xAmbient", "xLightPosition", "xConeDirection", "xConeAngle", "xConeDecay", "xLightStrength" })
            if (effect.Parameters[name] == null) throw new Exception("Missing spotlight parameter: " + name);
        effect.CurrentTechnique = effect.Techniques["SpotLight"];
        using var target = new RenderTarget2D(GraphicsDevice, Width, Height, false, SurfaceFormat.Color, DepthFormat.None);
        var vertices = new[] {
            new VertexPositionNormalTexture(new Vector3(-120,0,90), Vector3.Up, Vector2.Zero),
            new VertexPositionNormalTexture(new Vector3(-120,0,-90), Vector3.Up, Vector2.Zero),
            new VertexPositionNormalTexture(new Vector3(120,0,90), Vector3.Up, Vector2.Zero),
            new VertexPositionNormalTexture(new Vector3(120,0,-90), Vector3.Up, Vector2.Zero)
        };
        Color[] Render(float ambient, float strength, Vector3 position, Vector3 direction, float angle=.6f, float decay=16, bool flipNormal=false)
        {
            GraphicsDevice.SetRenderTarget(target);
            GraphicsDevice.Clear(Color.Magenta);
            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            effect.Parameters["xView"].SetValue(Matrix.CreateLookAt(new Vector3(0,100,0), Vector3.Zero, Vector3.Forward));
            effect.Parameters["xProjection"].SetValue(Matrix.CreateOrthographicOffCenter(-120,120,-90,90,.1f,200));
            effect.Parameters["xAmbient"].SetValue(ambient);
            effect.Parameters["xLightPosition"].SetValue(position);
            effect.Parameters["xConeDirection"].SetValue(direction);
            effect.Parameters["xConeAngle"].SetValue(angle);
            effect.Parameters["xConeDecay"].SetValue(decay);
            effect.Parameters["xLightStrength"].SetValue(strength);
            for(int i=0;i<vertices.Length;i++) vertices[i].Normal=flipNormal?Vector3.Down:Vector3.Up;
            effect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip,vertices,0,2);
            GraphicsDevice.SetRenderTarget(null);
            var pixels=new Color[Width*Height];target.GetData(pixels);return pixels;
        }
        int Sample(Color[] pixels,int x,int y=360)=>pixels[y*Width+x].R;
        void Require(bool condition,string message){if(!condition)throw new Exception(message);}
        var baseline=Render(0,1,new Vector3(0,100,0),Vector3.Down);
        Require(Sample(baseline,480)>245 && Sample(baseline,900)<5,"Spotlight center or cone cutoff is incorrect.");
        var ambient=Render(.25f,0,new Vector3(0,100,0),Vector3.Down);
        Require(Math.Abs(Sample(ambient,900)-64)<3,"Spotlight ambient/strength controls are incorrect.");
        var moved=Render(0,1,new Vector3(40,100,0),Vector3.Down);
        Require(Sample(moved,640)>245 && Sample(moved,640)>Sample(moved,480)+80,"Spotlight position is ignored.");
        var away=Render(0,1,new Vector3(0,100,0),Vector3.Up);
        Require(Sample(away,480)<5,"Spotlight direction is ignored.");
        var narrow=Render(0,1,new Vector3(0,100,0),Vector3.Down,.15f);
        Require(Sample(narrow,640)<5 && Sample(baseline,640)>40,"Spotlight cone angle is ignored.");
        var sharp=Render(0,1,new Vector3(0,100,0),Vector3.Down,.6f,64);
        Require(Sample(baseline,640)>Sample(sharp,640)+40,"Spotlight cone decay is ignored.");
        var back=Render(0,1,new Vector3(0,100,0),Vector3.Down,flipNormal:true);
        Require(Sample(back,480)<5,"Spotlight surface normals are ignored.");
        Render(0,1,new Vector3(0,100,0),Vector3.Down);
        using var stream=File.Create(Path.Combine(Path.GetDirectoryName(output)!,"spotlight-preview.png"));
        target.SaveAsPng(stream,Width,Height);
        Console.WriteLine("PASS: spotlight loading, matrices, normals, position, direction, ambient, strength, cone angle, and decay.");
    }

    void TestBlur()
    {
        var effect=Content.Load<Effect>("Effects/Blur");
        effect.CurrentTechnique=effect.Techniques["GaussianBlur"];
        effect.Parameters["ScreenResolution"].SetValue(new Vector2(9,9));
        using var texture=new Texture2D(GraphicsDevice,9,9);
        var data=new Color[81];data[40]=Color.White;texture.SetData(data);
        using var target=new RenderTarget2D(GraphicsDevice,9,9);
        using var batch=new SpriteBatch(GraphicsDevice);
        Color[] Render(Color tint)
        {
            GraphicsDevice.SetRenderTarget(target);GraphicsDevice.Clear(Color.Transparent);
            batch.Begin(SpriteSortMode.Immediate,BlendState.Opaque,SamplerState.PointClamp,DepthStencilState.None,RasterizerState.CullNone,effect);
            batch.Draw(texture,Vector2.Zero,tint);batch.End();GraphicsDevice.SetRenderTarget(null);
            var pixels=new Color[81];target.GetData(pixels);return pixels;
        }
        void Require(bool condition,string message){if(!condition)throw new Exception(message);}
        var blurred=Render(Color.White);
        Require(Math.Abs(blurred[40].R-64)<5 && Math.Abs(blurred[40].A-64)<5,"Blur center weight or alpha is incorrect.");
        Require(Math.Abs(blurred[39].R-32)<5 && Math.Abs(blurred[30].R-16)<5,"Blur neighboring weights are incorrect.");
        Require(blurred[0].R==0 && Math.Abs(blurred.Sum(p=>(int)p.R)-255)<12,"Blur radius or energy is incorrect.");
        var tinted=Render(Color.Green);
        Require(tinted[40].R==0 && tinted[40].G>20,"Blur does not preserve SpriteBatch tint.");
        Render(Color.White);
        using var stream=File.Create(Path.Combine(Path.GetDirectoryName(output)!,"blur-kernel-preview.png"));target.SaveAsPng(stream,9,9);
        Console.WriteLine("PASS: Gaussian blur loading, SpriteBatch shader compatibility, kernel weights, alpha, tint, and energy.");
    }

    void TestParticles()
    {
        var effect=Content.Load<Effect>("Effects/ParticleEffect");
        foreach(var name in new[]{"View","Projection","ViewportScale","CurrentTime","Duration","DurationRandomness","Gravity","EndVelocity","MinColor","MaxColor","RotateSpeed","StartSize","EndSize","Texture"})
            if(effect.Parameters[name]==null)throw new Exception("Missing particle parameter: "+name);
        effect.CurrentTechnique=effect.Techniques["Particles"];
        using var texture=new Texture2D(GraphicsDevice,1,1);texture.SetData(new[]{Color.White});
        using var clearTexture=new Texture2D(GraphicsDevice,1,1);clearTexture.SetData(new[]{Color.Transparent});
        using var target=new RenderTarget2D(GraphicsDevice,Width,Height);
        var vertices=new[]{new GpuParticle(-1,-1),new GpuParticle(1,-1),new GpuParticle(-1,1),new GpuParticle(1,1)};
        short[] indices=[0,1,2,2,1,3];
        Color[] Render(float time,Vector3 velocity=default,Vector3 gravity=default,float endVelocity=1,bool growth=false,float rotation=0,bool randomAge=false,bool sideView=false,Color? tint=null,bool transparent=false)
        {
            GraphicsDevice.SetRenderTarget(target);GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.DepthStencilState=DepthStencilState.None;GraphicsDevice.RasterizerState=RasterizerState.CullNone;
            GraphicsDevice.BlendState=BlendState.NonPremultiplied;
            effect.Parameters["View"].SetValue(Matrix.CreateLookAt(sideView?new Vector3(10,0,0):new Vector3(0,0,10),Vector3.Zero,Vector3.Up));
            effect.Parameters["Projection"].SetValue(Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,(float)Width/Height,.1f,100));
            effect.Parameters["ViewportScale"].SetValue(new Vector2(.5f/((float)Width/Height),-.5f));
            effect.Parameters["CurrentTime"].SetValue(time);effect.Parameters["Duration"].SetValue(2f);
            effect.Parameters["DurationRandomness"].SetValue(randomAge?1f:0f);
            effect.Parameters["Gravity"].SetValue(gravity);effect.Parameters["EndVelocity"].SetValue(endVelocity);
            effect.Parameters["MinColor"].SetValue((tint??Color.White).ToVector4());effect.Parameters["MaxColor"].SetValue((tint??Color.White).ToVector4());
            effect.Parameters["RotateSpeed"].SetValue(new Vector2(rotation));
            effect.Parameters["StartSize"].SetValue(new Vector2(growth?1:2));effect.Parameters["EndSize"].SetValue(new Vector2(growth?3:2));
            effect.Parameters["Texture"].SetValue(transparent?clearTexture:texture);
            for(int i=0;i<vertices.Length;i++){vertices[i].Velocity=velocity;vertices[i].Random=new Color(randomAge?255:0,0,0,0);}
            effect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,vertices,0,4,indices,0,2);
            GraphicsDevice.SetRenderTarget(null);
            var pixels=new Color[Width*Height];target.GetData(pixels);return pixels;
        }
        void Require(bool condition,string message){if(!condition)throw new Exception(message);}
        int Center(Color[] p)=>p[(Height/2)*Width+Width/2].R;
        (double X,double Y,int Width) Bounds(Color[] p)
        {
            long x=0,y=0,count=0;int min=Width,max=0;
            for(int i=0;i<p.Length;i++)if(p[i].R>20){int px=i%Width; x+=px;y+=i/Width;count++;min=Math.Min(min,px);max=Math.Max(max,px);}
            return count==0?(0,0,0):((double)x/count,(double)y/count,max-min+1);
        }
        var middle=Render(1);
        Require(Center(middle)>180,"Live particle did not render.");
        Require(Center(Render(0))==0 && Center(Render(2))==0 && Center(Render(-.1f))==0,"Particle lifetime fade is incorrect.");
        Require(Bounds(Render(1,new Vector3(3,0,0))).X>Bounds(middle).X+200,"Particle velocity is ignored.");
        Require(Bounds(Render(1,new Vector3(3,0,0))).X>Bounds(Render(1,new Vector3(3,0,0),endVelocity:0)).X+40,"Particle end velocity is ignored.");
        Require(Bounds(Render(1,gravity:new Vector3(0,-2,0))).Y>Bounds(middle).Y+60,"Particle gravity is ignored.");
        Require(Bounds(Render(1,growth:true)).Width>Bounds(Render(.3f,growth:true)).Width+35,"Particle size interpolation is ignored.");
        Require(Bounds(Render(1,rotation:MathHelper.PiOver4)).Width>Bounds(middle).Width+35,"Particle rotation is ignored.");
        Require(Center(Render(.6f))>Center(Render(.6f,randomAge:true))+50,"Particle lifetime randomness is ignored.");
        Require(Center(Render(1,sideView:true))>180,"Particle billboard does not face the camera.");
        Require(Render(1,tint:Color.Red)[(Height/2)*Width+Width/2].G==0,"Particle color is ignored.");
        Require(Center(Render(1,transparent:true))==0,"Particle texture alpha is ignored.");
        Render(1,tint:Color.Orange);
        using var stream=File.Create(Path.Combine(Path.GetDirectoryName(output)!,"particle-preview.png"));target.SaveAsPng(stream,Width,Height);
        Console.WriteLine("PASS: particle vertex layout, birth/death, randomness, movement, gravity, size, rotation, billboarding, color, and texture alpha.");
    }
}

struct GpuParticle : IVertexType
{
    public Microsoft.Xna.Framework.Graphics.PackedVector.Short2 Corner;
    public Vector3 Position;
    public Vector3 Velocity;
    public Color Random;
    public float Time;
    public GpuParticle(short x,short y){Corner=new(x,y);Position=Velocity=Vector3.Zero;Random=Color.Transparent;Time=0;}
    static readonly VertexDeclaration Declaration=new(
        new VertexElement(0,VertexElementFormat.Short2,VertexElementUsage.Position,0),
        new VertexElement(4,VertexElementFormat.Vector3,VertexElementUsage.Position,1),
        new VertexElement(16,VertexElementFormat.Vector3,VertexElementUsage.Normal,0),
        new VertexElement(28,VertexElementFormat.Color,VertexElementUsage.Color,0),
        new VertexElement(32,VertexElementFormat.Single,VertexElementUsage.TextureCoordinate,0));
    VertexDeclaration IVertexType.VertexDeclaration=>Declaration;
}
