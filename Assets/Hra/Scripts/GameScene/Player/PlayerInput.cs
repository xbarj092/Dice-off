using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int PlayerId;
    public GridNode GridNode;
    public Grid<GridNode> Grid;
    public Rigidbody Rigidbody;
    public float moveSpeed = 5f;

    private bool isMoving = false;
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
            GridNode nextNode = Grid.GetGridObject(nextNodePosition);

            if (nextNode != null)
            {
                if (IsNodeOccupiedByAnotherPlayer(nextNode))
                {
                    Debug.Log("[PlayerInput] - this node is occupied by another player");
                }
                else
                {
                    targetPosition = new(nextNode.X, 1, nextNode.Y);
                    isMoving = true;
                }
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
            GridNode.Dice.DecreaseValue(true);
            isMoving = false;
            FinishTurn();
        }
    }

    private void FinishTurn()
    {
        GameManager.Instance.PlayerIndexPlaying = (GameManager.Instance.PlayerIndexPlaying + 1) % GameManager.Instance.Players.Count;
    }
}
