using System.Collections;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private const string ScepicalAttackAnimation = "SpecialAttackAnimation";
    private const string LaserChargeAnimation = "SkillAnimationLaser";
    private const string IsLaserActiveParam = "IsLaserAbilityActive";

    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private ThreatSpawner _threatSpawner;
    [SerializeField] private LaserBeamAbility _laserPrefab;
    [SerializeField] private Transform _mouthPoint;

    [Header("Phase Timings")]
    [SerializeField] private float _easyPhaseStartTime = 3f;
    [SerializeField] private float _mediumPhaseStartTime = 20f;
    [SerializeField] private float _hardPhaseStartTime = 40f;

    [Header("Orb Attack")]
    [SerializeField] private float _spawnDelayAfterAnimationStart = 0.5f;

    [Header("Laser Attack")]
    [SerializeField] private float _laserFirstStartTime = 10f;
    [SerializeField] private float _laserRepeatDelay = 8f;

    private Coroutine _bossRoutine;
    private Coroutine _laserRoutine;
    private LaserBeamAbility _currentLaser;

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
        _laserRoutine = StartCoroutine(LaserLoopRoutine());
    }

    private void OnDisable()
    {
        if (_bossRoutine != null)
            StopCoroutine(_bossRoutine);

        if (_laserRoutine != null)
            StopCoroutine(_laserRoutine);

        if (_threatSpawner != null)
            _threatSpawner.StopSpawning();

        StopLaser();
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

    private IEnumerator LaserLoopRoutine()
    {
        yield return WaitUntilTime(_laserFirstStartTime);

        while (true)
        {
            StartLaserAttack();

            yield return new WaitForSeconds(_laserRepeatDelay);
        }
    }

    private void StartLaserAttack()
    {
        if (_laserPrefab == null || _mouthPoint == null)
            return;

        PlayAnimation(LaserChargeAnimation);
    }

    private void StartOrbPhase(ThreatSpawner.DifficultyMode difficultyMode)
    {
        StartCoroutine(StartOrbPhaseRoutine(difficultyMode));
    }

    private IEnumerator StartOrbPhaseRoutine(ThreatSpawner.DifficultyMode difficultyMode)
    {
        PlayAnimation(ScepicalAttackAnimation);

        yield return new WaitForSeconds(_spawnDelayAfterAnimationStart);

        if (_threatSpawner == null)
            yield break;

        _threatSpawner.SetDifficulty(difficultyMode);
        _threatSpawner.StartSpawning();
    }

    public void OnLaserFire()
    {
        if (_laserPrefab == null || _mouthPoint == null)
            return;

        if (_animator != null)
            _animator.SetBool(IsLaserActiveParam, true);

        if (_currentLaser != null)
            Destroy(_currentLaser.gameObject);

        _currentLaser = Instantiate(
            _laserPrefab,
            _mouthPoint.position,
            _mouthPoint.rotation
        );

        _currentLaser.Activate(_mouthPoint);
    }

    public void OnLaserEnd()
    {
        StopLaser();
    }

    private void StopLaser()
    {
        if (_animator != null)
            _animator.SetBool(IsLaserActiveParam, false);

        if (_currentLaser != null)
        {
            _currentLaser.Deactivate();
            Destroy(_currentLaser.gameObject);
            _currentLaser = null;
        }
    }

    private void PlayAnimation(string animationName)
    {
        if (_animator == null)
            return;

        _animator.Play(animationName, 0, 0f);
    }

    private IEnumerator WaitUntilTime(float targetTime)
    {
        while (Time.timeSinceLevelLoad < targetTime)
            yield return null;
    }
}