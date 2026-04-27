using System;
using UnityEngine;

[RequireComponent(typeof(IDamagable))]

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private IDamagable _damagable;

    public event Action<float> HealthChanged;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _damagable = GetComponent<IDamagable>();

        _damagable.Damaged += DamageHealth;
    }

    private void OnDisable()
    {
        _damagable.Damaged -= DamageHealth;
    }

    public void DamageHealth(float damage)
    {
        if (damage >= 0)
        {
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);

            HealthChanged?.Invoke(CurrentHealth);
        }
    }

    public void Healing(float healAmount)
    {
        if (healAmount >= 0)
        {
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);

            HealthChanged?.Invoke(CurrentHealth);
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = _maxHealth;
    }
}