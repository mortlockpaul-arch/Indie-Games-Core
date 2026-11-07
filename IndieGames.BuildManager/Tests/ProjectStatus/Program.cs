using System.Text.Json;
using IndieGames.BuildManager.Models;
using IndieGames.BuildManager.Services;

string root = Path.Combine(Path.GetTempPath(), "project-status-tests-" + Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(root);
var errors = new List<string>();
var store = new ProjectStatusStore(root, errors.Add);
GameProject Project(string folder = "one") => new() { Name = "Game", Path = Path.Combine(root, folder, "Game.csproj") };
void Require(bool condition, string message) { if (!condition) throw new Exception(message); }
var original = Project(); store.Track(original);
Require(original.Status == "No build or run recorded", "Missing history must not imply a build failed.");
original.Status = "Build succeeded (Release / x64 / NetStub); 1 warning(s)";
var restored = Project(); new ProjectStatusStore(root, errors.Add).Track(restored);
Require(restored.Status == original.Status && restored.StatusUpdatedAt == original.StatusUpdatedAt, "Status and timestamp must survive a new store.");
var other = Project("two"); store.Track(other);
Require(other.StatusUpdatedAt is null, "Same project name in another folder must have separate history.");
var rescanned = Project(); store.Track(rescanned);
original.Status = "Game exited with error -1";
Require(rescanned.Status == original.Status, "Completion must update rows recreated by a scan.");
string file = Directory.GetFiles(root, "*.json").Single();
File.WriteAllText(file, JsonSerializer.Serialize(new ProjectStatusStore.Entry(original.Path, "In progress: Build (Release / x64)", original.StatusUpdatedAt, -1)));
var interrupted = Project(); new ProjectStatusStore(root, errors.Add).Track(interrupted);
Require(interrupted.Status.StartsWith("Previous session ended before completion:"), "A previous session must not appear to still be building.");
File.WriteAllText(file, "{broken");
var corrupt = Project(); new ProjectStatusStore(root, errors.Add).Track(corrupt);
Require(corrupt.StatusUpdatedAt is null && errors.Count == 1, "Corrupt history should be reported without stopping the scan.");
Console.WriteLine("PASS: persistence, timestamps, path isolation, live rescan updates, interrupted operations, corrupt history");
Directory.Delete(root, true);
