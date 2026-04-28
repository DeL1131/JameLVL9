using UnityEngine;

public class PlayerShooter2D : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private BulletSpawner _spawner;
    [SerializeField] private Transform _firePoint;

    [Header("Shoot")]
    [SerializeField] private float _shotsPerSecond = 8f;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private LayerMask _targetMask;
    private float _nextShootTime;
    private float _shootTimer;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;

        if (_spawner == null)
            _spawner = GetComponent<BulletSpawner>();

        if (_firePoint == null)
            _firePoint = transform;

    }

    private void Update()
    {
        if (Input.GetMouseButton(0) == false)
            return;

        if (Time.time < _nextShootTime)
            return;

        Shoot();

        _nextShootTime = Time.time + 1f / Mathf.Max(_shotsPerSecond, 0.01f);
    }

    private void Shoot()
    {
        Vector2 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePosition = _firePoint.position;
        Vector2 direction = (mouseWorldPosition - firePosition).normalized;

        BulletSpawnData data = new BulletSpawnData(
            firePosition,
            direction,
            _bulletSpeed,
            _damage,
            _lifeTime,
            _targetMask);

        _spawner.Spawn(data);
    }
}