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
        GridNode gridNode = _grid.GetGridObject(x, y);
        Dice dice = Object.Instantiate(dicePrefab, _grid.GetWorldPosition(x, y), Quaternion.identity);
        int randomValue = Random.Range(0, 7);
        dice.Init(x, y, randomValue);
        gridNode.Dice = dice;
        _grid.SetGridObject(x, y, gridNode);
    }
}
