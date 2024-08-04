using UnityEngine;

public class EventContext
{
    private IEventStrategy _strategy;

    public EventContext(IEventStrategy strategy)
    {
        _strategy = strategy;
    }

    public void ExecuteStrategy()
    {
        _strategy.Execute();
    }
}
