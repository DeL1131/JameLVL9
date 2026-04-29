using UnityEngine;
using UnityEngine.SceneManagement;
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
            GameSession.Instance.ResetSession();

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (GameFlowManager.Instance != null)
        {
            GameFlowManager.Instance.LoadRitualScene();
            return;
        }

        SceneManager.LoadScene("RitualScene");
    }
}
