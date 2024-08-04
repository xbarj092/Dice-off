using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private GameOverScreen _gameOverScreenPrefab;
    [SerializeField] private AttackScreen _attackScreenPrefab;
    [SerializeField] private OptionsScreen _optionsScreenPrefab;
    [SerializeField] private PauseMenu _pauseScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.GameOver => Instantiate(_gameOverScreenPrefab, transform),
            GameScreenType.Attack => Instantiate(_attackScreenPrefab, transform),
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
