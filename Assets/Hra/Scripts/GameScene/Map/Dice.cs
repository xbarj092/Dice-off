using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private List<GameObject> _dots = new();
    [SerializeField] private SerializedDictionary<int, List<GameObject>> _combinations = new(); 

    public Vector2Int Coordinates;

    public int Value;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }

    public void Init(int x, int y, int randomValue)
    {
        Coordinates = new(x, y);
        Value = randomValue;

        SetValue();
    }

    public void DecreaseValue(int value)
    {
        Value -= value;
        if (Value <= 0)
        {
            _rigidbody.useGravity = true;
            foreach (PlayerInput player in GameManager.Instance.Players)
            {
                if (player.GridNode.Dice == this)
                {
                    player.Rigidbody.useGravity = true;
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
            dot.SetActive(_combinations[Value].Contains(dot));
        }
    }
}
