using UnityEngine;

public class Dice : MonoBehaviour
{
    public Vector2Int Coordinates;

    private int _randomValue;

    public void Init(int x, int y, int randomValue)
    {
        Coordinates = new(x, y);
        _randomValue = randomValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DecreaseValue(true);
        }
    }

    public void DecreaseValue(bool player = false)
    {
        _randomValue--;
        if (_randomValue <= 0)
        {
            // drop the dice
            if (player)
            {
                // drop the player
            }
        }
    }
}
