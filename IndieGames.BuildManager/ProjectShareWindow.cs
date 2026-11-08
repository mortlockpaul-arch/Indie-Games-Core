using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IndieGames.BuildManager;

/// <summary>The share editor has its own space, while the catalogue owns transfers and their lifetime.</summary>
public sealed class ProjectShareWindow : Window
{
    private readonly ScrollViewer _form;
    private readonly TextBox _status = new() { IsReadOnly=true, AcceptsReturn=true, Height=100, TextWrapping=TextWrapping.Wrap, VerticalScrollBarVisibility=ScrollBarVisibility.Auto };
    private readonly Button _submission = new() { Content="View Submission", IsEnabled=false };
    private readonly Button _close = new() { Content="Close Window", ToolTip="Return to the main Build Manager window. Completed submissions and seeding are kept." };
    private readonly Button _cancel = new() { Content="Cancel Operation", IsEnabled=false };
    private readonly TextBlock _nextSteps = new() { Text="Review the files, then publish for approval. Approved packages appear in everyone's global catalogue.", TextWrapping=TextWrapping.Wrap, Foreground=Brushes.LightSteelBlue, Margin=new Thickness(4,8,4,4) };
    private string? _submissionUrl;
    private readonly Panel? _actionsParent;
    private readonly FrameworkElement? _actions;
    private readonly int _actionsIndex;
    private readonly StackPanel _footer;
    private readonly ProgressBar _progress = new() { Visibility=Visibility.Collapsed, Height=8, Maximum=1, Margin=new Thickness(4) };
    public void SetBusy(bool busy) { _progress.IsIndeterminate=busy; _progress.Value=0; _progress.Visibility=busy ? Visibility.Visible : Visibility.Collapsed; _cancel.IsEnabled=busy; _close.IsEnabled=!busy; _submission.IsEnabled=_submissionUrl != null && !busy; }
    public void ShowSubmission(string url, string state) { _submissionUrl=url; _submission.IsEnabled=true; _nextSteps.Text=state + ": " + url + ". Keep Build Manager running to seed your package. The owner must review and merge the submission before other users can see it."; }
    public ProjectShareWindow(Window owner, object form, Func<bool> busy, Action cancel, Action<string> openSubmission, FrameworkElement? actions = null)
    {
        Owner=owner; Title=AppVersion.WindowTitle("New Catalogue Submission"); Width=980; Height=800; MinWidth=740; MinHeight=640;
        WindowStartupLocation=WindowStartupLocation.CenterOwner; Background=owner.Background; Foreground=owner.Foreground;
        Resources.MergedDictionaries.Add(new ResourceDictionary { Source=new Uri("/IndieGames.BuildManager;component/Themes/ManagerTheme.xaml",UriKind.Relative) });
        var panel=new DockPanel { Margin=new Thickness(16) }; Content=panel;
        var heading=new StackPanel(); DockPanel.SetDock(heading,Dock.Top); panel.Children.Add(heading);
        heading.Children.Add(new TextBlock { Text="Submit a Game to the Catalogue", FontSize=22, FontWeight=FontWeights.SemiBold });
        heading.Children.Add(new TextBlock { Text="Choose a game, review the package, and submit it for approval. Close window returns to the main Build Manager window; it does not quit Build Manager or withdraw a completed submission.", TextWrapping=TextWrapping.Wrap, Foreground=Brushes.LightGray, Margin=new Thickness(0,6,0,12) });
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
        var help=new Button { Content="Help / Sharing Steps" }; help.Click+=(_,_)=>SharingHelpWindow.ShowFor(this); buttons.Children.Add(help);
        _submission.Click+=(_,_)=> { if (_submissionUrl != null) openSubmission(_submissionUrl); }; buttons.Children.Add(_submission);
        _cancel.Click+=(_,_)=>cancel(); buttons.Children.Add(_cancel);
        _close.Click+=(_,_)=>Close(); buttons.Children.Add(_close);
        _form=new ScrollViewer { Content=form, VerticalScrollBarVisibility=ScrollBarVisibility.Auto }; panel.Children.Add(_form);
        Closing+=(_,e)=> { if (busy()) { e.Cancel=true; Log("Submission is still running. Wait for it to finish, or choose Cancel operation before closing this window."); } };
    }
    public void Log(string message) { _status.AppendText(message+Environment.NewLine); _status.ScrollToEnd(); }
    public void DetachForm()
    {
        _form.Content=null;
        if (_actions != null && _actionsParent != null) { _footer.Children.Remove(_actions); _actionsParent.Children.Insert(Math.Min(_actionsIndex,_actionsParent.Children.Count),_actions); }
    }
}
