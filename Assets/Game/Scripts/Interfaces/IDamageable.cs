using System;

public interface IDamageable
{
    public event Action<float> Damaged;
    public void TakeDamage(float damage);    
}