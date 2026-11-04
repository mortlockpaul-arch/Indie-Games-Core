namespace IndieGames.BuildManager.Services;

public sealed class SingleInstanceHost : IDisposable
{
    public const string ActivationEventName = @"Local\IndieGames.BuildManager.Activate";
    public const string SeedEventName = @"Local\IndieGames.BuildManager.StartSeeding";
    public const string StopSeedEventName = @"Local\IndieGames.BuildManager.StopSeeding";
    private readonly List<EventWaitHandle> _extraEvents = [];
    private readonly List<RegisteredWaitHandle> _extraRegistrations = [];
    private Mutex? _mutex;
    private EventWaitHandle? _activation;
    private RegisteredWaitHandle? _registration;
    private bool _ownsMutex;

    public bool TryStart(Action activate, bool seedOnly=false, Action? startSeeding=null, Action? stopSeeding=null)
    {
        _mutex = new Mutex(true, @"Local\IndieGames.BuildManager.Instance", out bool first);
        if (!first)
        {
            // The owner may still be creating its activation event.
            for (int attempt = 0; attempt < 20; attempt++)
            {
                try
                {
                    using var existing = EventWaitHandle.OpenExisting(seedOnly ? SeedEventName : ActivationEventName);
                    existing.Set();
                    break;
                }
                catch (WaitHandleCannotBeOpenedException) { Thread.Sleep(50); }
            }
            return false;
        }
        _ownsMutex = true;
        Register(SeedEventName, startSeeding ?? (()=>{}));
        Register(StopSeedEventName, stopSeeding ?? (()=>{}));
        _activation = new EventWaitHandle(false, EventResetMode.AutoReset, ActivationEventName);
        _registration = ThreadPool.RegisterWaitForSingleObject(_activation, (_, _) => activate(), null, Timeout.Infinite, false);
        return true;
    }

    private void Register(string name, Action action)
    {
        var handle=new EventWaitHandle(false,EventResetMode.AutoReset,name);_extraEvents.Add(handle);
        _extraRegistrations.Add(ThreadPool.RegisterWaitForSingleObject(handle,(_,_)=>action(),null,Timeout.Infinite,false));
    }

    public void Dispose()
    {
        _registration?.Unregister(null);
        foreach(var registration in _extraRegistrations)registration.Unregister(null);
        foreach(var handle in _extraEvents)handle.Dispose();
        _activation?.Dispose();
        if (_ownsMutex) { _mutex?.ReleaseMutex(); _ownsMutex = false; }
        _mutex?.Dispose();
    }
}
