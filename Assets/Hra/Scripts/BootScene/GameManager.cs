using System;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    public List<PlayerInput> Players = new();
    public int PlayerIndexPlaying = 1;
}
