using UnityEngine;

public class MenuMainButtons : GameScreen
{
    public void PlayTheGame()
    {
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.NameChange);
        CloseScreen();
    }

    public void GoToOptions()
    {
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Options);
        CloseScreen();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
