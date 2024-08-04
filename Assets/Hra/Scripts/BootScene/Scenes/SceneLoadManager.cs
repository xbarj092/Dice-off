using System;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    protected override void Init()
    {
        base.Init();
        GoBootToMenu();
    }

    public void GoBootToMenu()
    {
        SceneLoader.OnSceneLoadDone += OnBootToMenuLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene);
    }

    private void OnBootToMenuLoadDone(SceneLoader.Scenes scene)
    {
        SceneLoader.OnSceneLoadDone -= OnBootToMenuLoadDone;
    }

    public void GoMenuToGame()
    {
        SceneLoader.OnSceneLoadDone += OnMenuToGameLoadDone;
        // Cursor.lockState = CursorLockMode.Locked;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.Scenes.MenuScene);
    }

    private void OnMenuToGameLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnMenuToGameLoadDone;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoGameToAttack()
    {
        SceneLoader.OnSceneLoadDone += OnGameToAttackLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.AttackScene);
    }

    private void OnGameToAttackLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnGameToAttackLoadDone;
    }

    public void GoAttackToGame()
    {
        SceneLoader.OnSceneLoadDone += OnAttackToGameLoadDone;
        SceneLoader.UnloadScene(SceneLoader.Scenes.AttackScene);
    }

    private void OnAttackToGameLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnAttackToGameLoadDone;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoGameToMenu()
    {
        SceneLoader.OnSceneLoadDone += OnGameToMenuLoadDone;
        // Cursor.lockState = CursorLockMode.Confined;
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene, toUnload: SceneLoader.Scenes.GameScene);
    }

    private void OnGameToMenuLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnGameToMenuLoadDone;
    }

    public bool IsSceneLoaded(SceneLoader.Scenes sceneToCheck)
    {
        return SceneLoader.IsSceneLoaded(sceneToCheck);
    }

    public void RestartGame()
    {
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.Scenes.GameScene);
    }
}
