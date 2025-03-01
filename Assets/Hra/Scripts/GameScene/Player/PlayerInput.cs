using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private List<Material> _playerMats;

    public int PlayerId;
    public GridNode GridNode;
    public Grid<GridNode> Grid;
    public Rigidbody Rigidbody;
    public float MoveSpeed = 10f;
    public bool CanPlay = true;

    private GridNode _nextNode;
    private bool _isMoving = false;
    private bool _hasPlayedThisTurn = false;
    private Vector3 _targetPosition;

    private void Start()
    {
        CanPlay = TutorialManager.Instance.CompletedTutorials.Contains(TutorialID.Field);

        _meshRenderer.material = _playerMats[PlayerId];
    }

    private void Update()
    {
        if (CanPlay && GameManager.Instance.PlayerIndexPlaying == PlayerId && !_isMoving && !_hasPlayedThisTurn)
        {
            HandleMovementInput();
        }

        if (_isMoving)
        {
            MoveToTarget();
        }
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 nextNodePosition = new(GridNode.X + horizontal, 0, GridNode.Y + vertical);
            _nextNode = Grid.GetGridObject(nextNodePosition);

            if (_nextNode != null && DiceManager.Instance.GetAllDices().Any(dice => dice.GridNode == _nextNode))
            {
                TutorialEvents.OnPlayerMovedInvoke();
                _hasPlayedThisTurn = true;

                if (!TutorialManager.Instance.CompletedTutorials.Contains(TutorialID.Attack))
                {
                    if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Attack) && IsNodeOccupiedByAnotherPlayer(_nextNode))
                    {
                        TutorialEvents.OnPlayerAttackedInvoke();
                        SceneLoadManager.Instance.GoGameToAttack();
                        SceneLoader.OnSceneUnloadDone += DealDamage;
                    }
                    else
                    {
                        _targetPosition = new(_nextNode.X, 1, _nextNode.Y);
                        _isMoving = true;
                    }
                }
                else
                {
                    if (IsNodeOccupiedByAnotherPlayer(_nextNode))
                    {
                        SceneLoadManager.Instance.GoGameToAttack();
                        SceneLoader.OnSceneUnloadDone += DealDamage;
                    }
                    else
                    {
                        _targetPosition = new(_nextNode.X, 1, _nextNode.Y);
                        _isMoving = true;
                    }
                }
            }
        }
    }

    private bool IsNeighboringNodeOccupiedByAnotherPlayer(GridNode node)
    {
        Vector3[] neighborOffsets = new Vector3[]
        {
            new(1, 0, 0),
            new(-1, 0, 0),
            new(0, 0, 1),
            new(0, 0, -1)
        };

        foreach (Vector3 offset in neighborOffsets)
        {
            GridNode neighborNode = Grid.GetGridObject(new(node.X + offset.x, 0, node.Y + offset.z));
            if (neighborNode != null && IsNodeOccupiedByAnotherPlayer(neighborNode))
            {
                return true;
            }
        }

        return false;
    }

    private void DealDamage(SceneLoader.Scenes scene)
    {
        if (scene == SceneLoader.Scenes.AttackScene)
        {
            SceneLoader.OnSceneUnloadDone -= DealDamage;
            _nextNode.Dice.DecreaseValue(GameManager.Instance.NextDamageValue);
            if (_nextNode.Dice.Value > 0)
            {
                StartCoroutine(DelayedDealDamage());
            }
        }
    }

    private IEnumerator DelayedDealDamage()
    {
        yield return new WaitForSeconds(2);
        FinishTurn();
    }

    private bool IsNodeOccupiedByAnotherPlayer(GridNode node)
    {
        foreach (PlayerInput player in GameManager.Instance.Players)
        {
            if (player.GridNode == node && player.PlayerId != PlayerId)
            {
                return true;
            }
        }
        return false;
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.001f)
        {
            if (IsNeighboringNodeOccupiedByAnotherPlayer(_nextNode))
            {
                TutorialManager.Instance.InstantiateTutorial(TutorialID.Attack);
            }
            transform.position = _targetPosition;
            GridNode = Grid.GetGridObject(_targetPosition);
            GridNode.Dice.DecreaseValue(1);
            _isMoving = false;
            FinishTurn();
        }
    }

    private void FinishTurn()
    {
        GameManager.Instance.OnTurnFinishedInvoke(SetPlayedThisTurn);
    }

    private void SetPlayedThisTurn()
    {
        _hasPlayedThisTurn = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
            GameManager.Instance.OnRoundWonInvoke(PlayerId);
        }
    }
}
