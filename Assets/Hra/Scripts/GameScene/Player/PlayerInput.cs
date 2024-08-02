using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;

    private void Update()
    {
        HandlePauseInput();
        HandleMovementInput();
    }

    private void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _controller.Move(horizontal, vertical);
    }
}
