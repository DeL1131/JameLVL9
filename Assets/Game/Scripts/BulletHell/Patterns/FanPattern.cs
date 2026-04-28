using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPattern : MonoBehaviour, IBulletPattern
{
    public enum ShootDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    [Header("Allowed Directions")]
    [SerializeField]
    private List<ShootDirection> _allowedDirections = new()
    {
        ShootDirection.Left,
        ShootDirection.Down,
        ShootDirection.Right
    };

    [Header("Pattern")]
    [SerializeField] private int _bulletCount = 5;
    [SerializeField] private float _spreadAngle = 60f;

    [Header("Bullet")]
    [SerializeField] private float _bulletSpeed = 7f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private LayerMask _targetMask;

    public IEnumerator Execute(BulletShootContext context, BulletSpawner spawner)
    {
        Vector2 baseDirection = GetClosestAllowedDirection(context);

        if (_bulletCount <= 1)
        {
            SpawnBullet(context, spawner, baseDirection);
            yield break;
        }

        float startAngle = -_spreadAngle * 0.5f;
        float angleStep = _spreadAngle / (_bulletCount - 1);

        for (int i = 0; i < _bulletCount; i++)
        {
            float angleOffset = startAngle + angleStep * i;
            Vector2 direction = RotateVector(baseDirection, angleOffset);

            SpawnBullet(context, spawner, direction);
        }
    }

    private Vector2 GetClosestAllowedDirection(BulletShootContext context)
    {
        if (_allowedDirections.Count == 0)
            return Vector2.down;

        if (context.Target == null)
            return ToVector(_allowedDirections[0]);

        Vector2 toTarget = ((Vector2)context.Target.position - context.FirePosition).normalized;

        ShootDirection bestDirection = _allowedDirections[0];
        float bestDot = -1f;

        for (int i = 0; i < _allowedDirections.Count; i++)
        {
            Vector2 direction = ToVector(_allowedDirections[i]);
            float dot = Vector2.Dot(toTarget, direction);

            if (dot > bestDot)
            {
                bestDot = dot;
                bestDirection = _allowedDirections[i];
            }
        }

        return ToVector(bestDirection);
    }

    private Vector2 ToVector(ShootDirection direction)
    {
        switch (direction)
        {
            case ShootDirection.Up:
                return Vector2.up;

            case ShootDirection.Down:
                return Vector2.down;

            case ShootDirection.Left:
                return Vector2.left;

            case ShootDirection.Right:
                return Vector2.right;

            default:
                return Vector2.down;
        }
    }

    private void SpawnBullet(BulletShootContext context, BulletSpawner spawner, Vector2 direction)
    {
        BulletSpawnData data = new BulletSpawnData(
            context.FirePosition,
            direction,
            _bulletSpeed,
            _damage,
            _lifeTime,
            _targetMask);

        spawner.Spawn(data);
    }

    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radians = angle * Mathf.Deg2Rad;

        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos);
    }
}