using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IndieGames.BuildManager;

/// <summary>The share editor has its own space, while the catalogue owns transfers and their lifetime.</summary>
public sealed class ProjectShareWindow : Window
{
    private readonly ScrollViewer _form;
    private readonly TextBox _status = new() { IsReadOnly=true, AcceptsReturn=true, Height=100, TextWrapping=TextWrapping.Wrap, VerticalScrollBarVisibility=ScrollBarVisibility.Auto };
    private readonly Button _export = new() { Content="Save another catalogue copy…", IsEnabled=false };
    private readonly Button _cancel = new() { Content="Cancel operation", IsEnabled=false };
    private readonly TextBlock _nextSteps = new() { Text="Choose a game and review its files. Creating the share will also save the catalogue recipients need.", TextWrapping=TextWrapping.Wrap, Foreground=Brushes.LightSteelBlue, Margin=new Thickness(4,8,4,4) };
    private bool _hasCreated;
    private readonly Panel? _actionsParent;
    private readonly FrameworkElement? _actions;
    private readonly int _actionsIndex;
    private readonly StackPanel _footer;
    private readonly ProgressBar _progress = new() { Height=8, Maximum=1, Margin=new Thickness(4) };
    public void SetBusy(bool busy) { _progress.IsIndeterminate=busy; _progress.Value=busy ? 0 : 1; _cancel.IsEnabled=busy; _export.IsEnabled=_hasCreated && !busy; }
    public void EnableExport() { _hasCreated=true; _export.IsEnabled=!_progress.IsIndeterminate; }
    public void ShowSaved(string path) => _nextSteps.Text="Ready to share: send " + path + ". Recipients open it in the catalogue, download, then verify and install. Keep this app running while they download.";
    public ProjectShareWindow(Window owner, object form, Func<bool> busy, Action cancel, Action export, FrameworkElement? actions = null)
    {
        Owner=owner; Title=AppVersion.WindowTitle("Create Project Share"); Width=980; Height=800; MinWidth=740; MinHeight=640;
        WindowStartupLocation=WindowStartupLocation.CenterOwner; Background=owner.Background; Foreground=owner.Foreground;
        Resources.MergedDictionaries.Add(new ResourceDictionary { Source=new Uri("/IndieGames.BuildManager;component/Themes/ManagerTheme.xaml",UriKind.Relative) });
        var panel=new DockPanel { Margin=new Thickness(16) }; Content=panel;
        var heading=new StackPanel(); DockPanel.SetDock(heading,Dock.Top); panel.Children.Add(heading);
        heading.Children.Add(new TextBlock { Text="Create Project Share", FontSize=22, FontWeight=FontWeights.SemiBold });
        heading.Children.Add(new TextBlock { Text="Choose a game, review the package, then create and save a catalogue to send to other people.", TextWrapping=TextWrapping.Wrap, Foreground=Brushes.LightGray, Margin=new Thickness(0,6,0,12) });
        _footer=new StackPanel(); DockPanel.SetDock(_footer,Dock.Bottom); panel.Children.Add(_footer);
        _footer.Children.Add(_progress);
        _footer.Children.Add(_nextSteps);
        _status.Height=70; _footer.Children.Add(_status);
        if (actions?.Parent is Panel parent)
        {
            _actions=actions; _actionsParent=parent; _actionsIndex=parent.Children.IndexOf(actions);
            parent.Children.Remove(actions); actions.HorizontalAlignment=HorizontalAlignment.Right; _footer.Children.Add(actions);
        }
        var buttons=new WrapPanel { HorizontalAlignment=HorizontalAlignment.Right }; _footer.Children.Add(buttons);
        var help=new Button { Content="Help / sharing steps" }; help.Click+=(_,_)=>SharingHelpWindow.ShowFor(this); buttons.Children.Add(help);
        _export.Click+=(_,_)=>export(); buttons.Children.Add(_export);
        _cancel.Click+=(_,_)=>cancel(); buttons.Children.Add(_cancel);
        var close=new Button { Content="Close" }; close.Click+=(_,_)=>Close(); buttons.Children.Add(close);
        _form=new ScrollViewer { Content=form, VerticalScrollBarVisibility=ScrollBarVisibility.Auto }; panel.Children.Add(_form);
        Closing+=(_,e)=> { if (busy()) { e.Cancel=true; cancel(); Log("Cancelling the current operation. Close this window once it stops."); } };
    }
    public void Log(string message) { _status.AppendText(message+Environment.NewLine); _status.ScrollToEnd(); }
    public void DetachForm()
    {
        _form.Content=null;
        if (_actions != null && _actionsParent != null) { _footer.Children.Remove(_actions); _actionsParent.Children.Insert(Math.Min(_actionsIndex,_actionsParent.Children.Count),_actions); }
    }
}
