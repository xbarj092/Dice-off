using UnityEngine;

public class MenuCanvasController : BaseCanvasController
{
    [SerializeField] private MenuMainButtons _menuMainButtonsPrefab;
    [SerializeField] private MenuNameChangeScreen _menuNameChangeScreen;
    [SerializeField] private OptionsScreen _optionsScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.MenuMain => Instantiate(_menuMainButtonsPrefab, transform),
            GameScreenType.NameChange => Instantiate(_menuNameChangeScreen, transform),
            GameScreenType.Options => Instantiate(_optionsScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }

    protected override GameScreen GetActiveGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.NameChange => Instantiate(_menuMainButtonsPrefab, transform),
            _ => base.GetActiveGameScreen(gameScreenType),
        };
    }
}
