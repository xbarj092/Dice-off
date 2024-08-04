using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private Rounds _playerRounds;
    [SerializeField] private Image _currentlyPlayingImage;

    public void SetPlayerName(string name)
    {
        _playerName.text = name;
    }

    public void WinRound()
    {
        _playerRounds.SetRoundActive();
    }

    public void SetCurrentlyPlaying(int playerIndex)
    {
        _currentlyPlayingImage.enabled = playerIndex == GameManager.Instance.PlayerIndexPlaying;
    }
}
