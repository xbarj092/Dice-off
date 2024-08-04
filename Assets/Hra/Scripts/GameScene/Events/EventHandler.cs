using System.Collections;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private int _turnsPerEventInvoke = 6;

    private IEventStrategy _strategy;

    private void Start()
    {
        ScheduleNextEvent();
        TryInvokeEvent();
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
        if (_strategy != null && _strategy is PredefinedDiceFallStrategy diceFallStrategy)
        {
            bool triggered = false;
            if (GameManager.Instance.Turns > 0)
            {
                TriggerEvent();
                triggered = true;
            }

            if (triggered)
            {
                StartCoroutine(DelayedDiceSelect(diceFallStrategy));
            }
            else
            {
                diceFallStrategy.SelectRandomDiceForNextRound();
            }
        }
        else if (GameManager.Instance.Turns > 0 && GameManager.Instance.Turns % _turnsPerEventInvoke == 0)
        {
            TriggerEvent();
        }
    }

    private IEnumerator DelayedDiceSelect(PredefinedDiceFallStrategy diceFallStrategy)
    {
        foreach (PlayerInput player in GameManager.Instance.Players)
        {
            player.CanPlay = false;
        }
        yield return new WaitForSeconds(2);
        diceFallStrategy.SelectRandomDiceForNextRound();
        foreach (PlayerInput player in GameManager.Instance.Players)
        {
            player.CanPlay = true;
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
