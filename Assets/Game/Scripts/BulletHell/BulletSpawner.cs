using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    public void Spawn(BulletSpawnData data)
    {
        if (_bulletPrefab == null)
        {
            Debug.LogError($"{nameof(BulletSpawner)}: bullet prefab is not assigned.", this);
            return;
        }

        Bullet bullet = Instantiate(_bulletPrefab, data.Position, Quaternion.identity);

        bullet.Initialize(
            data.Direction,
            data.Speed,
            data.Damage,
            data.LifeTime,
            data.TargetMask);
    }
}