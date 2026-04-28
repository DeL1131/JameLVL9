using UnityEngine;

public readonly struct BulletSpawnData
{
    public readonly Vector2 Position;
    public readonly Vector2 Direction;
    public readonly float Speed;
    public readonly int Damage;
    public readonly float LifeTime;
    public readonly LayerMask TargetMask;

    public BulletSpawnData(
        Vector2 position,
        Vector2 direction,
        float speed,
        int damage,
        float lifeTime,
        LayerMask targetMask)
    {
        Position = position;
        Direction = direction;
        Speed = speed;
        Damage = damage;
        LifeTime = lifeTime;
        TargetMask = targetMask;
    }
}