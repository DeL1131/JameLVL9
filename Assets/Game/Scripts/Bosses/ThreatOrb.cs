using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class ThreatOrb : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _damageCollider;
    [SerializeField] private Animator _animator;

    private Coroutine _lifeCoroutine;

    private void Awake()
    {
        if (_damageCollider == null)
            _damageCollider = GetComponent<CircleCollider2D>();

        if (_animator == null)
            _animator = GetComponent<Animator>();

        _damageCollider.isTrigger = true;
        _damageCollider.enabled = false;
    }

    public void Initialize()
    {
        if (_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);

        _lifeCoroutine = StartCoroutine(LifeRoutine());
    }

    private IEnumerator LifeRoutine()
    {
        _damageCollider.enabled = true;

        yield return null;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        yield return new WaitForSeconds(animationLength);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Игрок получил бы урон от ThreatOrb");


        }
    }
}