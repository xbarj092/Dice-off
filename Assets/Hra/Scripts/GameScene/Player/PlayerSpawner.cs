using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerPrefab;

    private MapGenerator _mapGenerator;

    private void Awake()
    {
        _mapGenerator = FindObjectOfType<MapGenerator>();
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        SpawnPlayer(1, 0, 0);
        SpawnPlayer(2, _mapGenerator.Grid.GetWidth(), _mapGenerator.Grid.GetHeight());
    }

    private void SpawnPlayer(int playerId, int x, int y)
    {
        PlayerInput newPlayer = Instantiate(_playerPrefab, transform);
        GridNode gridNode = _mapGenerator.Grid.GetGridObject(x, y);
        newPlayer.PlayerId = playerId;
        newPlayer.GridNode = gridNode;
        newPlayer.transform.position = _mapGenerator.Grid.GetWorldPosition(x, y);
        newPlayer.transform.position += new Vector3(0, 1, 0);
    }
}
