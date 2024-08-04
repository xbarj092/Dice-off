using TMPro;
using UnityEngine;

public class AttackScreen : GameScreen
{
    [SerializeField] private TMP_Text _playerAttackingText;

    private void Start()
    {
        SetPlayerAttackingText();
    }

    private void SetPlayerAttackingText()
    {
        _playerAttackingText.text = $"Player " + GameManager.Instance.PlayerIndexPlaying + " is attacking!";
    }
}
