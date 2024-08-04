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
        StartCoroutine(SetUpNames());
    }

    private IEnumerator SetUpNames()
    {
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < GameManager.Instance.Players.Count; i++)
        {
            _playerHUD[i].SetPlayerName(GameManager.Instance.PlayerNames[i]);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnEventTypeChanged += SetUpEvent;
        GameManager.Instance.OnRoundWon += RoundWon;
        GameManager.Instance.OnTurnFinished += UpdateEventCount;
    }

    private void OnDisable()
    {
        GameEvents.OnEventTypeChanged -= SetUpEvent;
        GameManager.Instance.OnRoundWon -= RoundWon;
        GameManager.Instance.OnTurnFinished -= UpdateEventCount;
    }

    private void UpdateEventCount()
    {
        _event.UpdateEventCountText(6 - GameManager.Instance.Turns % 6);
    }

    private void SetUpEvent(EventType eventType)
    {
        if (eventType == EventType.PredefinedDiceFall)
        {
            _event.DisableText();
        }

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
