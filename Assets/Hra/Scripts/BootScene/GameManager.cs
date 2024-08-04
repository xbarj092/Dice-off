using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public List<PlayerInput> Players = new();
    public List<string> PlayerNames = new();
    public int PlayerIndexPlaying = 0;
    public int NextDamageValue;

    public int PlayerOneRounds;
    public int PlayerTwoRounds;

    public int Turns;
    public IEventStrategy Strategy;

    public event Action OnTurnFinished;
    public void OnTurnFinishedInvoke(Action onSuccess)
    {
        StartCoroutine(WaitForDiceToStopFalling(onSuccess));
    }

    private IEnumerator WaitForDiceToStopFalling(Action onSuccess)
    {
        while (DiceManager.Instance.GetAllDices().Any(dice => dice.IsFalling))
        {
            yield return new WaitForSeconds(1f);
        }

        Turns++;
        if (Players.Count > 1)
        {
            PlayerIndexPlaying = (PlayerIndexPlaying + 1) % Players.Count;
            onSuccess?.Invoke();
        }

        OnTurnFinished?.Invoke();
    }

    public event Action<int> OnRoundWon;
    public void OnRoundWonInvoke(int playerIndex)
    {
        Players = new();
        if (playerIndex == 1)
        {
            PlayerOneRounds++;
        }
        else
        {
            PlayerTwoRounds++;
        }

        Turns = 0;
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
    
    private IEnumerator DelayScreenOpen()
    {
        yield return new WaitForSeconds(2);
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.GameOver);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePauseInput();
        }
    }

    public void RestartGame()
    {
        Players = new();
        Turns = 0;
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
