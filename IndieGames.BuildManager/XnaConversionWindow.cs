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
    readonly Button convert=new(){Content="Convert separate copy"};
    readonly LibraryStorage storage; CancellationTokenSource? work; string? output;
    public XnaConversionWindow(LibraryStorage library)
    {
        storage=library;Title=AppVersion.WindowTitle("Convert Compiled XNA Game");Width=900;Height=650;MinWidth=700;MinHeight=520;WindowStartupLocation=WindowStartupLocation.CenterOwner;
        Background=new SolidColorBrush(Color.FromRgb(17,24,39));Foreground=Brushes.White;
        Resources.MergedDictionaries.Add(new ResourceDictionary{Source=new Uri("/IndieGames.BuildManager;component/Themes/ManagerTheme.xaml",UriKind.Relative)});
        var panel=new DockPanel{Margin=new Thickness(20)};Content=panel;
        var top=new StackPanel();DockPanel.SetDock(top,Dock.Top);panel.Children.Add(top);
        top.Children.Add(new TextBlock{Text="Convert Compiled XNA Game",FontSize=23,FontWeight=FontWeights.SemiBold});
        top.Children.Add(new TextBlock{Text="Uses upstream XnaToFna v18.05.1 for original .NET Framework games. Originals stay untouched. Rebuilt modern .NET games should use XnaAssemblyRedirector for legacy libraries.",TextWrapping=TextWrapping.Wrap,Margin=new Thickness(0,8,0,14)});
        void Field(string title,TextBox box,System.Action browse){top.Children.Add(new TextBlock{Text=title,Margin=new Thickness(3,8,3,4)});var row=new DockPanel();var button=new Button{Content="Browse…"};button.Click+=(_,_)=>browse();DockPanel.SetDock(button,Dock.Right);row.Children.Add(button);row.Children.Add(box);top.Children.Add(row);}
        Field("Original game EXE (its whole folder will be copied)",executable,()=>{var d=new OpenFileDialog{Filter="Game executable (*.exe)|*.exe"};if(d.ShowDialog(this)==true)executable.Text=d.FileName;});
        Field("Matching legacy FNA runtime folder (FNA.dll, SDL2 and native libraries)",runtime,()=>{var d=new OpenFolderDialog();if(d.ShowDialog(this)==true)runtime.Text=d.FolderName;});
        top.Children.Add(new TextBlock{Text="Output library: "+storage.Games,TextWrapping=TextWrapping.Wrap,Margin=new Thickness(3,10,3,10)});
        var actions=new WrapPanel();top.Children.Add(actions);
        void Button(string title,System.Action click){var b=new Button{Content=title};b.Click+=(_,_)=>click();actions.Children.Add(b);}
        Button("Check compatibility",()=>{var check=XnaBinaryConversion.Check(executable.Text,runtime.Text);report.Text=(check.Compatible?"Ready for conversion check run":"Cannot convert this combination")+Environment.NewLine+string.Join(Environment.NewLine,check.Details);});
        convert.Click+=async(_,_)=>{if(work!=null)return;work=new();convert.IsEnabled=false;executable.IsReadOnly=runtime.IsReadOnly=true;string exe=executable.Text,rt=runtime.Text;report.Clear();try{output=await Task.Run(()=>XnaBinaryConversion.ConvertAsync(exe,rt,storage.Games,Log,work.Token));}catch(Exception ex){Log(ex.GetBaseException().Message);}finally{work.Dispose();work=null;convert.IsEnabled=true;executable.IsReadOnly=runtime.IsReadOnly=false;}};actions.Children.Add(convert);
        Button("Cancel",()=>work?.Cancel());Button("Open converted folder",()=>{if(output!=null)Process.Start(new ProcessStartInfo(output){UseShellExecute=true});});
        top.Children.Add(new TextBlock{Text="Includes original binary backups, SHA-256 inventory, conversion.log and conversion-report.json. Content stays unchanged; audio, video and shaders may need separate repairs. A successful conversion still needs a launch test.",TextWrapping=TextWrapping.Wrap,Margin=new Thickness(3,10,3,12),Foreground=Brushes.LightSteelBlue});
        panel.Children.Add(report);Closing+=(_,e)=>{if(work!=null){work.Cancel();e.Cancel=true;Log("Cancelling conversion; close again after it finishes.");}};
    }
    void Log(string message){if(!Dispatcher.CheckAccess()){Dispatcher.InvokeAsync(()=>Log(message));return;}report.AppendText(message+Environment.NewLine);report.ScrollToEnd();}
}
