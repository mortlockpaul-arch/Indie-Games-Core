using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using IndieGames.BuildManager.Services;
namespace IndieGames.BuildManager;
public sealed class BackupCleanupWindow : Window
{
 public BackupCleanupWindow(string archive)
 {
  Title=AppVersion.WindowTitle("Delete All Backups — Recycle Bin");Width=1050;Height=650;MinWidth=700;MinHeight=450;WindowStartupLocation=WindowStartupLocation.CenterOwner;
  Background=System.Windows.Media.Brushes.Black;Foreground=System.Windows.Media.Brushes.White;
  Resources.MergedDictionaries.Add(new ResourceDictionary{Source=new Uri("/IndieGames.BuildManager;component/Themes/ManagerTheme.xaml",UriKind.Relative)});
  var root=new DockPanel{Margin=new Thickness(16)};Content=root;
  var heading=new TextBlock{Text="Delete all backups from this archive\n"+archive+"\nPreview first. Recognized backup folders and loose .bak / .before-* files go to the Recycle Bin. Linked paths and changed items are refused. No permanent-delete fallback.",TextWrapping=TextWrapping.Wrap,Margin=new Thickness(5)};DockPanel.SetDock(heading,Dock.Top);root.Children.Add(heading);
  var footer=new StackPanel();DockPanel.SetDock(footer,Dock.Bottom);root.Children.Add(footer);
  var status=new TextBlock{Text="Nothing scanned or deleted.",TextWrapping=TextWrapping.Wrap,Margin=new Thickness(5)};footer.Children.Add(status);
  var buttons=new WrapPanel();footer.Children.Add(buttons);
  var preview=new Button{Content="Preview all backups"};var recycle=new Button{Content="Move previewed backups to Recycle Bin",IsEnabled=false};var close=new Button{Content="Close window"};buttons.Children.Add(preview);buttons.Children.Add(recycle);buttons.Children.Add(close);
  var grid=new DataGrid{AutoGenerateColumns=false,IsReadOnly=true,CanUserAddRows=false};grid.Columns.Add(new DataGridTextColumn{Header="Backup",Binding=new Binding("Path"),Width=new DataGridLength(1,DataGridLengthUnitType.Star)});grid.Columns.Add(new DataGridTextColumn{Header="Bytes",Binding=new Binding("Bytes"),Width=130});grid.Columns.Add(new DataGridTextColumn{Header="Files",Binding=new Binding("Files"),Width=80});root.Children.Add(grid);
  IReadOnlyList<BackupCleanupEntry> entries=[];bool busy=false;
  void Busy(bool value){busy=value;preview.IsEnabled=!value;recycle.IsEnabled=!value&&entries.Count>0;close.IsEnabled=!value;}
  preview.Click+=async(_,_)=>{Busy(true);try{entries=await Task.Run(()=>BackupCleanup.Preview(archive));grid.ItemsSource=entries;status.Text=$"{entries.Count:N0} backup items, {entries.Sum(e=>e.Files):N0} files, {entries.Sum(e=>e.Bytes)/1048576d:N1} MB. Nothing deleted.";}catch(Exception e){entries=[];status.Text="Preview stopped: "+e.Message;}finally{Busy(false);}};
  recycle.Click+=async(_,_)=>{
   if(entries.Count==0)return;
   if(MessageBox.Show(this,$"Move these {entries.Count:N0} previewed backup items ({entries.Sum(e=>e.Bytes)/1048576d:N1} MB) from {archive} to the Recycle Bin?\n\nRecovery depends on Windows Recycle Bin availability. Changed or linked paths will be refused. There is no permanent-delete fallback.","Confirm backup cleanup",MessageBoxButton.YesNo,MessageBoxImage.Warning)!=MessageBoxResult.Yes)return;
   Busy(true);var selected=entries;int completed=0;try{await Task.Run(()=>{foreach(var entry in selected){BackupCleanup.Recycle(entry);completed++;}});status.Text=$"Moved {completed:N0} backup items to the Recycle Bin.";}catch(Exception e){status.Text=$"Stopped after {completed:N0} items: {e.Message}. Preview again before retrying.";}finally{entries=[];Busy(false);}
  };
  close.Click+=(_,_)=>Close();Closing+=(_,e)=>{if(busy)e.Cancel=true;};
 }
}
