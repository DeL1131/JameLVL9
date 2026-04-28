using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BulletPatternRunner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BulletSpawner _spawner;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _target;

    [Header("Patterns")]
    [SerializeField] private List<MonoBehaviour> _patternComponents = new();

    [Header("Settings")]
    [SerializeField] private bool _playOnStart;
    [SerializeField] private bool _loop;
    [SerializeField] private float _startDelay = 2f;
    [SerializeField] private float _delayBetweenPatterns = 0.5f;

    private readonly List<IBulletPattern> _patterns = new();
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        if(_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (_spawner == null)
            _spawner = GetComponent<BulletSpawner>();

        CachePatterns();
    }

    private void Start()
    {
        if (_playOnStart)
            Play();
    }

    private void OnDisable()
    {
        Stop();
    }

    public void Play()
    {
        if (_patterns.Count == 0 || _spawner == null)
            return;

        Stop();

        _shootCoroutine = StartCoroutine(PlayRoutine());
    }

    public void Stop()
    {
        if (_shootCoroutine == null)
            return;

        StopCoroutine(_shootCoroutine);
        _shootCoroutine = null;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private IEnumerator PlayRoutine()
    {
        if (_startDelay > 0f)
            yield return new WaitForSeconds(_startDelay);

        do
        {
            BulletShootContext context = new BulletShootContext(
                transform,
                _firePoint,
                _target);

            for (int i = 0; i < _patterns.Count; i++)
            {
                yield return _patterns[i].Execute(context, _spawner);

                if (_delayBetweenPatterns > 0f)
                    yield return new WaitForSeconds(_delayBetweenPatterns);
            }

        } while (_loop);

        _shootCoroutine = null;
    }

    private void CachePatterns()
    {
        _patterns.Clear();

        for (int i = 0; i < _patternComponents.Count; i++)
        {
            if (_patternComponents[i] == null)
                continue;

            if (_patternComponents[i] is IBulletPattern pattern)
            {
                _patterns.Add(pattern);
                continue;
            }

            Debug.LogError(
                $"{_patternComponents[i].GetType().Name} must implement {nameof(IBulletPattern)}.",
                this);
        }
    }
}