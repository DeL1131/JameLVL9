using System;
using UnityEngine;

public class PlayerOld : MonoBehaviour, IDamageable
{
    Rigidbody2D rb;

    public event Action<float> Damaged;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    [SerializeField] private float maxSpeed = 1.5f;
    [SerializeField] private float acceleration = 350;
    [SerializeField] private float friction = 250;
    void FixedUpdate()
    {
        Vector2 wasd = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


        rb.AddForce(wasd * acceleration);

        if (wasd == Vector2.zero)
            rb.AddForce(-rb.linearVelocity.normalized * friction * Time.fixedDeltaTime);

        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

    }

    public void TakeDamage(float damage)
    {
        Damaged?.Invoke(damage);
    }
}