using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; private set; }

    [SerializeField] private string _mainGameScene = "RitualScene";

    [SerializeField]
    private string[] _miniGameSceneNames =
    {
        "MiniGame_01",
        "MiniGame_02",
        "MiniGame_03",
        "MiniGame_04",
        "MiniGame_05"
    };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (Instance != null)
            return;

        GameObject gameFlowManagerObject = new GameObject(nameof(GameFlowManager));
        gameFlowManagerObject.AddComponent<GameFlowManager>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMainGameScene()
    {
        LoadScene(_mainGameScene);
    }

    public void LoadRitualScene()
    {
        LoadMainGameScene();
    }

    public void LoadMiniGame(int miniGameIndex)
    {
        if (miniGameIndex < 1 || miniGameIndex > _miniGameSceneNames.Length)
        {
            Debug.LogWarning($"Invalid mini-game index '{miniGameIndex}'. Expected a value from 1 to {_miniGameSceneNames.Length}. No scene was loaded.");
            return;
        }

        LoadScene(_miniGameSceneNames[miniGameIndex - 1]);
    }

    public void LoadSceneByName(string sceneName)
    {
        LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        Debug.Log($"Reloading scene: {sceneName}");

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }

    private void LoadScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogWarning("Scene name is empty. No scene was loaded.");
            return;
        }

        if (Application.CanStreamedLevelBeLoaded(sceneName) == false)
        {
            Debug.LogError($"Scene '{sceneName}' was not found in Build Settings. No scene was loaded.");
            return;
        }

        Debug.Log($"Loading scene: {sceneName}");

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
