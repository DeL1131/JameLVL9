using System;

public interface IDamagable
{
    public event Action<float> Damaged;
    public void TakeDamage(float damage);    
}