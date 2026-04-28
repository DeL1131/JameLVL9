using System.Collections;
using UnityEngine;

public class ThreatSpawner : MonoBehaviour
{
    public enum DifficultyMode
    {
        Easy,
        Medium,
        Hard
    }

    [Header("References")]
    [SerializeField] private ThreatOrb _threatOrbPrefab;

    [Header("Spawn Area")]
    [SerializeField] private Transform _areaCenter;
    [SerializeField] private float _spawnRadius = 3f;

    [Header("Difficulty")]
    [SerializeField] private DifficultyMode _difficultyMode = DifficultyMode.Easy;

    [Header("Easy")]
    [SerializeField] private int _easyOrbCount = 2;
    [SerializeField] private float _easyFadeInDuration = 1.5f;
    [SerializeField] private Vector2 _easySpawnDelayRange = new Vector2(0.7f, 1.2f);

    [Header("Medium")]
    [SerializeField] private int _mediumOrbCount = 4;
    [SerializeField] private float _mediumFadeInDuration = 1f;
    [SerializeField] private Vector2 _mediumSpawnDelayRange = new Vector2(0.45f, 0.9f);

    [Header("Hard")]
    [SerializeField] private int _hardOrbCount = 7;
    [SerializeField] private float _hardFadeInDuration = 0.6f;
    [SerializeField] private Vector2 _hardSpawnDelayRange = new Vector2(0.2f, 0.55f);

    private Coroutine _spawnCoroutine;


    public void SetDifficulty(DifficultyMode difficultyMode)
    {
        _difficultyMode = difficultyMode;
        RestartSpawning();
    }

    public void StartSpawning()
    {
        if (_spawnCoroutine != null)
            return;

        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnCoroutine == null)
            return;

        StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;
    }

    private void RestartSpawning()
    {
        StopSpawning();
        StartSpawning();
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            int orbCount = GetOrbCount();

            for (int i = 0; i < orbCount; i++)
            {
                SpawnSingleOrb();

                float delay = GetRandomSpawnDelay();
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitForSeconds(GetWaveRestDelay());
        }
    }

    private void SpawnSingleOrb()
    {
        Vector2 spawnPosition = GetRandomPointInRadius();

        ThreatOrb orb = Instantiate(
            _threatOrbPrefab,
            spawnPosition,
            Quaternion.identity
        );

        orb.Initialize(GetFadeInDuration());
    }

    private Vector2 GetRandomPointInRadius()
    {
        Vector2 center = _areaCenter != null
            ? _areaCenter.position
            : transform.position;

        return center + Random.insideUnitCircle * _spawnRadius;
    }

    private int GetOrbCount()
    {
        return _difficultyMode switch
        {
            DifficultyMode.Easy => _easyOrbCount,
            DifficultyMode.Medium => _mediumOrbCount,
            DifficultyMode.Hard => _hardOrbCount,
            _ => _easyOrbCount
        };
    }

    private float GetFadeInDuration()
    {
        return _difficultyMode switch
        {
            DifficultyMode.Easy => _easyFadeInDuration,
            DifficultyMode.Medium => _mediumFadeInDuration,
            DifficultyMode.Hard => _hardFadeInDuration,
            _ => _easyFadeInDuration
        };
    }

    private float GetRandomSpawnDelay()
    {
        Vector2 range = _difficultyMode switch
        {
            DifficultyMode.Easy => _easySpawnDelayRange,
            DifficultyMode.Medium => _mediumSpawnDelayRange,
            DifficultyMode.Hard => _hardSpawnDelayRange,
            _ => _easySpawnDelayRange
        };

        return Random.Range(range.x, range.y);
    }

    private float GetWaveRestDelay()
    {
        return _difficultyMode switch
        {
            DifficultyMode.Easy => 1.5f,
            DifficultyMode.Medium => 1f,
            DifficultyMode.Hard => 0.5f,
            _ => 1.5f
        };
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = _areaCenter != null
            ? _areaCenter.position
            : transform.position;

        Gizmos.DrawWireSphere(center, _spawnRadius);
    }
}