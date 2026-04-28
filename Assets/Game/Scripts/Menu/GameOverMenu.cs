using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    [SerializeField] private Button _buttonCloseGame;

    private void OnEnable()
    {
        if (_buttonCloseGame != null)
            _buttonCloseGame.onClick.AddListener(CloseGame);
    }

    private void OnDisable()
    {
        if (_buttonCloseGame != null)
            _buttonCloseGame.onClick.RemoveListener(CloseGame);
    }

    private void CloseGame()
    {
        Application.Quit();
    }
}