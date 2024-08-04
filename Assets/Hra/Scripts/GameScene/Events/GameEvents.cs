using System;

public static class GameEvents
{
    public static event Action<EventType> OnEventTypeChanged;
    public static void OnEventTypeChangedInvoke(EventType eventType)
    {
        OnEventTypeChanged?.Invoke(eventType);
    }
}
