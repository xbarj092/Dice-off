using TMPro;
using UnityEngine;

public class GameOverScreen : GameScreen
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {

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
