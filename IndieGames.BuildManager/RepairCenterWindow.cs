using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IndieGames.BuildManager.Services;

namespace IndieGames.BuildManager;

public sealed class RepairCenterWindow : Window
{
    sealed record Choice(string Id, string Title, string Description, string Category, bool Sdk = false, bool Recommended = false);
    readonly Dictionary<Choice, CheckBox> _checks = new();
    readonly Dictionary<string, Button> _tabs = new();
    readonly UniformGridHost _cards = new();
    readonly TextBox _search = new();
    readonly TextBlock _selection = new();
    readonly Button _preview = new(), _apply = new();
    readonly CheckBox _restore = new() { Content = "Restore Packages", IsChecked = true };
    readonly CheckBox _clean = new() { Content = "Clean Intermediates" };
    readonly CheckBox _rids = new() { Content = "Set Shared Runtime Identifiers", IsChecked = true };
    string _category = "All Repairs";
    static SolidColorBrush Brush(string hex) => (SolidColorBrush)new BrushConverter().ConvertFromString(hex)!;
    public string[] SourceRepairs => _checks.Where(p => p.Value.IsChecked == true && !p.Key.Sdk).Select(p => p.Key.Id).ToArray();
    public string[] SdkRepairs => _checks.Where(p => p.Value.IsChecked == true && p.Key.Sdk).Select(p => p.Key.Id).ToArray();
    public bool PreviewOnly { get; private set; } = true;
    public bool RestorePackages => _restore.IsChecked == true;
    public bool CleanIntermediates => _clean.IsChecked == true;
    public bool SetRuntimeIdentifiers => _rids.IsChecked == true;

