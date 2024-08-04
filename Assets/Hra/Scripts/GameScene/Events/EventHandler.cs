using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private int _turnsPerEventInvoke = 6;

    private IEventStrategy _strategy;

    private void Start()
    {
        ScheduleNextEvent();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnTurnFinished += TryInvokeEvent;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTurnFinished -= TryInvokeEvent;
    }

    private void TryInvokeEvent()
    {
        if (GameManager.Instance.Turns % _turnsPerEventInvoke == 0)
        {
            TriggerEvent();
        }
    }

    public void ScheduleNextEvent()
    {
        int eventTypeIndex = Random.Range(0, 3);
        EventType eventType = (EventType)eventTypeIndex;
        _strategy = GetStrategy(eventType);
        if (_strategy != null)
        {
            GameEvents.OnEventTypeChangedInvoke(eventType);
        }
    }

    private void TriggerEvent()
    {
        EventContext context = new(_strategy);
        context.ExecuteStrategy();
    }

    private IEventStrategy GetStrategy(EventType eventType)
    {
        int randomAmount = Random.Range(1, 4);
        return eventType switch
        {
            EventType.LowerAllDicesValue => new LowerAllDicesValueStrategy(),
            EventType.LowerRandomDicesValue => new LowerRandomDicesValueStrategy(randomAmount),
            EventType.PredefinedDiceFall => new PredefinedDiceFallStrategy(),
            _ => null,
        };
    }
}
