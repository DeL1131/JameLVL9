using System.Collections;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private const string ScepicalAttackAnimation = "SpecialAttackAnimation";

    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private ThreatSpawner _threatSpawner;

    [Header("Phase Timings")]
    [SerializeField] private float _easyPhaseStartTime = 3f;
    [SerializeField] private float _mediumPhaseStartTime = 20f;
    [SerializeField] private float _hardPhaseStartTime = 40f;

    [Header("Attack")]
    [SerializeField] private float _spawnDelayAfterAnimationStart = 0.5f;

    private Coroutine _bossRoutine;

    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();

        if (_threatSpawner != null)
            _threatSpawner.StopSpawning();
    }

    private void OnEnable()
    {
        _bossRoutine = StartCoroutine(BossRoutine());
    }

    private void OnDisable()
    {
        if (_bossRoutine != null)
            StopCoroutine(_bossRoutine);

        if (_threatSpawner != null)
            _threatSpawner.StopSpawning();
    }

    private IEnumerator BossRoutine()
    {
        yield return WaitUntilTime(_easyPhaseStartTime);
        StartOrbPhase(ThreatSpawner.DifficultyMode.Easy);

        yield return WaitUntilTime(_mediumPhaseStartTime);
        StartOrbPhase(ThreatSpawner.DifficultyMode.Medium);

        yield return WaitUntilTime(_hardPhaseStartTime);
        StartOrbPhase(ThreatSpawner.DifficultyMode.Hard);
    }

    private void StartOrbPhase(ThreatSpawner.DifficultyMode difficultyMode)
    {
        StartCoroutine(StartOrbPhaseRoutine(difficultyMode));
    }

    private IEnumerator StartOrbPhaseRoutine(ThreatSpawner.DifficultyMode difficultyMode)
    {
        PlayAttackAnimation();

        yield return new WaitForSeconds(_spawnDelayAfterAnimationStart);

        if (_threatSpawner == null)
        {
            Debug.LogWarning($"{nameof(Boss1)} has no ThreatSpawner assigned.", this);
            yield break;
        }

        _threatSpawner.SetDifficulty(difficultyMode);
        _threatSpawner.StartSpawning();

        Debug.Log($"Boss1 started orb phase: {difficultyMode}");
    }

    private void PlayAttackAnimation()
    {
        if (_animator == null)
        {
            Debug.LogWarning($"{nameof(Boss1)} has no Animator assigned.", this);
            return;
        }

        _animator.Play(ScepicalAttackAnimation);
    }

    private IEnumerator WaitUntilTime(float targetTime)
    {
        while (Time.timeSinceLevelLoad < targetTime)
            yield return null;
    }
}