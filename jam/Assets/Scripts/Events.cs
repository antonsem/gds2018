using UnityEngine.Events;

public class IUpdateEventEmmiter : UnityEvent<IUpdate>
{ }

public class EventEmmiter : UnityEvent
{ }

public class Events : Singleton<Events>
{
    public IUpdateEventEmmiter registerUpdate = new IUpdateEventEmmiter();
    public IUpdateEventEmmiter unregisterUpdate = new IUpdateEventEmmiter();
    public EventEmmiter playerDied = new EventEmmiter();
}
