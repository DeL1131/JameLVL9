using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class ThreatOrb : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _damage = 1;

    [Header("References")]
    [SerializeField] private CircleCollider2D _damageCollider;
    [SerializeField] private Animator _animator;

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
       
    }

    public void DealDamage()
    {
        _damageCollider.enabled = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            _damageCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y)
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable iDamagale))
            {
                Debug.Log($"Игрок получил бы {_damage} урона от ThreatOrb");
                iDamagale.TakeDamage(_damage);
            }
        }

        _damageCollider.enabled = false;
    }


    public void DestroyOrb()
    {
        Destroy(gameObject);
    }
}