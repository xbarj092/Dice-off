using System.Collections.Generic;
using UnityEngine;

public class Rounds : MonoBehaviour
{
    [SerializeField] private List<Round> _rounds = new();

    private int _roundsWon = 0;

    public void SetRoundActive()
    {
        _rounds[_roundsWon].SetRoundActive();
        _roundsWon++;
    }
}
