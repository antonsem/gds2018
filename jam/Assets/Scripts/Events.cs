using UnityEngine.Events;

public class IUpdateEventEmmiter : UnityEvent<IUpdate>
{ }

public class EventEmmiter : UnityEvent
{ }

public class DeathEventEmmiter : UnityEvent<DeathType>
{ }

public class StringEventEmmiter : UnityEvent<string>
{ }

public class LevelStateEventEmmiter : UnityEvent<LevelStateController>
{ }

public class Events : Singleton<Events>
{
    public IUpdateEventEmmiter registerUpdate = new IUpdateEventEmmiter();
    public IUpdateEventEmmiter unregisterUpdate = new IUpdateEventEmmiter();
    public DeathEventEmmiter playerDied = new DeathEventEmmiter();
    public LevelStateEventEmmiter levelLoaded = new LevelStateEventEmmiter();
    public EventEmmiter levelFailed = new EventEmmiter();
    public EventEmmiter levelCompleted = new EventEmmiter();
    public EventEmmiter shakeCamera = new EventEmmiter();
}
