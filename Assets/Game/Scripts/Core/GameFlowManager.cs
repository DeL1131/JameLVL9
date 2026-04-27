using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; private set; }

    [SerializeField] private string _mainGameScene = "MainGameScene";

    [SerializeField]
    private string[] _miniGameSceneNames =
    {
        "MiniGame_01",
        "MiniGame_02",
        "MiniGame_03",
        "MiniGame_04",
        "MiniGame_05"
    };

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

    public void LoadMiniGame(int miniGameIndex)
    {
        if (miniGameIndex < 1 || miniGameIndex > _miniGameSceneNames.Length)
        {
            Debug.LogWarning($"Invalid mini-game index '{miniGameIndex}'. Expected a value from 1 to {_miniGameSceneNames.Length}. No scene was loaded.");
            return;
        }

        LoadScene(_miniGameSceneNames[miniGameIndex - 1]);
    }

    public void ReloadCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log($"Reloading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }

    private void LoadScene(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}