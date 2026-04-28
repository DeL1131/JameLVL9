using UnityEngine;

public class OrbitBullet : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _damage = 10f;

    private Transform _center;
    private float _radius;
    private float _rotationSpeed;
    private float _angle;

    public void Initialize(Transform center, float radius, float rotationSpeed, float startAngle)
    {
        _center = center;
        _radius = radius;
        _rotationSpeed = rotationSpeed;
        _angle = startAngle;
    }

    private void Update()
    {
        if (_center == null)
        {
            Destroy(gameObject);
            return;
        }

        _angle += _rotationSpeed * Time.deltaTime;

        float radians = _angle * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(
            Mathf.Cos(radians),
            Mathf.Sin(radians)) * _radius;

        transform.position = (Vector2)_center.position + offset;
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