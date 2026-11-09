using System.IO;
using System.Windows;
using System.Windows.Controls;
using IndieGames.BuildManager.Services;

namespace IndieGames.BuildManager;

public partial class MainWindow
{
    private void ZxBoxBackend_Changed(object sender, SelectionChangedEventArgs e) => UpdateZxBoxStatus();

    private void UpdateZxBoxStatus()
    {
        if (ZxBoxStatus == null || ZxBoxBackendBox == null) return;
        try {
            var plan = ZxBoxLauncher.Plan(_storage, Value(ZxBoxBackendBox));
            ZxBoxStatus.Text = File.Exists(plan.Executable) ? "Ready · " + plan.Backend + " · Spectrum 48K" : "Build on first run · " + plan.Backend;
        }
        catch (Exception error) { ZxBoxStatus.Text = error.Message; }
    }

    private async void ZxBoxRun_Click(object sender, RoutedEventArgs e) => await RunZxBoxAsync(false);
    private async void ZxBoxBuild_Click(object sender, RoutedEventArgs e) => await RunZxBoxAsync(true);

    private async Task RunZxBoxAsync(bool build)
    {
        if (_cts != null) { Log("Finish the current operation before starting ZXBox."); return; }
        string backend = Value(ZxBoxBackendBox);
        await ExecuteAsync(async token => {
            ZxBoxRunButton.IsEnabled = ZxBoxBuildButton.IsEnabled = ZxBoxBackendBox.IsEnabled = false;
            try {
                var plan = ZxBoxLauncher.Plan(_storage, backend);
                if (build || !File.Exists(plan.Executable)) {
                    OutputTabs.SelectedIndex = 0;
                    ZxBoxStatus.Text = "Building ZXBox · " + backend;
                    Log($"Building ZXBox {backend} in x64 Release. Output: {plan.Output}");
                    int code = await ProcessRunner.RunAsync("dotnet", plan.BuildArguments,
                        Path.GetDirectoryName(plan.Project)!, Log, token);
                    if (code != 0) throw new IOException($"ZXBox {backend} build failed (exit {code}). See the build log.");
                }
                token.ThrowIfCancellationRequested();
                using var process = ZxBoxLauncher.Start(plan);
                ZxBoxStatus.Text = "Launched · " + backend;
                Log($"Started ZXBox {backend} (process {process.Id}). Games: {plan.GamesFolder}. F1 / Escape opens its menu.");
            }
            catch (OperationCanceledException) { ZxBoxStatus.Text = "ZXBox build cancelled."; throw; }
            catch (Exception error) { ZxBoxStatus.Text = error.Message; throw; }
            finally { ZxBoxRunButton.IsEnabled = ZxBoxBuildButton.IsEnabled = ZxBoxBackendBox.IsEnabled = true; }
        });
    }

    private void ZxBoxGames_Click(object sender, RoutedEventArgs e)
    {
        try {
            var plan = ZxBoxLauncher.Plan(_storage, Value(ZxBoxBackendBox));
            Directory.CreateDirectory(plan.GamesFolder);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(plan.GamesFolder) { UseShellExecute = true });
        }
        catch (Exception error) { Log("ZXBox: " + error.Message); }
    }
}
