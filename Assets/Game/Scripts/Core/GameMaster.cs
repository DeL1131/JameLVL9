using UnityEngine;
using UnityEngine.InputSystem;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameMusic _gameMusic;
    [SerializeField] private GameOverMenu _endGameScreen;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private GameMenu _gameMenu;

    private bool _isGameOver;
    private GameOverMenu _spawnedEndScreen;

    private void Update()
    {
        if (_isGameOver)
            return;

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_gameMenu != null)
                _gameMenu.PauseToMain();
        }
    }

    public void GameComplete()
    {
        if (_isGameOver)
            return;

        _isGameOver = true;


        if (_gameMusic != null)
            _gameMusic.PlayGameCompleteMusic();

        Time.timeScale = 0f;
    }

    private void GameOver()
    {
        if (_isGameOver)
            return;

        _isGameOver = true;

        if (_gameMusic != null)
            _gameMusic.PlayGameOverMusic();

        if (_spawnedEndScreen == null && _endGameScreen != null)
            _endGameScreen.Open();

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }

    private void NoPause()
    {
        Time.timeScale = 1.0f;
    }
}
