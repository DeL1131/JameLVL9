using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private LayerMask _targetMask;

    private Vector2 _direction;
    private float _speed;
    private float _lifeTimer;

    public void Initialize(Vector2 direction, float speed, int damage, float lifeTime, LayerMask targetMask)
    {
        _direction = direction.normalized;
        _speed = speed;
        _damage = damage;
        _lifeTime = lifeTime;
        _targetMask = targetMask;

        _lifeTimer = 0f;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);

        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject.layer, _targetMask) == false)
            return;

        if (other.TryGetComponent(out IDamageable damageable) == false)
            damageable = other.GetComponentInParent<IDamageable>();

        if (damageable == null)
            return;

        damageable.TakeDamage(_damage);
        Destroy(gameObject);
    }

    private bool IsInLayerMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}