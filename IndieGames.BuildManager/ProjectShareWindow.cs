using System.Windows;
using System.Windows.Controls;

namespace IndieGames.BuildManager;

/// <summary>The share editor has its own space, while the catalogue owns transfers and their lifetime.</summary>
public sealed class ProjectShareWindow : Window
{
    private readonly ScrollViewer _form;
    private readonly TextBox _status = new() { IsReadOnly=true, AcceptsReturn=true, Height=100, TextWrapping=TextWrapping.Wrap, VerticalScrollBarVisibility=ScrollBarVisibility.Auto };
    private readonly Button _export = new() { Content="Save Share Catalogue…", IsEnabled=false };
    private readonly ProgressBar _progress = new() { Height=8, Maximum=1, Margin=new Thickness(4) };
    public void SetBusy(bool busy) { _progress.IsIndeterminate=busy; _progress.Value=busy ? 0 : 1; }
    public void EnableExport() => _export.IsEnabled=true;
    public ProjectShareWindow(Window owner, object form, Func<bool> busy, Action cancel, Action export)
    {
        Owner=owner; Title=AppVersion.WindowTitle("Create Project Share"); Width=950; Height=720; MinWidth=700; MinHeight=500;
        WindowStartupLocation=WindowStartupLocation.CenterOwner; Background=owner.Background; Foreground=owner.Foreground;
        foreach (var key in owner.Resources.Keys) Resources.Add(key,owner.Resources[key]);
        var panel=new DockPanel { Margin=new Thickness(16) }; Content=panel;
        var heading=new StackPanel(); DockPanel.SetDock(heading,Dock.Top); panel.Children.Add(heading);
        heading.Children.Add(new TextBlock { Text="Create Project Share", FontSize=22, FontWeight=FontWeights.SemiBold });
        heading.Children.Add(new TextBlock { Text="Share source, a playable Windows game, or both. Preview the files before creating a torrent.", TextWrapping=TextWrapping.Wrap });
        var footer=new StackPanel(); DockPanel.SetDock(footer,Dock.Bottom); panel.Children.Add(footer);
        footer.Children.Add(_progress);
        footer.Children.Add(_status);
        var buttons=new WrapPanel { HorizontalAlignment=HorizontalAlignment.Right }; footer.Children.Add(buttons);
        _export.Click+=(_,_)=>export(); buttons.Children.Add(_export);
        var cancelButton=new Button { Content="Cancel Operation" }; cancelButton.Click+=(_,_)=>cancel(); buttons.Children.Add(cancelButton);
        var close=new Button { Content="Close" }; close.Click+=(_,_)=>Close(); buttons.Children.Add(close);
        _form=new ScrollViewer { Content=form, VerticalScrollBarVisibility=ScrollBarVisibility.Auto }; panel.Children.Add(_form);
        Closing+=(_,e)=> { if (busy()) { e.Cancel=true; cancel(); Log("Cancelling the current operation. Close this window once it stops."); } };
    }
    public void Log(string message) { _status.AppendText(message+Environment.NewLine); _status.ScrollToEnd(); }
    public void DetachForm() => _form.Content=null;
}
