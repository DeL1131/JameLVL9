using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LaserBeamAbility : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _damageInterval = 1f;
    [SerializeField] private LayerMask _playerMask;

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed = 10f;

    [Header("References")]
    [SerializeField] private Collider2D _damageCollider;
    [SerializeField] private Transform _laserStartPoint;

    private float _damageTimer;
    private Transform _firePoint;
    private int _rotationDirection;

    private void Awake()
    {
        if (_damageCollider == null)
            _damageCollider = GetComponent<Collider2D>();

        _damageCollider.isTrigger = true;
        _damageCollider.enabled = false;
    }

    private void Update()
    {
        if (_firePoint == null)
            return;

        RotateLaser();
        SnapStartPointToFirePoint();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _playerMask) == 0)
            return;

        if (other.TryGetComponent(out IDamagable iDamageble) == false)
            return;

        _damageTimer -= Time.deltaTime;

        if (_damageTimer > 0f)
            return;

        Debug.Log($"Игрок получил {_damage} урона от лазера");

        iDamageble.TakeDamage(_damage);

        _damageTimer = _damageInterval;
    }

    public void Activate(Transform firePoint)
    {
        if (firePoint == null)
            return;

        _firePoint = firePoint;

        _rotationDirection = Random.value < 0.5f ? -1 : 1;

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        SnapStartPointToFirePoint();

        _damageCollider.enabled = true;
    }

    public void Deactivate()
    {
        _damageCollider.enabled = false;
    }

    private void RotateLaser()
    {
        transform.Rotate(0f, 0f, _rotationDirection * _rotationSpeed * Time.deltaTime);
    }

    private void SnapStartPointToFirePoint()
    {
        if (_laserStartPoint == null)
        {
            transform.position = _firePoint.position;
            return;
        }

        Vector3 offset = _laserStartPoint.position - transform.position;
        transform.position = _firePoint.position - offset;
    }
}