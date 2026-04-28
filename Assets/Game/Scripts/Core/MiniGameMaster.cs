using UnityEngine;

public class MiniGameMaster : MonoBehaviour
{
    [SerializeField] private GameMusic _gameMusic;
    [SerializeField] private GameOverMenu _endGameScreen;

    private bool _isGameEnded;

    public void CompleteGame()
    {
        if (_isGameEnded)
            return;

        _isGameEnded = true;

        if (_gameMusic != null)
            _gameMusic.PlayGameCompleteMusic();

        if (_endGameScreen != null)
            _endGameScreen.Open();

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GameOver()
    {
        if (_isGameEnded)
            return;

        _isGameEnded = true;

        if (_gameMusic != null)
            _gameMusic.PlayGameOverMusic();

        if (_endGameScreen != null)
            _endGameScreen.Open();

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}