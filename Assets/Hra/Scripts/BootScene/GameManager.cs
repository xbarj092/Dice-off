using System;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    public List<PlayerInput> Players;
    public int PlayerIndexPlaying;

    public event Action OnPlayerFinishedTurn;
    public void OnPlayerFinishedTurnInvoke()
    {
        OnPlayerFinishedTurn?.Invoke();
    }
}
