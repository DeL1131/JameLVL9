using System.Collections;
using UnityEngine;

public class AimedShotPattern : MonoBehaviour, IBulletPattern
{
    [SerializeField] private float _bulletSpeed = 8f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private LayerMask _targetMask;

    public IEnumerator Execute(BulletShootContext context, BulletSpawner spawner)
    {
        if (context.Target == null)
            yield break;

        Vector2 direction = ((Vector2)context.Target.position - context.FirePosition).normalized;

        BulletSpawnData data = new BulletSpawnData(
            context.FirePosition,
            direction,
            _bulletSpeed,
            _damage,
            _lifeTime,
            _targetMask);

        spawner.Spawn(data);

        yield break;
    }
}