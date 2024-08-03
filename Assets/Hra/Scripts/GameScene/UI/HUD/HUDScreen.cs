using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScreen : MonoBehaviour
{
    [SerializeField] private List<PlayerHUD> _playerHUD;

    private void Start()
    {
        SetUpRounds(GameManager.Instance.PlayerOneRounds, 0);
        SetUpRounds(GameManager.Instance.PlayerTwoRounds, 1);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnRoundWon += RoundWon;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnRoundWon -= RoundWon;
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
