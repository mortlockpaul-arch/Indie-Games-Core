using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using IndieGames.BuildManager.Models;
using IndieGames.BuildManager.Services;
using System.Diagnostics;
using System.Windows.Media;
namespace IndieGames.BuildManager;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<GameProject> _projects = [];
    private CancellationTokenSource? _cts;
    public MainWindow()
    {
        InitializeComponent();
        ProjectsGrid.ItemsSource = _projects;
        Scan();
    }
    private void Log(string line) => Dispatcher.Invoke(() => { LogBox.AppendText(line + Environment.NewLine); LogBox.ScrollToEnd(); });
    private void Scan()
    {
        _projects.Clear();
        try { foreach (var p in ProjectScanner.Scan(RootBox.Text)) _projects.Add(p); Log($"Found {_projects.Count} projects."); }
        catch (Exception ex) { Log($"Scan failed: {ex.Message}"); }
    }
    private void Scan_Click(object sender, RoutedEventArgs e) => Scan();

    private async void InstallGameLoggerButton_Click( object sender, RoutedEventArgs e)
    {
        string script = Path.Combine(
            AppContext.BaseDirectory,
            "scripts",
            "Install-GameLogger.ps1"
        );
        // C:\PycharmProjects\xenia-game-manager\src\scripts\Install-GameLogger.ps1
        if (!File.Exists(script))
        {
            MessageBox.Show($"Script not found:\n{script}");
            return;
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments =
                $"-NoProfile -ExecutionPolicy Bypass -File \"{script}\" " +
                "-Root \"C:\\source\\Indie-Games\"",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using var process = new Process { StartInfo = startInfo };

        process.Start();

        Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
        Task<string> errorTask = process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        string output = await outputTask;
        string errors = await errorTask;

        MessageBox.Show(
            output + Environment.NewLine + errors,
            process.ExitCode == 0
                ? "GameLogger Installed"
                : "GameLogger Installation Failed"
        );
    }

    private void RunGameButton_Click(object sender, RoutedEventArgs e)
    {
        var project = Selected().FirstOrDefault();
        if (project is null)
        {
            MessageBox.Show("Please select a game first.", "Run Game",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        string? gameDirectory = Path.GetDirectoryName(project.Path);
        if (string.IsNullOrWhiteSpace(gameDirectory) || !Directory.Exists(gameDirectory))
        {
            MessageBox.Show($"Project directory not found: {gameDirectory}", "Run Game",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        string binDirectory = Path.Combine(gameDirectory, "bin");
        if (!Directory.Exists(binDirectory))
        {
            MessageBox.Show($"No build output found in:\n{binDirectory}\nBuild the game first.",
                "Run Game", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            string configuration = Value(ConfigBox);
            string platform = Value(PlatformBox);
            string runtime = RuntimeBox.Text.Trim();
            string framework = FrameworkBox.Text.Trim();
            string projectName = Path.GetFileNameWithoutExtension(project.Path);
            string? assemblyName = null;

            // AssemblyName can differ from the project filename (e.g. spaces in XBLIG names).
            // Reading the simple property from the project avoids launching an unrelated tool.
            var doc = System.Xml.Linq.XDocument.Load(project.Path);
            assemblyName = doc.Descendants()
                .FirstOrDefault(e => e.Name.LocalName == "AssemblyName" &&
                                     !e.Value.Contains("$(", StringComparison.Ordinal))?.Value.Trim();
            if (string.IsNullOrWhiteSpace(assemblyName)) assemblyName = projectName;

            var executables = Directory.EnumerateFiles(binDirectory, "*.exe", SearchOption.AllDirectories)
                .Where(path => !path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    .Any(part => part.Equals("ref", StringComparison.OrdinalIgnoreCase) ||
                                 part.Equals("obj", StringComparison.OrdinalIgnoreCase)))
                .Select(path => new
                {
                    Path = path,
                    Name = Path.GetFileNameWithoutExtension(path),
                    Parts = Path.GetRelativePath(binDirectory, path)
                        .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
                    Modified = File.GetLastWriteTimeUtc(path)
                })
                .Select(item => new
                {
                    item.Path,
                    item.Modified,
                    Score =
                        (item.Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase) ? 1000 :
                         item.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase) ? 900 : 0) +
                        (item.Parts.Any(part => part.Equals(configuration, StringComparison.OrdinalIgnoreCase)) ? 100 : 0) +
                        (item.Parts.Any(part => part.Equals(platform, StringComparison.OrdinalIgnoreCase)) ? 50 : 0) +
                        (string.IsNullOrWhiteSpace(runtime) || item.Parts.Any(part => part.Equals(runtime, StringComparison.OrdinalIgnoreCase)) ? 25 : 0) +
                        (string.IsNullOrWhiteSpace(framework) || item.Parts.Any(part => part.Equals(framework, StringComparison.OrdinalIgnoreCase)) ? 25 : 0) -
                        (item.Parts.Any(part => part.Equals("x86", StringComparison.OrdinalIgnoreCase) ||
                                                part.Equals("win-x86", StringComparison.OrdinalIgnoreCase)) &&
                         platform.Equals("x64", StringComparison.OrdinalIgnoreCase) ? 200 : 0) -
                        (item.Parts.Any(part => part.Equals("x64", StringComparison.OrdinalIgnoreCase) ||
                                                part.Equals("win-x64", StringComparison.OrdinalIgnoreCase)) &&
                         platform.Equals("x86", StringComparison.OrdinalIgnoreCase) ? 200 : 0)
                })
                .Where(item => item.Score >= 900)
                .OrderByDescending(item => item.Score)
                .ThenByDescending(item => item.Modified)
                .ToList();

            if (executables.Count == 0)
            {
                Log($"No executable matching {assemblyName}.exe found under {binDirectory}");
                MessageBox.Show($"No executable for '{project.Name}' found in:\n{binDirectory}\n\nBuild the game first, or check its AssemblyName and output directory.",
                    "Run Game", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string executable = executables[0].Path;
            Log($"=== Running {project.Name} ===");
            Log($"Executable: {executable}");
            Process.Start(new ProcessStartInfo
            {
                FileName = executable,
                WorkingDirectory = Path.GetDirectoryName(executable)!,
                UseShellExecute = false
            });
        }
        catch (Exception ex)
        {
            Log($"Game launch failed: {ex}");
            MessageBox.Show(ex.ToString(), "Game Launch Failed",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Browse_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog { Title = "Choose indie-game-archive", InitialDirectory = Directory.Exists(RootBox.Text) ? RootBox.Text : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) };
        if (dialog.ShowDialog() == true) { RootBox.Text = dialog.FolderName; Scan(); }
    }
    private static string Value(ComboBox box) => box.SelectedItem is ComboBoxItem item ? item.Content.ToString() ?? "" : box.Text.Trim();
    private List<GameProject> Selected() => ProjectsGrid.SelectedItems.Cast<GameProject>().ToList();
    private async Task ExecuteAsync(Func<CancellationToken, Task> job)
    {
        if (_cts != null) { Log("Another operation is already running."); return; }
        _cts = new CancellationTokenSource();
        try { await job(_cts.Token); }
        catch (OperationCanceledException) { Log("Cancelled."); }
        catch (Exception ex) { Log($"ERROR: {ex.Message}"); }
        finally { _cts.Dispose(); _cts = null; }
    }
    private void Cancel_Click(object sender, RoutedEventArgs e) => _cts?.Cancel();
    private async void Inspect_Click(object sender, RoutedEventArgs e)
    {
        var project = Selected().FirstOrDefault();
        if (project is null) { Log("Select a project first."); return; }
        await ExecuteAsync(async token =>
        {
            Log($"Inspecting {project.Name}");
            await ProcessRunner.RunAsync("dotnet", ["msbuild", project.Path,
                "-getProperty:TargetFramework,TargetFrameworks,Configuration,Platform,PlatformTarget,Platforms,RuntimeIdentifier,RuntimeIdentifiers,OutputPath,BaseIntermediateOutputPath,ProjectAssetsFile"],
                Path.GetDirectoryName(project.Path)!, Log, token);
        });
    }
    private async void Detect_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more projects first."); return; }
        string exe = UseVsBox.IsChecked == true ? MsbuildPathBox.Text.Trim() : "dotnet";
        if (UseVsBox.IsChecked == true && !File.Exists(exe)) { Log("Set a valid MSBuild.exe path."); return; }
        string config = Value(ConfigBox), platform = Value(PlatformBox), target = Value(TargetBox);
        string framework = FrameworkBox.Text.Trim(), runtime = RuntimeBox.Text.Trim();
        await ExecuteAsync(async token =>
        {
            foreach (var project in selected)
            {
                token.ThrowIfCancellationRequested();
                Log($"=== Checking {project.Name} [{config}|{platform}] ===");
                try
                {
                    var report = await ConfigurationDiagnostics.InspectAsync(project, config, platform,
                        target, framework, runtime, exe, Log, token);
                    foreach (var issue in report.Issues)
                        Log($"[{issue.Severity}] {issue.Code}: {issue.Detail}\\n  Suggested action: {issue.Recommendation}");
                    project.Status = report.Issues.Any(i => i.Severity == "Error") ? "Issues found" :
                        report.Issues.Any(i => i.Severity == "Warning") ? "Warnings" : "Checked";
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex) { project.Status = "Check failed"; Log($"ERROR: {ex.Message}"); }
            }
        });
    }
    private async void Build_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more projects."); return; }
        var action = Value(ActionBox).ToLowerInvariant();
        await ExecuteAsync(async token =>
        {
            foreach (var project in selected)
            {
                token.ThrowIfCancellationRequested();
                project.Status = "Running";
                var args = new List<string>();
                bool vs = UseVsBox.IsChecked == true;
                string exe = vs ? MsbuildPathBox.Text.Trim() : "dotnet";
                if (vs && !File.Exists(exe)) throw new FileNotFoundException("Specify an existing MSBuild.exe path.", exe);
                if (!vs) args.Add(action == "restore" ? "restore" : "build");
                args.Add(project.Path);
                if (vs) args.Add(action switch { "rebuild" => "-t:Rebuild", "clean" => "-t:Clean", "restore" => "-t:Restore", _ => "-t:Build" });
                else if (action == "rebuild") args.Add("-t:Rebuild");
                else if (action == "clean") args.Add("-t:Clean");
                if (action != "restore")
                {
                    args.Add($"-p:Configuration={Value(ConfigBox)}");
                    args.Add($"-p:Platform={Value(PlatformBox)}");
                    args.Add($"-p:PlatformTarget={Value(TargetBox)}");
                    string networkBackend = Value(NetworkBackendBox);

                    args.Add($"-p:NetworkBackend={networkBackend}");
                    if (!string.IsNullOrWhiteSpace(FrameworkBox.Text)) args.Add($"-p:TargetFramework={FrameworkBox.Text.Trim()}");
                    if (!string.IsNullOrWhiteSpace(RuntimeBox.Text)) args.Add($"-p:RuntimeIdentifier={RuntimeBox.Text.Trim()}");
                    if (!string.IsNullOrWhiteSpace(DefineConstantsBox.Text))
                    {
                        // MSBuild -p treats semicolons as property separators.
                        // Escape them as %3B to preserve the full DefineConstants value.
                        string symbols = DefineConstantsBox.Text.Trim().Replace(";", "%3B");
                        args.Add($"-p:DefineConstants={symbols}");
                        Log("Note: DefineConstants overrides the project's evaluated symbols for this build.");
                    }
                    if (!vs && NoRestoreBox.IsChecked == true) args.Add("--no-restore");
                }
                Log($"> {exe} {string.Join(" ", args)}");
                try
                {
                    int code = await ProcessRunner.RunAsync(exe, args, Path.GetDirectoryName(project.Path)!, Log, token);
                    project.Status = code == 0 ? "Succeeded" : $"Failed ({code})";
                }
                catch { project.Status = "Failed"; throw; }
            }
        });
    }
    private void InjectStartup_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more projects."); return; }
        if (MessageBox.Show($"Inject startup diagnostics into {selected.Count} selected project(s)? Source backups will be created.",
            "Confirm source changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
        foreach (var project in selected)
        {
            try { Log($"{project.Name}: {StartupDiagnosticsInjector.Inject(project.Path)}"); }
            catch (Exception ex) { Log($"{project.Name}: {ex.Message}"); }
        }
    }

    private sealed class RepairChoice
    {
        public string Name { get; init; } = "";
        public string Folder { get; init; } = "";
        public bool Apply { get; set; }
    }

    private async void BatchXnaFixes_Click(object sender, RoutedEventArgs e)
    {
        string archive = RootBox.Text.Trim();
        if (!Directory.Exists(archive)) { Log($"Archive not found: {archive}"); return; }
        string script = @"C:\PycharmProjects\xenia-game-manager\src\scripts\XnaRepairFunctions.ps1";
        var games = Directory.EnumerateDirectories(archive)
            .Where(folder => Directory.EnumerateFiles(folder, "*.csproj", SearchOption.AllDirectories)
                .Any(file => !file.Split(Path.DirectorySeparatorChar).Any(part => part is "bin" or "obj")))
            .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase)
            .Select(folder => new RepairChoice { Name = Path.GetFileName(folder), Folder = folder })
            .ToList();
        if (games.Count == 0) { Log("No game folders containing projects found."); return; }

        var dialog = new Window {
            Title = "Batch XNA + SDK repairs", Width = 980, Height = 760,
            MinWidth = 650, MinHeight = 450, Owner = this,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(17,24,39)),
            Foreground = Brushes.White
        };
        var layout = new DockPanel { Margin = new Thickness(14) };
        dialog.Content = layout;
        var top = new StackPanel(); DockPanel.SetDock(top, Dock.Top); layout.Children.Add(top);
        top.Children.Add(new TextBlock { Text = "Choose games and XNA / SDK repairs (all games start unchecked)", FontSize = 17, Margin = new Thickness(0,0,0,10) });
        var tools = new WrapPanel(); top.Children.Add(tools);
        var all = new Button { Content = "Select all", Padding = new Thickness(10,5,10,5), Margin = new Thickness(0,0,8,8) };
        var none = new Button { Content = "Select none", Padding = new Thickness(10,5,10,5), Margin = new Thickness(0,0,8,8) };
        tools.Children.Add(all); tools.Children.Add(none);
        var fixers = new[] {
            "Repair-XnaStorageSelector", "Repair-XnaOpenContainer", "Repair-XnaStorageSave",
            "Repair-XnaStorageLoad", "Repair-XnaFileStream", "Repair-XnaColors",
            "Repair-XnaSignedInGamerAvatar"
        };
        top.Children.Add(new TextBlock { Text = "Fixers (select only those you want):", Margin = new Thickness(0,8,0,4) });
        var fixerPanel = new WrapPanel(); top.Children.Add(fixerPanel);
        var fixerChecks = fixers.Select(name => new CheckBox {
            Content = name, IsChecked = name == "Repair-XnaColors",
            Margin = new Thickness(0,0,16,7), Foreground = Brushes.White
        }).ToList();
        foreach (var check in fixerChecks) fixerPanel.Children.Add(check);
        var sdkFixers = new[] {
            ("NETSDK1005 (clean obj/bin + restore)", "Repair-NetSdk1005"),
            ("Shared project assets (unique obj paths)", "Repair-SharedProjectAssets"),
            ("SDK1047 (restore, build and verify assets)", "Repair-SDK1047"),
            ("SDK1032 (architecture mappings)", "Repair-SDK1032"),
            ("Disable auxiliary DLL projects", "Rename-DllProjects"),
            ("Restore disabled DLL projects", "Rename-DllProjects-Restore"),
            ("Format project XML (with backups)", "Format-XbligProjects")
        };
        top.Children.Add(new TextBlock { Text = "SDK / project fixes:", Margin = new Thickness(0,8,0,4) });
        var sdkPanel = new WrapPanel(); top.Children.Add(sdkPanel);
        var sdkChecks = sdkFixers.Select(f => new CheckBox {
            Content = f.Item1, Tag = f.Item2, IsChecked = false,
            Margin = new Thickness(0,0,16,7), Foreground = Brushes.White
        }).ToList();
        foreach (var check in sdkChecks) sdkPanel.Children.Add(check);
        var sdkRestore = new CheckBox { Content = "Restore packages (shared assets / SDK1032)",
            IsChecked = true, Foreground = Brushes.White, Margin = new Thickness(0,3,12,3) };
        var sdkClean = new CheckBox { Content = "Clean intermediates (shared assets / SDK1047)",
            IsChecked = false, Foreground = Brushes.White, Margin = new Thickness(0,3,12,3) };
        var sdkRids = new CheckBox { Content = "Set RuntimeIdentifiers on shared projects",
            IsChecked = true, Foreground = Brushes.White, Margin = new Thickness(0,3,12,3) };
        top.Children.Add(sdkRestore); top.Children.Add(sdkClean); top.Children.Add(sdkRids);
        var preview = new CheckBox { Content = "Preview only (-WhatIf)", IsChecked = true,
            Foreground = Brushes.White, Margin = new Thickness(0,6,0,8) };
        top.Children.Add(preview);
        var bottom = new StackPanel(); DockPanel.SetDock(bottom, Dock.Bottom); layout.Children.Add(bottom);
        var count = new TextBlock { Margin = new Thickness(0,6,0,6), Foreground = Brushes.White };
        bottom.Children.Add(count);
        var actions = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
        bottom.Children.Add(actions);
        var cancel = new Button { Content = "Cancel", Padding = new Thickness(15,6,15,6), Margin = new Thickness(5) };
        var run = new Button { Content = "Run selected repairs", Padding = new Thickness(15,6,15,6), Margin = new Thickness(5) };
        actions.Children.Add(cancel); actions.Children.Add(run);
        var grid = new DataGrid { AutoGenerateColumns = false, CanUserAddRows = false,
            ItemsSource = games, Background = Brushes.White, Foreground = Brushes.Black };
        grid.Columns.Add(new DataGridCheckBoxColumn { Header = "Yes / No", Binding = new System.Windows.Data.Binding(nameof(RepairChoice.Apply)) { Mode = System.Windows.Data.BindingMode.TwoWay, UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged }, Width = 90 });
        grid.Columns.Add(new DataGridTextColumn { Header = "Game", Binding = new System.Windows.Data.Binding(nameof(RepairChoice.Name)), Width = new DataGridLength(1, DataGridLengthUnitType.Star), IsReadOnly = true });
        layout.Children.Add(grid);
        void UpdateCount() => count.Text = $"Selected: {games.Count(g => g.Apply)} / {games.Count}";
        all.Click += (_, _) => { foreach (var game in games) game.Apply = true; grid.Items.Refresh(); UpdateCount(); };
        none.Click += (_, _) => { foreach (var game in games) game.Apply = false; grid.Items.Refresh(); UpdateCount(); };
        grid.CurrentCellChanged += (_, _) => UpdateCount();
        cancel.Click += (_, _) => { dialog.DialogResult = false; };
        run.Click += (_, _) => {
            grid.CommitEdit(DataGridEditingUnit.Cell, true);
            grid.CommitEdit(DataGridEditingUnit.Row, true);
            UpdateCount();
            if (!games.Any(g => g.Apply) || !(fixerChecks.Any(c => c.IsChecked == true) || sdkChecks.Any(c => c.IsChecked == true))) {
                MessageBox.Show(dialog, "Select at least one game and one repair."); return;
            }
            if (preview.IsChecked != true && MessageBox.Show(dialog,
                $"Modify source files for {games.Count(g => g.Apply)} games? Backups will be created.",
                "Confirm source modifications", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            dialog.DialogResult = true;
        };
        UpdateCount();
        if (dialog.ShowDialog() != true) return;
        var selectedGames = games.Where(g => g.Apply).ToList();
        var selectedFixers = fixerChecks.Where(c => c.IsChecked == true).Select(c => (string)c.Content).ToArray();
        if (selectedFixers.Length > 0 && !File.Exists(script)) {
            var chooser = new OpenFileDialog { Title = "Choose XnaRepairFunctions.ps1",
                Filter = "PowerShell (*.ps1)|*.ps1" };
            if (chooser.ShowDialog(this) != true) return;
            script = chooser.FileName;
        }
        var selectedSdkFixers = sdkChecks.Where(c => c.IsChecked == true)
            .Select(c => (string)c.Tag).ToArray();
        bool sdkDoRestore = sdkRestore.IsChecked == true;
        bool sdkDoClean = sdkClean.IsChecked == true;
        bool sdkFixRids = sdkRids.IsChecked == true;
        string sdkScript = Path.Combine(AppContext.BaseDirectory, "scripts", "BuildSdkRepairs.ps1");
        if (selectedSdkFixers.Length > 0 && !File.Exists(sdkScript)) {
            var chooser = new OpenFileDialog { Title = "Choose BuildSdkRepairs.ps1",
                Filter = "PowerShell (*.ps1)|*.ps1" };
            if (chooser.ShowDialog(this) != true) return;
            sdkScript = chooser.FileName;
        }
        string sdkFramework = string.IsNullOrWhiteSpace(FrameworkBox.Text)
            ? "net9.0-windows" : FrameworkBox.Text.Trim();
        string sdkPlatform = Value(PlatformBox);
        if (sdkPlatform is not ("x64" or "x86")) sdkPlatform = "x64";
        bool whatIf = preview.IsChecked == true;
        await ExecuteAsync(async token => {
            int succeeded = 0, failed = 0;
            foreach (var game in selectedGames) {
                token.ThrowIfCancellationRequested();
                Log($"=== {(whatIf ? "PREVIEW" : "REPAIR")}: {game.Name} ===");
                // Run a wrapper using -Command so the function-only .ps1 can be dot-sourced.
                // EncodedCommand avoids escaping game names with quotes/apostrophes.
                string quote(string value) => "'" + value.Replace("'", "''") + "'";
                string commands = "$ErrorActionPreference='Stop'; " +
                    "if (" + (selectedFixers.Length > 0 ? "$true" : "$false") + ") { . " + quote(script) + "; " +
                    "foreach($fix in @(" + string.Join(",", selectedFixers.Select(quote)) + ")) { " +
                    "if(-not (Get-Command $fix -CommandType Function -ErrorAction SilentlyContinue)) { throw \"Missing fixer: $fix\" }; " +
                    "& $fix -Root " + quote(game.Folder) + (whatIf ? " -WhatIf" : "") +
                    " -ErrorAction Stop; if(-not $?) { throw \"Fixer failed: $fix\" } } }; ";
                if (selectedSdkFixers.Length > 0) {
                    if (whatIf) {
                        commands += "Write-Host 'PREVIEW: SDK fixes are listed but NOT executed (they do not support -WhatIf).'; " +
                            "Write-Host " + quote("SDK: " + string.Join(", ", selectedSdkFixers)) + "; ";
                    } else {
                        commands += ". " + quote(sdkScript) + "; " +
                            "$game=" + quote(game.Folder) + "; " +
                            "$framework=" + quote(sdkFramework) + "; " +
                            "$platform=" + quote(sdkPlatform) + "; ";
                        foreach (string fix in selectedSdkFixers) {
                            commands += fix switch {
                                "Repair-NetSdk1005" =>
                                    "Repair-NetSdk1005 -Root " + quote(archive) +
                                    " -ProjectName " + quote(game.Name) + " -Framework $framework; ",
                                "Repair-SharedProjectAssets" =>
                                    "Repair-SharedProjectAssets -Game $game" +
                                    (sdkDoRestore ? " -Restore" : "") +
                                    (sdkDoClean ? " -Clean" : "") +
                                    (sdkFixRids ? " -FixRuntimeIdentifiers" : "") + "; ",
                                "Repair-SDK1047" =>
                                    "Repair-SDK1047 -Game $game -Framework $framework -Platform $platform" +
                                    (sdkDoClean ? " -Clean" : "") + "; ",
                                "Repair-SDK1032" =>
                                    "Repair-SDK1032 -Game $game -Framework $framework -Platform $platform" +
                                    (sdkDoRestore ? " -Restore" : "") + "; ",
                                "Rename-DllProjects" => "Rename-DllProjects -Game $game; ",
                                "Rename-DllProjects-Restore" => "Rename-DllProjects -Game $game -Restore; ",
                                "Format-XbligProjects" => "Format-XbligProjects -Game $game; ",
                                _ => ""
                            };
                            commands += "if(-not $?) { throw 'SDK fixer failed' }; ";
                        }
                    }
                }
                string encoded = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(commands));
                try {
                    int exit = await ProcessRunner.RunAsync("powershell.exe",
                        ["-NoProfile", "-NonInteractive", "-ExecutionPolicy", "Bypass", "-EncodedCommand", encoded],
                        game.Folder, Log, token);
                    if (exit == 0) succeeded++; else { failed++; Log($"FAILED: {game.Name} (exit {exit})"); }
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex) { failed++; Log($"FAILED: {game.Name}: {ex.Message}"); }
            }
            Log($"Batch complete. Successful: {succeeded}; failed: {failed}; preview: {whatIf}.");
        });
    }

    private async void Fixer_Click(object sender, RoutedEventArgs e)
    {
        var project = Selected().FirstOrDefault();
        if (project is null) { Log("Select a project first."); return; }
        if (!File.Exists(ScriptBox.Text)) { Log("PowerShell script not found."); return; }
        var confirm = MessageBox.Show($"Run this script?\\n{ScriptBox.Text}\\n\\nProject: {project.Name}\\n\\nReview the script parameters first. The initial integration passes no project parameters.", "Confirm fixer", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (confirm != MessageBoxResult.Yes) return;
        await ExecuteAsync(async token =>
        {
            var code = await ProcessRunner.RunAsync("powershell.exe",
                ["-NoProfile", "-ExecutionPolicy", "Bypass", "-File", ScriptBox.Text],
                Path.GetDirectoryName(project.Path)!, Log, token);
            Log($"Fixer exit code: {code}");
        });
    }
}