    public RepairCenterWindow(IReadOnlyList<string> games)
    {
        Title = "Repair Center"; Width = 1000; Height = 720; MinWidth = 820; MinHeight = 560;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        Background = Brush("#0B1020"); Foreground = Brush("#E6EDF8");
        FontFamily = new FontFamily("Segoe UI"); FontSize = 13;
        Resources[typeof(Button)] = new Style(typeof(Button)) {
            Setters = { new Setter(Control.PaddingProperty, new Thickness(11, 6, 11, 6)), new Setter(Control.MarginProperty, new Thickness(0, 0, 8, 0)),
                new Setter(Control.ForegroundProperty, Brush("#DCE7F8")), new Setter(Control.BackgroundProperty, Brush("#202C44")),
                new Setter(Control.BorderBrushProperty, Brush("#34445F")), new Setter(Control.CursorProperty, System.Windows.Input.Cursors.Hand) }
        };
        ((Style)Resources[typeof(Button)]).Setters.Add(new Setter(Control.TemplateProperty,
            System.Windows.Markup.XamlReader.Parse("""
            <ControlTemplate xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" TargetType="Button">
              <Border x:Name="Frame" Background="{TemplateBinding Background}" BorderBrush="{TemplateBinding BorderBrush}" BorderThickness="{TemplateBinding BorderThickness}" CornerRadius="6" Padding="{TemplateBinding Padding}">
                <ContentPresenter HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}" VerticalAlignment="Center" RecognizesAccessKey="True"/>
              </Border>
              <ControlTemplate.Triggers>
                <Trigger Property="IsMouseOver" Value="True"><Setter TargetName="Frame" Property="BorderBrush" Value="#7AD8C8"/><Setter TargetName="Frame" Property="Opacity" Value="0.85"/></Trigger>
                <Trigger Property="IsEnabled" Value="False"><Setter TargetName="Frame" Property="Opacity" Value="0.35"/></Trigger>
                <Trigger Property="IsKeyboardFocused" Value="True"><Setter TargetName="Frame" Property="BorderBrush" Value="#B6ADF7"/></Trigger>
              </ControlTemplate.Triggers>
            </ControlTemplate>
            """)));
        var shell = new Grid { Margin = new Thickness(20, 18, 20, 16) };
        shell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        shell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        shell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        Content = shell;
        var header = new Grid { Margin = new Thickness(0, 0, 0, 22) };
        header.ColumnDefinitions.Add(new ColumnDefinition()); header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        shell.Children.Add(header);
        var heading = new StackPanel(); header.Children.Add(heading);
        heading.Children.Add(new TextBlock { Text = "BUILD / REPAIR / PLAY", Foreground = Brush("#68D9CB"), FontSize = 11, FontWeight = FontWeights.SemiBold, Margin = new Thickness(0, 0, 0, 6) });
        heading.Children.Add(new TextBlock { Text = "Repair Center", FontSize = 25, FontWeight = FontWeights.SemiBold });
        heading.Children.Add(new TextBlock { Text = $"{games.Count} game{(games.Count == 1 ? "" : "s")} in your repair queue", Foreground = Brush("#A4B4CF"), Margin = new Thickness(1, 6, 0, 0), ToolTip = string.Join("\n", games) });
        var body = new Grid(); Grid.SetRow(body, 1); shell.Children.Add(body);
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(156) });
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        var navigation = new StackPanel { Margin = new Thickness(0, 0, 18, 0) }; body.Children.Add(navigation);
        navigation.Children.Add(new TextBlock { Text = "REPAIR LIBRARY", Foreground = Brush("#7285A4"), FontSize = 10, Margin = new Thickness(6, 0, 0, 12) });
        foreach (string category in new[] { "All Repairs", "Diagnostics", "Source APIs", "Rendering", "Project & SDK" }) {
            var tab = new Button { Content = category, HorizontalContentAlignment = HorizontalAlignment.Left, Margin = new Thickness(0, 0, 0, 7), BorderThickness = new Thickness(0), Padding = new Thickness(10, 8, 8, 8) };
            tab.Click += (_, _) => { _category = category; Filter(); };
            navigation.Children.Add(tab); _tabs[category] = tab;
        }
        var content = new DockPanel(); Grid.SetColumn(content, 1); body.Children.Add(content);
        var tools = new StackPanel { Margin = new Thickness(0, 0, 0, 14) }; DockPanel.SetDock(tools, Dock.Top); content.Children.Add(tools);
        tools.Children.Add(new TextBlock { Text = "Find a repair", Foreground = Brush("#A4B4CF"), Margin = new Thickness(0, 0, 0, 6) });
        _search.Background = Brush("#141E32"); _search.Foreground = Foreground; _search.BorderBrush = Brush("#34445F"); _search.Padding = new Thickness(10, 6, 10, 6);
        _search.ToolTip = "Search titles, descriptions, warning codes, or categories"; _search.TextChanged += (_, _) => Filter(); tools.Children.Add(_search);
        var presets = new WrapPanel { Margin = new Thickness(0, 12, 0, 0) }; tools.Children.Add(presets);
        void Preset(string label, Func<Choice, bool> select) {
            var button = new Button { Content = label, ToolTip = "Replace the current selection with " + label.ToLowerInvariant() + " repairs. Use Preview selected to review before applying.", Padding = new Thickness(11, 6, 11, 6), FontSize = 12 };
            button.Click += (_, _) => { foreach (var pair in _checks) pair.Value.IsChecked = select(pair.Key); RefreshSelection(); };
            presets.Children.Add(button);
        }
        Preset("Recommended", c => c.Recommended); Preset("XNA Compatibility", c => c.Id is "Repair-Xna3" or "Repair-XnaColors" or "Repair-XnaSpriteBatch");
        Preset("Warnings + Ambiguities", c => c.Category == "Diagnostics"); Preset("Clear", _ => false);
        var options = new StackPanel { Margin = new Thickness(4, 8, 0, 8) };
        foreach (var box in new[] { _restore, _clean, _rids }) { box.Foreground = Foreground; box.Margin = new Thickness(0, 4, 0, 4); options.Children.Add(box); }
        _restore.ToolTip = "Restore NuGet packages when running SDK repairs.";
        _clean.ToolTip = "Remove intermediate build files for SDK repairs so they are regenerated.";
        _rids.ToolTip = "Set shared runtime identifiers when running SDK repairs.";
        var advanced = new Expander { Header = "Advanced SDK Options", ToolTip = "Build and package options used by the selected SDK repairs.", Content = options, Foreground = Brush("#A4B4CF"), Margin = new Thickness(0, 10, 0, 0) };
        DockPanel.SetDock(advanced, Dock.Bottom); content.Children.Add(advanced);
        var scroll = new ScrollViewer { Content = _cards, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled };
        content.Children.Add(scroll);
        foreach (var choice in Choices()) {
            var text = new StackPanel { Margin = new Thickness(10, 0, 4, 0) };
            text.Children.Add(new TextBlock { Text = choice.Category.ToUpperInvariant(), FontSize = 9, Foreground = Brush(choice.Category == "Diagnostics" ? "#68D9CB" : "#A89CF6"), Margin = new Thickness(0, 0, 0, 6) });
            text.Children.Add(new TextBlock { Text = choice.Title, FontSize = 12, FontWeight = FontWeights.SemiBold, TextWrapping = TextWrapping.Wrap, Foreground = Foreground });

            var check = new CheckBox { Content = text, Foreground = Foreground, Padding = new Thickness(2), VerticalContentAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Stretch };
            System.Windows.Automation.AutomationProperties.SetName(check, choice.Title);
            var row = new DockPanel(); var help = Help(choice.Title, choice.Description); DockPanel.SetDock(help, Dock.Right); row.Children.Add(help); row.Children.Add(check);
            check.ToolTip = new TextBlock { Text = choice.Description, TextWrapping = TextWrapping.Wrap, MaxWidth = 300 };
            var card = new Border { Child = row, Background = Brush("#141E32"), BorderBrush = Brush("#263650"), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(6), Padding = new Thickness(9), Margin = new Thickness(0, 0, 8, 7), VerticalAlignment = VerticalAlignment.Top };
            _checks[choice] = check; _cards.Children.Add(card); card.Tag = choice;
            void Changed() { card.BorderBrush = Brush(check.IsChecked == true ? "#56C8B6" : "#263650"); card.Background = Brush(check.IsChecked == true ? "#142D35" : "#141E32"); RefreshSelection(); }
            check.Checked += (_, _) => Changed(); check.Unchecked += (_, _) => Changed();
        }
        var footer = new Grid { Margin = new Thickness(0, 20, 0, 0) }; Grid.SetRow(footer, 2); shell.Children.Add(footer);
        footer.ColumnDefinitions.Add(new ColumnDefinition()); footer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        var note = new StackPanel(); footer.Children.Add(note); _selection.FontSize = 14; _selection.FontWeight = FontWeights.SemiBold; note.Children.Add(_selection);
        var repairHelp = Help("About Repairs", "Preview shows proposed changes without editing source. Apply saves a backup before making changes. Diagnostics that need a source-specific decision stay in the report."); repairHelp.Content = "About Repairs ?"; repairHelp.HorizontalAlignment = HorizontalAlignment.Left; repairHelp.Margin = new Thickness(0, 5, 0, 0); note.Children.Add(repairHelp);
        var actions = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center }; Grid.SetColumn(actions, 1); footer.Children.Add(actions);
        var close = new Button { Content = "Close" }; close.Click += (_, _) => DialogResult = false; actions.Children.Add(close);
        _preview.Content = "Preview Selected"; _preview.Click += (_, _) => Run(true); actions.Children.Add(_preview);
        _apply.Content = "Apply Repairs"; _apply.Background = Brush("#2D766D"); _apply.BorderBrush = Brush("#59BFAE"); _apply.Margin = new Thickness(0); _apply.Click += (_, _) => Run(false); actions.Children.Add(_apply);
        Filter(); RefreshSelection();
    }

    Button Help(string title, string description)
    {
        var button = new Button { Content = "?", Padding = new Thickness(5, 1, 5, 1), Margin = new Thickness(6, 0, 0, 0),
            FontSize = 11, VerticalAlignment = VerticalAlignment.Top, ToolTip = description };
        System.Windows.Automation.AutomationProperties.SetName(button, "Help: " + title);
        var popup = new ContextMenu { Background = Brush("#202C44"), Foreground = Foreground, BorderBrush = Brush("#34445F"), Padding = new Thickness(8) };
        var message = new StackPanel { MaxWidth = 280 };
        message.Children.Add(new TextBlock { Text = title, FontWeight = FontWeights.SemiBold, Margin = new Thickness(0, 0, 0, 6) });
        message.Children.Add(new TextBlock { Text = description, TextWrapping = TextWrapping.Wrap });
        popup.Items.Add(message);
        button.Click += (_, _) => { popup.PlacementTarget = button; popup.IsOpen = true; };
        return button;
    }
    void Run(bool preview) { PreviewOnly = preview; DialogResult = true; }
    void RefreshSelection() { int count = _checks.Count(p => p.Value.IsChecked == true); _selection.Text = count == 0 ? "Choose your repairs" : $"{count} repair{(count == 1 ? "" : "s")} selected"; _selection.ToolTip = string.Join("\n", _checks.Where(p => p.Value.IsChecked == true).Select(p => p.Key.Title)); _preview.IsEnabled = _apply.IsEnabled = count > 0; }
    void Filter() {
        string query = _search.Text.Trim();
        foreach (Border card in _cards.Children) { var c = (Choice)card.Tag; card.Visibility = (_category == "All Repairs" || c.Category == _category) && (query.Length == 0 || (c.Title + " " + c.Description + " " + c.Category).Contains(query, StringComparison.OrdinalIgnoreCase)) ? Visibility.Visible : Visibility.Collapsed; }
        foreach (var tab in _tabs) tab.Value.Background = Brush(tab.Key == _category ? "#263A54" : "#111A2C");
    }
    sealed class UniformGridHost : System.Windows.Controls.Primitives.UniformGrid { public UniformGridHost() { Columns = 2; } }

    static IEnumerable<Choice> Choices() => new[] {
        new Choice("Repair-Ambiguities", "Ambiguous Types", "Fix CS0104 for XNA Color and Farseer Path. Report CS0121 overloads for review. Python 3.10+.", "Diagnostics", Recommended: true),
        new Choice("Repair-Warnings", "Warning Fixer", "Fix unused literal locals, local self-assignments and unused event sender nullability. Other warnings stay in the review report.", "Diagnostics", Recommended: true),
        new Choice("Repair-DeepNullability", "Deep Nullability Analysis", "Trace CS8618 field assignments and references using the evaluated project. Preview compiler-checked private-field annotations; retain lifecycle and content-loading cases for review.", "Diagnostics"),
        new Choice("Repair-XnaColors", "Color Compatibility", "Replace old transparent constants and unsupported Color constructors.", "Rendering", Recommended: true),
        new Choice("Repair-XnaSpriteBatch", "Sprite Drawing", "Adapt legacy SpriteBatch calls for the current framework.", "Rendering"),
        new Choice("Repair-CronBloom", "Scene Capture", "Targeted back-buffer repairs for CRON, Avatar Insane Race 3D, and Jet Pack War. Python 3.10+.", "Rendering"),
        new Choice("Repair-XnaStorageSelector", "Storage Selector", "Convert legacy device-selector calls to the supported storage API.", "Source APIs"),
        new Choice("Repair-XnaOpenContainer", "Open Storage", "Adapt older save-container opening calls.", "Source APIs"),
        new Choice("Repair-XnaStorageSave", "Save Games", "Migrate supported container-based file creation patterns.", "Source APIs"),
        new Choice("Repair-XnaStorageLoad", "Load Games", "Migrate supported save-file reading patterns.", "Source APIs"),
        new Choice("Repair-XnaFileStream", "File Access", "Use supported stream APIs for known legacy file-access patterns.", "Source APIs"),
        new Choice("Repair-XnaSignedInGamerAvatar", "Gamer Avatars", "Update legacy signed-in gamer avatar API calls.", "Source APIs"),
        new Choice("Repair-EmbeddedResources", "Missing Strings", "Repair embedded string resources without replacing existing translations.", "Source APIs", Recommended: true),
        new Choice("Repair-Xna3", "XNA 3 → FNA", "Run the full source conversion pipeline. Broader changes; inspect the preview first. Python 3.10+.", "Source APIs"),
        new Choice("Repair-NetSdk1005", "Framework Assets · 1005", "Repair missing target-framework assets. Preview lists this action without running it.", "Project & SDK", true),
        new Choice("Repair-SharedProjectAssets", "Shared Build Folders", "Separate conflicting SDK and legacy assets. Preview lists this action without running it.", "Project & SDK", true),
        new Choice("Repair-SDK1047", "Runtime Assets · 1047", "Repair runtime-specific assets. Preview lists this action without running it.", "Project & SDK", true),
        new Choice("Repair-SDK1032", "Architecture · 1032", "Align project architecture settings. Preview lists this action without running it.", "Project & SDK", true),
        new Choice("Rename-DllProjects", "Disable Auxiliary Projects", "Rename auxiliary DLL project files. Preview lists this action without running it.", "Project & SDK", true),
        new Choice("Rename-DllProjects-Restore", "Restore Auxiliary Projects", "Restore previously disabled DLL project files. Preview lists this action without running it.", "Project & SDK", true),
        new Choice("Format-XbligProjects", "Format Project XML", "Format project XML with backups. Preview lists this action without running it.", "Project & SDK", true)
    };
}
