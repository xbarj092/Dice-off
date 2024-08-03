using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private List<GameObject> _dots = new();
    [SerializeField] private SerializedDictionary<int, List<GameObject>> _combinations = new(); 

    public Vector2Int Coordinates;

    private int _value;

    public void Init(int x, int y, int randomValue)
    {
        Coordinates = new(x, y);
        _value = randomValue;

        SetValue();
    }

    public void DecreaseValue(bool isPlayerOnDice = false)
    {
        _value--;
        if (_value <= 0)
        {
            _rigidbody.useGravity = true;
            if (isPlayerOnDice)
            {
                foreach (PlayerInput player in GameManager.Instance.Players)
                {
                    if (player.GridNode.Dice == this)
                    {
                        player.Rigidbody.useGravity = true;
                    }
                }
            }
        }
        else
        {
            SetValue();
        }
    }

    private void SetValue()
    {
        foreach (GameObject dot in _dots)
        {
            dot.SetActive(_combinations[_value].Contains(dot));
        }
    }
}
