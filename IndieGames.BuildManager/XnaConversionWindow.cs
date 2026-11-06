using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using IndieGames.BuildManager.Services;

namespace IndieGames.BuildManager;

public sealed class XnaConversionWindow : Window
{
    readonly TextBox executable=new(),runtime=new(),report=new(){IsReadOnly=true,AcceptsReturn=true,TextWrapping=TextWrapping.Wrap,VerticalScrollBarVisibility=ScrollBarVisibility.Auto};
    readonly Button convert=new(){Content="3. Convert separate copy",IsEnabled=false};
    readonly LibraryStorage storage; CancellationTokenSource? work; string? output;
    public XnaConversionWindow(LibraryStorage library, string? projectPath = null)
    {
        storage=library;Title=AppVersion.WindowTitle("Convert Compiled XNA Game");Width=900;Height=650;MinWidth=700;MinHeight=520;WindowStartupLocation=WindowStartupLocation.CenterOwner;
        Background=new SolidColorBrush(Color.FromRgb(17,24,39));Foreground=Brushes.White;
        Resources.MergedDictionaries.Add(new ResourceDictionary{Source=new Uri("/IndieGames.BuildManager;component/Themes/ManagerTheme.xaml",UriKind.Relative)});
        executable.Text = XnaBinaryConversion.FindOriginalExecutable(projectPath) ?? "";
        if (Directory.Exists(storage.LegacyFnaRuntime))
            runtime.Text = storage.LegacyFnaRuntime;

        var panel=new DockPanel{Margin=new Thickness(20)};Content=panel;
        var top=new StackPanel();DockPanel.SetDock(top,Dock.Top);panel.Children.Add(top);
        top.Children.Add(new TextBlock{Text="Convert Compiled XNA Game",FontSize=23,FontWeight=FontWeights.SemiBold});
        top.Children.Add(new TextBlock{Text="Create a separate Windows port from an original XNA executable. To rebuild a C# project, return to the project list and choose Build.",TextWrapping=TextWrapping.Wrap,Margin=new Thickness(0,8,0,14)});
        void Field(string title,TextBox box,System.Action browse){top.Children.Add(new TextBlock{Text=title,Margin=new Thickness(3,8,3,4)});var row=new DockPanel();var button=new Button{Content="Browse…"};button.Click+=(_,_)=>browse();DockPanel.SetDock(button,Dock.Right);row.Children.Add(button);row.Children.Add(box);top.Children.Add(row);}
        Field("1. Original XNA game EXE (usually inside 584E07D1)",executable,()=>{var d=new OpenFileDialog{Filter="Game executable (*.exe)|*.exe"};if(d.ShowDialog(this)==true)executable.Text=d.FileName;});
        Field("2. Legacy FNA runtime folder (FNA.dll, SDL2.dll and matching native libraries)",runtime,()=>{var d=new OpenFolderDialog();if(d.ShowDialog(this)==true)runtime.Text=d.FolderName;});
        var status = new TextBlock { TextWrapping = TextWrapping.Wrap, Foreground = Brushes.LightSteelBlue, Margin = new Thickness(3, 10, 3, 4) };
        top.Children.Add(status);

        void RefreshStatus()
        {
            bool hasExecutable = File.Exists(executable.Text.Trim());
            bool hasRuntime = Directory.Exists(runtime.Text.Trim());
            convert.IsEnabled = work is null && hasExecutable && hasRuntime;
            status.Text = !hasExecutable
                ? "Choose the original EXE. Build Manager fills it in when the selected project has one matching executable."
                : !hasRuntime
                    ? "Choose a legacy .NET Framework FNA runtime. Build Manager remembers it after conversion."
                    : "Ready to check compatibility or convert. A successful conversion enables Run converted game.";
        }

        executable.TextChanged += (_, _) => RefreshStatus();
        runtime.TextChanged += (_, _) => RefreshStatus();
        RefreshStatus();
        top.Children.Add(new TextBlock{Text="Converted copies go to "+storage.Games,TextWrapping=TextWrapping.Wrap,Margin=new Thickness(3,4,3,10)});
        var actions=new WrapPanel();top.Children.Add(actions);
        void Button(string title,System.Action click){var b=new Button{Content=title};b.Click+=(_,_)=>click();actions.Children.Add(b);}
        Button("Check compatibility",()=>{var check=XnaBinaryConversion.Check(executable.Text,runtime.Text);report.Text=(check.Compatible?"Ready for conversion check run":"Cannot convert this combination")+Environment.NewLine+string.Join(Environment.NewLine,check.Details);});
        var run = new Button { Content = "Run converted game", IsEnabled = false };
        run.Click += (_, _) =>
        {
            if (output is null) return;
            try
            {
                Process.Start(new ProcessStartInfo(PortableGames.ResolveExecutable(output))
                {
                    WorkingDirectory = output,
                    UseShellExecute = true
                })?.Dispose();
            }
            catch (Exception error) { Log(error.GetBaseException().Message); }
        };

        convert.Click += async (_, _) =>
        {
            if (work is not null) return;
            string exe = executable.Text.Trim(), rt = runtime.Text.Trim();
            if (exe.Length == 0 || rt.Length == 0)
            {
                Log("Choose an original game EXE and a compatible legacy FNA runtime folder.");
                return;
            }

            work = new();
            output = null;
            run.IsEnabled = convert.IsEnabled = false;
            executable.IsReadOnly = runtime.IsReadOnly = true;
            report.Clear();
            try
            {
                output = await Task.Run(() => XnaBinaryConversion.ConvertAsync(exe, rt, storage.Games, Log, work.Token));
                run.IsEnabled = true;
                storage.LegacyFnaRuntime = Path.GetFullPath(rt);
                try { storage.Save(); }
                catch (Exception error) when (error is IOException or UnauthorizedAccessException)
                { Log("Could not remember the runtime folder: " + error.Message); }
            }
            catch (Exception error) { Log(error.GetBaseException().Message); }
            finally
            {
                work.Dispose();
                work = null;
                RefreshStatus();
                executable.IsReadOnly = runtime.IsReadOnly = false;
            }
        };
        actions.Children.Add(convert);
        actions.Children.Add(run);
        Button("Cancel",()=>work?.Cancel());Button("Open converted folder",()=>{if(output!=null)Process.Start(new ProcessStartInfo(output){UseShellExecute=true});});
        top.Children.Add(new TextBlock{Text="Includes original binary backups, SHA-256 inventory, conversion.log and conversion-report.json. Content stays unchanged; audio, video and shaders may need separate repairs. A successful conversion still needs a launch test.",TextWrapping=TextWrapping.Wrap,Margin=new Thickness(3,10,3,12),Foreground=Brushes.LightSteelBlue});
        panel.Children.Add(report);Closing+=(_,e)=>{if(work!=null){work.Cancel();e.Cancel=true;Log("Cancelling conversion; close again after it finishes.");}};
    }
    void Log(string message){if(!Dispatcher.CheckAccess()){Dispatcher.InvokeAsync(()=>Log(message));return;}report.AppendText(message+Environment.NewLine);report.ScrollToEnd();}
}
