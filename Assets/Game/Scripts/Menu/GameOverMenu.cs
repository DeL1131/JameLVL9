using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    [SerializeField] private Button _buttonCloseGame;
    [SerializeField] private Button _buttonRestartGame;

    private void OnEnable()
    {
        _buttonCloseGame.onClick.AddListener(CloseGame);
        _buttonRestartGame.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        _buttonCloseGame.onClick.RemoveListener(CloseGame);
        _buttonRestartGame.onClick.RemoveListener(RestartGame);
    }

    private void CloseGame()
    {
        Application.Quit();
    }

    private void RestartGame()
    {
        if (GameSession.Instance != null)
            GameSession.Instance.RecordBossFightResult(false);

        if (GameFlowManager.Instance != null)
        {
            GameFlowManager.Instance.LoadRitualScene();
            return;
        }

        Debug.LogWarning("GameOverMenu could not find a GameFlowManager.");
    }
}
