using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScreen : MonoBehaviour
{
    [SerializeField] private EventUI _event;
    [SerializeField] private List<PlayerHUD> _playerHUD;

    private void Start()
    {
        SetUpRounds(GameManager.Instance.PlayerOneRounds, 0);
        SetUpRounds(GameManager.Instance.PlayerTwoRounds, 1);
    }

    private void OnEnable()
    {
        GameEvents.OnEventTypeChanged += SetUpEvent;
        GameManager.Instance.OnRoundWon += RoundWon;
    }

    private void OnDisable()
    {
        GameEvents.OnEventTypeChanged -= SetUpEvent;
        GameManager.Instance.OnRoundWon -= RoundWon;
    }

    private void SetUpEvent(EventType eventType)
    {
        _event.SetUpEvent(eventType);
    }

    private void SetUpRounds(int count, int index)
    {
        for (int i = 0; i < count; i++)
        {
            _playerHUD[index].WinRound();
        }
    }

    private void RoundWon(int playerIndex)
    {
        _playerHUD[playerIndex].WinRound();
    }
}
