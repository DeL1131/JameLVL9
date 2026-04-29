using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameCompleetMenu : Menu
{
    [SerializeField] private Button _buttonCloseGame;

    private void OnEnable()
    {
        _buttonCloseGame.onClick.AddListener(CloseGame);
    }

    private void OnDisable()
    {
        _buttonCloseGame.onClick.RemoveListener(CloseGame);
    }

    private void CloseGame()
    {
        if (GameSession.Instance != null)
            GameSession.Instance.RecordBossFightResult(true);

        if (GameFlowManager.Instance != null)
        {
            GameFlowManager.Instance.LoadRitualScene();
            return;
        }

        Debug.LogWarning("GameCompleetMenu could not find a GameFlowManager.");
    }
}
