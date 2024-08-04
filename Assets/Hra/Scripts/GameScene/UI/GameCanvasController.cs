using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private GameOverScreen _gameOverScreenPrefab;
    [SerializeField] private OptionsScreen _optionsScreenPrefab;
    [SerializeField] private PauseMenu _pauseScreenPrefab;

    private void Awake()
    {
        if (!TutorialManager.Instance.CompletedTutorials.Contains(TutorialID.Field))
        {
            TutorialManager.Instance.InstantiateTutorial(TutorialID.Field);
        }
    }

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.GameOver => Instantiate(_gameOverScreenPrefab, transform),
            GameScreenType.Options => Instantiate(_optionsScreenPrefab, transform),
            GameScreenType.Pause => Instantiate(_pauseScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }

    protected override GameScreen GetActiveGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Options => Instantiate(_pauseScreenPrefab, transform),
            _ => base.GetActiveGameScreen(gameScreenType),
        };
    }
}
