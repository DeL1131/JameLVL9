using System.Collections;
using UnityEngine;

public class SpiralPattern : MonoBehaviour, IBulletPattern
{
    [SerializeField] private int _shotsCount = 60;
    [SerializeField] private float _angleStep = 12f;
    [SerializeField] private float _delayBetweenShots = 0.05f;

    [SerializeField] private float _bulletSpeed = 6f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private LayerMask _targetMask;

    public IEnumerator Execute(BulletShootContext context, BulletSpawner spawner)
    {
        float angle = 0f;

        for (int i = 0; i < _shotsCount; i++)
        {
            Vector2 direction = GetDirectionFromAngle(angle);

            BulletSpawnData data = new BulletSpawnData(
                context.FirePosition,
                direction,
                _bulletSpeed,
                _damage,
                _lifeTime,
                _targetMask);

            spawner.Spawn(data);

            angle += _angleStep;

            yield return new WaitForSeconds(_delayBetweenShots);
        }
    }

    private Vector2 GetDirectionFromAngle(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;

        return new Vector2(
            Mathf.Cos(radians),
            Mathf.Sin(radians));
    }
}