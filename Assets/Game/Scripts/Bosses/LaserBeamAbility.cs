using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LaserBeamAbility : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private int _damage = 1;

    [Header("References")]
    [SerializeField] private Collider2D _damageCollider;
    [SerializeField] private Transform _laserStartPoint;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // урон потом добавишь
    }
}