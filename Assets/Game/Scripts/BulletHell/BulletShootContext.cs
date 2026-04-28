using UnityEngine;

public readonly struct BulletShootContext
{
    public readonly Transform Shooter;
    public readonly Transform FirePoint;
    public readonly Transform Target;

    public Vector2 FirePosition => FirePoint != null ? FirePoint.position : Shooter.position;
    public Vector2 ForwardDirection => FirePoint != null ? FirePoint.right : Shooter.right;

    public BulletShootContext(Transform shooter, Transform firePoint, Transform target)
    {
        Shooter = shooter;
        FirePoint = firePoint;
        Target = target;
    }
}