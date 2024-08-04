using TMPro;
using UnityEngine;

public class MenuNameChangeScreen : GameScreen
{
    [SerializeField] private TMP_InputField _playerOneName;
    [SerializeField] private TMP_InputField _playerTwoName;

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
        _playerOneName.onValueChanged.AddListener(ChangePlayerOneName);
        _playerTwoName.onValueChanged.AddListener(ChangePlayerTwoName);
    }

    private void OnDisable()
    {
        _playerOneName.onValueChanged.RemoveListener(ChangePlayerOneName);
        _playerTwoName.onValueChanged.RemoveListener(ChangePlayerTwoName);
    }

    private void ChangePlayerOneName(string newName)
    {
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

    private void ChangePlayerTwoName(string newName)
    {
        if (GameManager.Instance.PlayerNames.Count == 1)
        {
            GameManager.Instance.PlayerNames.Add(newName);
        }
        else
        {
            GameManager.Instance.PlayerNames[1] = newName;
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
