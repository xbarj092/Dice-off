using TMPro;
using UnityEngine;

public class MenuNameChangeScreen : GameScreen
{
    [SerializeField] private TMP_InputField _playerOneName;
    [SerializeField] private TMP_InputField _playerTwoName;
    private const int MaxNameLength = 8;

    private void Start()
    {
        if (GameManager.Instance.PlayerNames.Count > 0 && GameManager.Instance.PlayerNames[0] != null)
        {
            _playerOneName.text = GameManager.Instance.PlayerNames[0];
        }
        if (GameManager.Instance.PlayerNames.Count > 1 && GameManager.Instance.PlayerNames[1] != null)
        {
            _playerTwoName.text = GameManager.Instance.PlayerNames[1];
        }

        _playerTwoName.interactable = _playerOneName.text.Length > 0;
    }

    private void OnEnable()
    {
        _playerOneName.onValueChanged.AddListener(HandlePlayerOneNameChange);
        _playerOneName.onEndEdit.AddListener(EnforcePlayerOneNameLength);
        _playerTwoName.onValueChanged.AddListener(HandlePlayerTwoNameChange);
        _playerTwoName.onEndEdit.AddListener(EnforcePlayerTwoNameLength);
    }

    private void OnDisable()
    {
        _playerOneName.onValueChanged.RemoveListener(HandlePlayerOneNameChange);
        _playerOneName.onEndEdit.RemoveListener(EnforcePlayerOneNameLength);
        _playerTwoName.onValueChanged.RemoveListener(HandlePlayerTwoNameChange);
        _playerTwoName.onEndEdit.RemoveListener(EnforcePlayerTwoNameLength);
    }

    private void HandlePlayerOneNameChange(string newName)
    {
        if (newName.Length > MaxNameLength)
        {
            _playerOneName.text = newName.Substring(0, MaxNameLength);
            newName = _playerOneName.text;
        }

        _playerTwoName.interactable = newName.Length > 0;

        if (GameManager.Instance.PlayerNames.Count == 0)
        {
            GameManager.Instance.PlayerNames.Add(newName);
        }
        else
        {
            GameManager.Instance.PlayerNames[0] = newName;
        }
    }

    private void EnforcePlayerOneNameLength(string newName)
    {
        if (newName.Length > MaxNameLength)
        {
            _playerOneName.text = newName.Substring(0, MaxNameLength);
            GameManager.Instance.PlayerNames[0] = _playerOneName.text;
        }
    }

    private void HandlePlayerTwoNameChange(string newName)
    {
        if (newName.Length > MaxNameLength)
        {
            _playerTwoName.text = newName.Substring(0, MaxNameLength);
            newName = _playerTwoName.text;
        }

        if (GameManager.Instance.PlayerNames.Count == 1)
        {
            GameManager.Instance.PlayerNames.Add(newName);
        }
        else
        {
            GameManager.Instance.PlayerNames[1] = newName;
        }
    }

    private void EnforcePlayerTwoNameLength(string newName)
    {
        if (newName.Length > MaxNameLength)
        {
            _playerTwoName.text = newName.Substring(0, MaxNameLength);
            GameManager.Instance.PlayerNames[1] = _playerTwoName.text;
        }
    }

    public void StartGame()
    {
        if (GameManager.Instance.PlayerNames.Count == 2 &&
            GameManager.Instance.PlayerNames[0].Length > 0 &&
            GameManager.Instance.PlayerNames[1].Length > 0)
        {
            SceneLoadManager.Instance.GoMenuToGame();
        }
    }
}
