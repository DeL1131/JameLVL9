using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameMaster : MonoBehaviour
{
    [SerializeField] private GameMusic _gameMusic;
    [SerializeField] private GameOverMenu _endGameScreen;
    [SerializeField] Health _playerHealth;
    [SerializeField] Health _bossHealth;

    private bool _isGameEnded;

    private void Update()
    {
        if(_playerHealth.CurrentHealth <= 0)
        {
            GameOver();
        }
        if(_bossHealth.CurrentHealth <= 0)
        {
            CompleteGame();
        }
    }

    public void CompleteGame()
    {
        //if (_isGameEnded)
        //    return;

        //_isGameEnded = true;

        //if (_gameMusic != null)
        //    _gameMusic.PlayGameCompleteMusic();

        //if (_endGameScreen != null)
        //    _endGameScreen.Open();

        //Time.timeScale = 0f;

        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        SceneManager.LoadScene("RitualScene");
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