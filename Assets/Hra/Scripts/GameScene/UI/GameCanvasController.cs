using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private GameOverScreen _gameOverScreenPrefab;
    [SerializeField] private AttackScreen _attackScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.GameOver => Instantiate(_gameOverScreenPrefab, transform),
            GameScreenType.Attack => Instantiate(_attackScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}