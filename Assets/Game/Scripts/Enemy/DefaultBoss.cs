using UnityEngine;
using System;

public class DefaultEnemy : MonoBehaviour,IDamageable
{
    public event Action<float> Damaged;

    public void TakeDamage(float damage)
    {
        Damaged?.Invoke(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(50);
        }
    }
}