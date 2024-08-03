using TMPro;
using UnityEngine;

public class GameOverScreen : GameScreen
{
    [SerializeField] private TMP_Text _playerWon;

    private void Start()
    {
        SetPlayerWonText();
    }

    private void SetPlayerWonText()
    {
        _playerWon.text = $"Player " + GameManager.Instance.PlayerIndexPlaying + " won!";
    }

    public void ContinueGame()
    {
        SceneLoadManager.Instance.RestartGame();
        Debug.Log($"ContinueGame");
    }

    public void ReturnToMenu()
    {
        SceneLoadManager.Instance.GoGameToMenu();
    }
}
