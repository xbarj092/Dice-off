using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner
{
    private Grid<GridNode> _grid;

    public void Spawn(Grid<GridNode> grid, Dice dicePrefab)
    {
        _grid = grid;

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PlaceDice(x, y, dicePrefab);
            }
        }
    }

    private void PlaceDice(int x, int y, Dice dicePrefab)
    {
        Dice dice = Object.Instantiate(dicePrefab);
        int randomValue = Random.Range(0, 7);
        dice.Init(x, y, randomValue);
        GridNode gridNode = _grid.GetGridObject(x, y);
        gridNode.Dice = dice;
        _grid.SetGridObject(x, y, gridNode);
    }
}
