using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    [SerializeField] private Button _buttonRestart;

    private void OnEnable()
    {
        if (_buttonRestart != null)
            _buttonRestart.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        if (_buttonRestart != null)
            _buttonRestart.onClick.RemoveListener(RestartGame);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}