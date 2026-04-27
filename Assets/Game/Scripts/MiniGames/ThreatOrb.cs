using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class ThreatOrb : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CircleCollider2D _damageCollider;

    private Coroutine _activationCoroutine;

    private void Awake()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_damageCollider == null)
            _damageCollider = GetComponent<CircleCollider2D>();

        _damageCollider.isTrigger = true;
        _damageCollider.enabled = false;
    }

    public void Initialize(float fadeInDuration)
    {
        if (_activationCoroutine != null)
            StopCoroutine(_activationCoroutine);

        _activationCoroutine = StartCoroutine(ActivationRoutine(fadeInDuration));
    }

    private IEnumerator ActivationRoutine(float fadeInDuration)
    {
        SetAlpha(0f);
        _damageCollider.enabled = false;

        float timer = 0f;

        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Clamp01(timer / fadeInDuration);
            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(1f);

     

        Destroy(gameObject);
    }

    private void SetAlpha(float alpha)
    {
        Color color = _spriteRenderer.color;
        color.a = alpha;
        _spriteRenderer.color = color;
    }
}