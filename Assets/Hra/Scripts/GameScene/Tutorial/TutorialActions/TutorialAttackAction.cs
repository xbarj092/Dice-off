using UnityEngine;

public class TutorialAttackAction : TutorialAction
{
    [SerializeField] private GameObject _clickToContinue;

    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    public override void StartAction()
    {
        _tutorialPlayer.MoveToNextNarratorText();
        _clickToContinue.SetActive(true);
        _actionScheduler.ScheduleAction(HideText, () => Input.GetMouseButtonDown(0));
    }

    private void HideText()
    {
        _tutorialPlayer.PublicText.transform.parent.gameObject.SetActive(false);
        _clickToContinue.SetActive(false);
        TutorialEvents.OnPlayerAttacked += OnPlayerAttacked;
    }

    private void OnPlayerAttacked()
    {
        TutorialEvents.OnPlayerAttacked -= OnPlayerAttacked;
        _tutorialPlayer.PublicText.transform.parent.gameObject.SetActive(true);
        _clickToContinue.SetActive(true);
        _tutorialPlayer.MoveToNextNarratorText();
        _actionScheduler.ScheduleAction(OnActionFinishedInvoke, () => Input.GetMouseButtonDown(0));
    }

    public override void Exit()
    {
        SceneLoadManager.Instance.GoAttackToGame();
    }
}
