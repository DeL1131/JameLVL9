using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameMaster : MonoBehaviour
{
    [SerializeField] private GameMusic _gameMusic;
    [SerializeField] Health _playerHealth;
    [SerializeField] Health _bossHealth;
    [SerializeField] BossDeathEffect _bossDeathEffect;
    [SerializeField] private GameOverMenu _gameOverMenu;
    [SerializeField] private GameCompleetMenu _gameCompleeMenu;
    [SerializeField] private PlayerDeathEffect _playerDeathEffect;
    [SerializeField] private AudioSource _audioSource;

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
        if (_isGameEnded)
            return;

        _isGameEnded = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (_gameMusic != null)
            _gameMusic.PlayGameCompleteMusic();

        _bossDeathEffect.PlayDeath();   
        Invoke(nameof(OpenGameCompleteMenu), 3f);
    }

    private void OpenGameCompleteMenu()
    {
        //_gameCompleeMenu.Open();
        //_audioSource.Stop();
        //if (GameSession.Instance != null)
        //    GameSession.Instance.RecordBossFightResult(true);

        //if (GameFlowManager.Instance != null)
        //{
        //    GameFlowManager.Instance.LoadRitualScene();
        //    return;
        //}

        _gameCompleeMenu.Open();

    }

    public void GameOver()
    {
        if (_isGameEnded)
            return;

        _isGameEnded = true;

        if (_gameMusic != null)
            _gameMusic.PlayGameOverMusic();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _playerDeathEffect.PlayDeath();
        Invoke(nameof(OpenGameOverMenu), 3f);
    }

    private void OpenGameOverMenu()
    {
        //_gameOverMenu.Open();
        //_audioSource.Stop();
        //Time.timeScale = 0f;
        if (GameSession.Instance != null)
            GameSession.Instance.RecordBossFightResult(false);

        if (GameFlowManager.Instance != null)
        {
            GameFlowManager.Instance.LoadRitualScene();
            return;
        }

        SceneManager.LoadScene("RitualScene");
    }
}
