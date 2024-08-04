using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public List<PlayerInput> Players = new();
    public int PlayerIndexPlaying = 0;
    public int NextDamageValue;

    public int PlayerOneRounds;
    public int PlayerTwoRounds;

    public event Action<int> OnRoundWon;
    public void OnRoundWonInvoke(int playerIndex)
    {
        Players = new();
        if (playerIndex == 0)
        {
            PlayerOneRounds++;
        }
        else
        {
            PlayerTwoRounds++;
        }

        OnRoundWon?.Invoke(playerIndex);

        if (PlayerOneRounds >= 2 || PlayerTwoRounds >= 2)
        {
            PlayerOneRounds = 0;
            PlayerTwoRounds = 0;
            StartCoroutine(DelayScreenOpen());
        }
        else
        {
            SceneLoadManager.Instance.RestartGame();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePauseInput();
        }
    }

    private IEnumerator DelayScreenOpen()
    {
        yield return new WaitForSeconds(2);
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.GameOver);
    }

    public void RestartGame()
    {
        Players = new();
        PlayerOneRounds = 0;
        PlayerTwoRounds = 0;
        SceneLoadManager.Instance.RestartGame();
    }

    private void HandlePauseInput()
    {
        if (ScreenManager.Instance.ActiveGameScreen != null)
        {
            if (ScreenManager.Instance.ActiveGameScreen.GameScreenType == GameScreenType.Pause)
            {
                ScreenEvents.OnGameScreenClosedInvoke(GameScreenType.Pause);
            }
            else
            {
                ScreenEvents.OnGameScreenClosedInvoke(ScreenManager.Instance.ActiveGameScreen.GameScreenType);
            }
        }
        else
        {
            ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Pause);
        }
    }
}
