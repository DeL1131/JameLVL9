using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerDeathEffect : MonoBehaviour
{
    [Header("Death Settings")]
    [SerializeField] private float _deathDuration = 2.5f;
    [SerializeField] private bool _destroyAfterDeath = false;
    

    [Header("Shake Settings")]
    [SerializeField] private float _shakeDistance = 0.04f;

    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private MonoBehaviour _playerController;

    [SerializeField] private BulletPatternRunner _bulletPatternRunner;
    private Coroutine _deathCoroutine;
    private Vector3 _startPosition;

    private void Awake()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        _startPosition = transform.localPosition;
    }

    public void PlayDeath()
    {
        if (_deathCoroutine != null)
            return;
        _bulletPatternRunner.Stop();
        DisableControl();

        _deathCoroutine = StartCoroutine(DeathRoutine());
    }

    private void DisableControl()
    {
        if (_playerController != null)
            _playerController.enabled = false;
    }

    private IEnumerator DeathRoutine()
    {
        float timer = 0f;
        Color startColor = _spriteRenderer.color;

        while (timer < _deathDuration)
        {
            timer += Time.deltaTime;

            float progress = timer / _deathDuration;

            Shake();

            float alpha = Mathf.Lerp(1f, 0f, progress);
            _spriteRenderer.color = new Color(
                startColor.r,
                startColor.g,
                startColor.b,
                alpha
            );

            yield return null;
        }

        transform.localPosition = _startPosition;
        _spriteRenderer.color = new Color(
            startColor.r,
            startColor.g,
            startColor.b,
            0f
        );

        if (_destroyAfterDeath)
            Destroy(gameObject);
    }

    private void Shake()
    {
        float x = Random.Range(-_shakeDistance, _shakeDistance);
        float y = Random.Range(-_shakeDistance, _shakeDistance);

        transform.localPosition = _startPosition + new Vector3(x, y, 0f);
    }
}