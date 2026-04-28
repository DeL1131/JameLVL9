using System.Collections;
using UnityEngine;

public class SingleShotPattern : MonoBehaviour, IBulletPattern
{
    [SerializeField] private float _bulletSpeed = 8f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private LayerMask _targetMask;

    public IEnumerator Execute(BulletShootContext context, BulletSpawner spawner)
    {
        BulletSpawnData data = new BulletSpawnData(
            context.FirePosition,
            context.ForwardDirection,
            _bulletSpeed,
            _damage,
            _lifeTime,
            _targetMask);

        spawner.Spawn(data);

        yield break;
    }
}