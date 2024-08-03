using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerPrefab;

    private MapGenerator _mapGenerator;

    private void Start()
    {
        _mapGenerator = FindObjectOfType<MapGenerator>();
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        SpawnPlayer(0, 0, 0);
        SpawnPlayer(1, _mapGenerator.Grid.GetWidth() - 1, _mapGenerator.Grid.GetHeight() - 1);
    }

    private void SpawnPlayer(int playerId, int x, int y)
    {
        PlayerInput newPlayer = Instantiate(_playerPrefab, transform);
        GridNode gridNode = _mapGenerator.Grid.GetGridObject(x, y);
        newPlayer.PlayerId = playerId;
        newPlayer.Grid = _mapGenerator.Grid;
        newPlayer.GridNode = gridNode;
        newPlayer.transform.position = _mapGenerator.Grid.GetWorldPosition(x, y);
        newPlayer.transform.position += new Vector3(0, 1, 0);
        if (!GameManager.Instance.Players.Contains(newPlayer))
        {
            GameManager.Instance.Players.Add(newPlayer);
        }
    }
}
