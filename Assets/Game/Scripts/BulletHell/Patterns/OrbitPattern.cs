using System.Collections;
using UnityEngine;

public class OrbitPattern : MonoBehaviour, IBulletPattern
{
    [Header("Center")]
    [SerializeField] private OrbitCenter _centerPrefab;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _lifeTime = 6f;

    [Header("Orbit Bullets")]
    [SerializeField] private OrbitBullet _orbitBulletPrefab;
    [SerializeField] private int _bulletCount = 6;
    [SerializeField] private float _radius = 1.5f;
    [SerializeField] private float _rotationSpeed = 180f;

    public IEnumerator Execute(BulletShootContext context, BulletSpawner spawner)
    {
        if (_centerPrefab == null || _orbitBulletPrefab == null)
            yield break;

        Vector2 direction = context.ForwardDirection;

        if (context.Target != null)
        {
            direction = ((Vector2)context.Target.position - context.FirePosition).normalized;
        }

        OrbitCenter center = Instantiate(
            _centerPrefab,
            context.FirePosition,
            Quaternion.identity);

        center.Initialize(direction, _moveSpeed, _lifeTime);

        float angleStep = 360f / _bulletCount;

        for (int i = 0; i < _bulletCount; i++)
        {
            float angle = i * angleStep;

            OrbitBullet bullet = Instantiate(
                _orbitBulletPrefab,
                context.FirePosition,
                Quaternion.identity);

            bullet.Initialize(
                center.transform,
                _radius,
                _rotationSpeed,
                angle);
        }

        yield break;
    }
}