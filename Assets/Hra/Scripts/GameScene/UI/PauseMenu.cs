using UnityEngine;

public class PauseMenu : GameScreen
{
    public void ResumeGame()
    {
        ScreenEvents.OnGameScreenClosedInvoke(GameScreenType);
    }

    public void OptionsScreen()
    {
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Options);
        CloseScreen();
    }

    public void RestartRound()
    {
        GameManager.Instance.Players = new();
        SceneLoadManager.Instance.RestartGame();
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void ReturnToMenu()
    {
        SceneLoadManager.Instance.GoGameToMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
