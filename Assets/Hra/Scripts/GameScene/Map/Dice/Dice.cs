using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private DiceAnimator _animator;
    [SerializeField] private DiceEffectPlayer _effectPlayer;
    public DiceEffectPlayer DiceEffectPlayer => _effectPlayer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private List<GameObject> _dots = new();
    [SerializeField] private SerializedDictionary<int, List<GameObject>> _combinations = new(); 

    public GridNode GridNode;

    public int Value;

    private void OnEnable()
    {
        DiceManager.Instance.RegisterDice(this);
    }

    private void OnDisable()
    {
        DiceManager.Instance.UnregisterDice(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }

    public void Init(GridNode gridNode, int randomValue)
    {
        GridNode = gridNode;
        Value = randomValue;

        SetValue();
    }

    public void DecreaseValue(int value)
    {
        Value -= value;
        if (Value <= 0)
        {
            StartCoroutine(Fall());
        }
        else
        {
            SetValue();
        }
    }

    private IEnumerator Fall()
    {
        _animator.PlayAnimation(DiceAnimations.Fall);
        _effectPlayer.PlayFallFX();
        yield return new WaitForSeconds(1);
        _rigidbody.useGravity = true;
        foreach (PlayerInput player in GameManager.Instance.Players)
        {
            if (player.GridNode.Dice == this)
            {
                player.Rigidbody.useGravity = true;
            }
        }
    }

    private void SetValue()
    {
        foreach (GameObject dot in _dots)
        {
            dot.SetActive(_combinations[Value].Contains(dot));
        }
    }
}
