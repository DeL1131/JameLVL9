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

    public void EnableInteractionDuringCredits()
    {
        if (CanvasGroup == null)
            CanvasGroup = GetComponent<CanvasGroup>();

        if (CanvasGroup != null)
        {
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.ignoreParentGroups = true;
        }

        if (_buttonCloseGame != null)
            _buttonCloseGame.interactable = true;
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
