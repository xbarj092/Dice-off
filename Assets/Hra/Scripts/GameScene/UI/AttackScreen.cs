using System.Collections;
using TMPro;
using UnityEngine;

public class AttackScreen : GameScreen
{
    [SerializeField] private TMP_Text _playerAttackingText;
    [SerializeField] private TMP_Text _damageText;

    private const int SCREEN_LIFE_TIME = 3;

    private void Start()
    {
        SetPlayerAttackingText();
        StartCoroutine(Close());
    }

    private new IEnumerator Close()
    {
        yield return new WaitForSeconds(SCREEN_LIFE_TIME);
        CloseScreen();
        base.Close();
    }

    private void SetPlayerAttackingText()
    {
        _playerAttackingText.text = $"Player " + GameManager.Instance.PlayerIndexPlaying + " is attacking!";
        int nextDamageValue = Random.Range(1, 7);
        GameManager.Instance.NextDamageValue = nextDamageValue;
        _damageText.text = $"" + nextDamageValue;
    }
}
