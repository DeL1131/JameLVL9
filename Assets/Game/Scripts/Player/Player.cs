using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerOld : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxSpeed = 1.5f;
    [SerializeField] private float acceleration = 350;
    [SerializeField] private float friction = 250;

    private Rigidbody2D rb;
    private Animator _animator;
    private AudioSource _audioSource;

    public event Action<float> Damaged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Vector2 wasd = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        rb.AddForce(wasd * acceleration);

        if (wasd == Vector2.zero)
            rb.AddForce(-rb.linearVelocity.normalized * friction * Time.fixedDeltaTime);

        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        UpdateAnimation(wasd);
    }

    private void UpdateAnimation(Vector2 input)
    {
        _animator.SetFloat("Speed", input != Vector2.zero ? 1f : 0f);
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    }

    private void PlayDamagedSound()
    {
        if (_audioSource != null)
            _audioSource.Play();
    }

    public void TakeDamage(float damage)
    {
        Damaged?.Invoke(damage);
        PlayDamagedSound();
    }
}