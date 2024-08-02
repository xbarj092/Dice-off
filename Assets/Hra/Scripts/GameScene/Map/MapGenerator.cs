using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Dice _dicePrefab;

    private DiceSpawner _diceSpawner;
    private Grid<GridNode> _grid;

    private const int GRID_WIDTH = 5;
    private const int GRID_HEIGHT = 5;

    private void Awake()
    {
        _grid = new(GRID_WIDTH, GRID_HEIGHT, 10, (Grid<GridNode> g, int x, int y) => new GridNode(g, x, y));
    }

    private void Start()
    {
        _diceSpawner.Spawn(_grid, _dicePrefab);
    }
}
