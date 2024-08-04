using AYellowpaper.SerializedCollections;
using System.Collections;
using UnityEngine;

public class ThrowDice : MonoBehaviour
{
    [SerializeField] private BottomCollider _bottomCollider;
    [SerializeField] private Rigidbody _diceRigidbody;
    [SerializeField] private SerializedDictionary<Collider, int> _colliders = new();

    private bool _isThrown = false;
    private bool _hasSettled = false;
    private float _settlingTime = 1.0f;
    private float _checkVelocityThreshold = 0.1f;
    private float _waitTimeBeforeCheck = 1.0f;

    private void Start()
    {
        Throw();
    }

    private void Update()
    {
        if (_isThrown && !_hasSettled)
        {
            StartCoroutine(CheckIfSettled());
        }
    }

    private void Throw()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f)).normalized;
        float randomForce = Random.Range(10f, 20f);
        _diceRigidbody.AddForce(randomDirection * randomForce, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f));
        _diceRigidbody.AddTorque(randomTorque, ForceMode.Impulse);

        _isThrown = true;
    }

    private IEnumerator CheckIfSettled()
    {
        yield return new WaitForSeconds(_waitTimeBeforeCheck);

        while (_diceRigidbody.velocity.magnitude > _checkVelocityThreshold || _diceRigidbody.angularVelocity.magnitude > _checkVelocityThreshold)
        {
            yield return new WaitForSeconds(_settlingTime);
        }

        if (_bottomCollider.Collider != null)
        {
            GameManager.Instance.NextDamageValue = _colliders[_bottomCollider.Collider];
            _hasSettled = true;
            StartCoroutine(LoadToGame());
        }
        else
        {
            AlignDiceRotation();
        }
    }

    private void AlignDiceRotation()
    {
        Vector3 alignedEulerAngles = new(
            Mathf.Round(_diceRigidbody.transform.rotation.eulerAngles.x / 90) * 90,
            Mathf.Round(_diceRigidbody.transform.rotation.eulerAngles.y / 90) * 90,
            Mathf.Round(_diceRigidbody.transform.rotation.eulerAngles.z / 90) * 90
        );

        _diceRigidbody.transform.rotation = Quaternion.Euler(alignedEulerAngles);
    }

    private IEnumerator LoadToGame()
    {
        yield return new WaitForSeconds(1);
        SceneLoadManager.Instance.GoAttackToGame();
    }
}
