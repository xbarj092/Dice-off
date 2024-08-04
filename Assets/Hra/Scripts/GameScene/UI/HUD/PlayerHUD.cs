using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private Rounds _playerRounds;

    public void SetPlayerName(string name)
    {
        _playerName.text = name;
    }

    public void WinRound()
    {
        _playerRounds.SetRoundActive();
    }
}
