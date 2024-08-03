using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int PlayerId;
    public GridNode GridNode;
    public Grid<GridNode> Grid;
    public Rigidbody Rigidbody;
    public float moveSpeed = 5f;

    private GridNode _nextNode;
    private bool isMoving = false;
    private bool _hasAttackedThisTurn = false;
    private Vector3 targetPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePauseInput();
        }

        if (GameManager.Instance.PlayerIndexPlaying == PlayerId && !isMoving)
        {
            HandleMovementInput();
        }

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void HandlePauseInput()
    {
        if (ScreenManager.Instance.ActiveGameScreen.GameScreenType == GameScreenType.Pause)
        {
            ScreenEvents.OnGameScreenClosedInvoke(GameScreenType.Pause);
        }
        else if (ScreenManager.Instance.ActiveGameScreen != null)
        {
            ScreenEvents.OnGameScreenClosedInvoke(ScreenManager.Instance.ActiveGameScreen.GameScreenType);
        }
        else
        {
            ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Pause);
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

            if (_nextNode != null)
            {
                if (IsNodeOccupiedByAnotherPlayer(_nextNode))
                {
                    if (!_hasAttackedThisTurn)
                    {
                        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Attack);
                        ScreenEvents.OnGameScreenClosed += DealDamage;
                        _hasAttackedThisTurn = true;
                    }
                }
                else
                {
                    targetPosition = new(_nextNode.X, 1, _nextNode.Y);
                    isMoving = true;
                }
            }
        }
    }

    private void DealDamage(GameScreenType type)
    {
        ScreenEvents.OnGameScreenClosed -= DealDamage;
        if (type == GameScreenType.Attack)
        {
            _nextNode.Dice.DecreaseValue(GameManager.Instance.NextDamageValue);
            if (_nextNode.Dice.Value > 0)
            {
                FinishTurn();
            }
            else
            {
                GameManager.Instance.OnRoundWonInvoke(PlayerId);
            }
        }
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
            GridNode = Grid.GetGridObject(targetPosition);
            GridNode.Dice.DecreaseValue(1);
            isMoving = false;
            FinishTurn();
        }
    }

    private void FinishTurn()
    {
        GameManager.Instance.PlayerIndexPlaying = (GameManager.Instance.PlayerIndexPlaying + 1) % GameManager.Instance.Players.Count;
        _hasAttackedThisTurn = false;
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
