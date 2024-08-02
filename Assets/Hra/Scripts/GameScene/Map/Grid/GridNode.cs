using System;

[Serializable]
public class GridNode
{
    private Grid<GridNode> _grid;

    public int X;
    public int Y;

    public bool IsVisited;

    public GridNode CameFromNode;
    public int DistanceFromStart;

    public GridNode(Grid<GridNode> grid, int x, int y)
    {
        _grid = grid;
        X = x;
        Y = y;
    }
}
