using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int PlayerId;
    public GridNode GridNode;

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerFinishedTurn += UpdatePlayerIndex;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerFinishedTurn -= UpdatePlayerIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePauseInput();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }

        HandleMovementInput();
        HandleAttackInput();
    }

    private void HandlePauseInput()
    {
        if (ScreenManager.Instance.ActiveGameScreen.GameScreenType == GameScreenType.Pause)
        {
            ScreenEvents.OnGameScreenClosedInvoke(GameScreenType.Pause);
        }
        else if (ScreenManager.Instance.ActiveGameScreen != null)
        {
            ScreenEvents.OnGameScreenClosedInvoke(ScreenManager.Instance.ActiveGameScreen.GameScreenType);
        }
        else
        {
            ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Pause);
        }
    }

    private void HandleMovementInput()
    {
        
    }

    private void HandleAttackInput()
    {

    }

    private void UpdatePlayerIndex()
    {
        GameManager.Instance.PlayerIndexPlaying = PlayerId;
    }

    private void FinishTurn()
    {
        GameManager.Instance.PlayerIndexPlaying = PlayerId;
    }
}
