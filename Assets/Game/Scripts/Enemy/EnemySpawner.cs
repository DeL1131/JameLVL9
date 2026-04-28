using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event Action GameOver;

    [Header("References")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private SpawnPoint[] _spawnPoints;

    [Header("Spawn Settings")]
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private float _spawnRateIncrease = 0.5f;
    [SerializeField] private float _increaseEverySeconds = 10f;
    [SerializeField] private float _stopAfterSeconds = 60f;

    private float _gameTimer;
    private float _increaseTimer;
    private float _spawnTimer;
    private bool _isSpawning = true;

    private void Update()
    {
        if (_isSpawning == false)
            return;

        _gameTimer += Time.deltaTime;
        _increaseTimer += Time.deltaTime;
        _spawnTimer += Time.deltaTime;

        if (_gameTimer >= _stopAfterSeconds)
        {
            _isSpawning = false;
            return;
        }

        if (_increaseTimer >= _increaseEverySeconds)
        {
            _spawnRate += _spawnRateIncrease;
            _increaseTimer = 0f;
        }

        float spawnInterval = 1f / _spawnRate;

        if (_spawnTimer >= spawnInterval)
        {
            TrySpawnEnemy();
            _spawnTimer = 0f;
        }
    }

    private void TrySpawnEnemy()
    {
        if (TryGetFreeSpawnPoint(out SpawnPoint spawnPoint) == false)
        {
            _isSpawning = false;
            GameOver?.Invoke();
            Debug.Log("Game Over: все точки спавна заняты");
            return;
        }

        Enemy enemy = Instantiate(
            _enemyPrefab,
            spawnPoint.transform.position,
            spawnPoint.transform.rotation
        );

        spawnPoint.SetState(true);

        enemy.Died += () =>
        {
            spawnPoint.SetState(false);
        };
    }

    private bool TryGetFreeSpawnPoint(out SpawnPoint freeSpawnPoint)
    {
        freeSpawnPoint = null;

        int startIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            int index = (startIndex + i) % _spawnPoints.Length;
            SpawnPoint spawnPoint = _spawnPoints[index];

            if (spawnPoint.IsPointPlacement == false)
            {
                freeSpawnPoint = spawnPoint;
                return true;
            }
        }

        return false;
    }
}