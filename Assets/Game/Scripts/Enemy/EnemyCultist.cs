using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    private Health _health;

    public event Action<float> Damaged;
    public event Action Died;

    private void Awake()
    {
        _health = GetComponent<Health>();      
    }

    private void OnEnable()
    {
        _health.HealthChanged += IsAlive;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= IsAlive;
    }

    public void TakeDamage(float damage)
    {
        Damaged?.Invoke(damage);
    }

    public void IsAlive(float health)
    {
        if(health <= 0)
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}