using IndieGames.BuildManager.Services;
using Microsoft.CodeAnalysis.CSharp;
string root=Path.Combine(Path.GetTempPath(),"overlay-installer-"+Guid.NewGuid().ToString("N"));Directory.CreateDirectory(root);
string core=Path.Combine(root,"core");Directory.CreateDirectory(Path.Combine(core,"IndieGames.Overlay.FNA"));File.WriteAllText(Path.Combine(core,"IndieGames.Overlay.FNA","IndieGames.Overlay.FNA.csproj"),"<Project/>");
void Require(bool value,string message){if(!value)throw new Exception(message);}
(string Project,string Source) Fixture(string name,string draw){
 string folder=Path.Combine(root,name);Directory.CreateDirectory(folder);
 string project=Path.Combine(folder,"Game.csproj"),source=Path.Combine(folder,"Game.cs");
 File.WriteAllText(project,"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net9.0-windows</TargetFramework><OutputType>WinExe</OutputType></PropertyGroup><ItemGroup><ProjectReference Include=\"FNA.Core.csproj\"/></ItemGroup></Project>");
 File.WriteAllText(source,"using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; class Game1 : Game { GraphicsDeviceManager graphics; SpriteBatch spriteBatch; public Game1(){graphics=new GraphicsDeviceManager(this);} protected override void Update(GameTime t){base.Update(t);} protected override void Draw(GameTime t){"+draw+"base.Draw(t);} }");return(project,source);
}
const string simple="GraphicsDevice.Clear(Color.Black);spriteBatch.Begin();spriteBatch.Draw(texture,new Rectangle(0,0,800,450),Color.White);spriteBatch.End();";
var fresh=Fixture("fresh",simple);var first=GameOverlayInstaller.Install(fresh.Project,core);
Require(first.Status=="Installed"&&File.Exists(first.Backup),"Fresh install must back up");
string text=File.ReadAllText(fresh.Source);Require(text.Contains("GameCanvas(GraphicsDevice,800,450)")&&text.Contains("PresentLastFrame()")&&text.Contains("__buildManagerCanvas?.Dispose()"),"Canvas hooks missing");
Require(!CSharpSyntaxTree.ParseText(text).GetDiagnostics().Any(d=>d.Severity==Microsoft.CodeAnalysis.DiagnosticSeverity.Error),"Generated syntax invalid");
Require(GameOverlayInstaller.Install(fresh.Project,core).Status=="Already installed"&&File.ReadAllText(fresh.Source)==text,"Repeat must not duplicate canvas");
var upgrade=Fixture("upgrade",simple);File.WriteAllText(upgrade.Source,text.Replace("private IndieGames.Overlay.GameCanvas? __buildManagerCanvas;","").Replace("__buildManagerCanvas.Present();","").Replace("protected override void UnloadContent() { __buildManagerCanvas?.Dispose(); base.UnloadContent(); }","").Replace("if (__buildManagerOverlay.IsOpen && __buildManagerCanvas != null) { __buildManagerCanvas.PresentLastFrame(); base.Draw(t); return; }","").Replace("__buildManagerCanvas ??= new IndieGames.Overlay.GameCanvas(GraphicsDevice,800,450);","").Replace("__buildManagerCanvas.Begin();",""));
var upgraded=GameOverlayInstaller.Install(upgrade.Project,core);Require(upgraded.Status=="Installed"&&File.ReadAllText(upgrade.Source).Contains("GameCanvas(GraphicsDevice,800,450)"),"Existing menu must gain canvas");
foreach(var pair in new[]{("custom",simple+"DrawScene();"),("dynamic",simple+"var width=GraphicsDevice.Viewport.Width;"),("targets",simple+"GraphicsDevice.SetRenderTarget(target);"),("ambiguous",simple+"spriteBatch.Draw(texture,new Rectangle(0,0,1280,720),Color.White);"),("unknown","spriteBatch.Begin();spriteBatch.Draw(texture,position,Color.White);spriteBatch.End();")}){
 var fixture=Fixture(pair.Item1,pair.Item2);var result=GameOverlayInstaller.Install(fixture.Project,core);Require(result.Status=="Installed"&&!File.ReadAllText(fixture.Source).Contains("GameCanvas?"),"Unsafe or unknown layout received canvas: "+pair.Item1);
}
if(args.Length==2){
 string folder=Path.Combine(root,"fright-light");Directory.CreateDirectory(folder);
 string project=Path.Combine(folder,"Fright.csproj");File.Copy(args[0],project);string source=Path.Combine(folder,"Game1.cs");File.Copy(args[1],source);
 var upgradedGame=GameOverlayInstaller.Install(project,core);
 Require(upgradedGame.Status=="Installed"&&File.ReadAllText(source).Contains("GameCanvas(GraphicsDevice,800,450)"),"Full Fright Light source was not automatically upgraded: "+upgradedGame.Message);
 Console.WriteLine("PASS actual Fright Light existing overlay upgraded to 800x450 canvas");
}
Console.WriteLine("PASS fresh canvas install, existing overlay upgrade, backup, repeat, syntax, custom render rejection and ambiguous layout rejection");
Directory.Delete(root,true);
