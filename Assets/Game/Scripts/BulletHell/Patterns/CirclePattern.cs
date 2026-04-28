using System.Collections;
using UnityEngine;

public class CirclePattern : MonoBehaviour, IBulletPattern
{
    [SerializeField] private int _bulletCount = 16;
    [SerializeField] private float _bulletSpeed = 6f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private LayerMask _targetMask;

    public IEnumerator Execute(BulletShootContext context, BulletSpawner spawner)
    {
        float angleStep = 360f / _bulletCount;

        for (int i = 0; i < _bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = GetDirectionFromAngle(angle);

            BulletSpawnData data = new BulletSpawnData(
                context.FirePosition,
                direction,
                _bulletSpeed,
                _damage,
                _lifeTime,
                _targetMask);

            spawner.Spawn(data);
        }

        yield break;
    }

    private Vector2 GetDirectionFromAngle(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;

        return new Vector2(
            Mathf.Cos(radians),
            Mathf.Sin(radians));
    }
}