using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class OccultistMouseDeltaMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Collider2D _playArea;

    [Header("Movement")]
    [SerializeField] private float _mouseSensitivity = 0.015f;
    [SerializeField] private float _maxMoveSpeed = 12f;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_camera == null)
            _camera = Camera.main;

        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        Vector2 movement = mouseDelta * _mouseSensitivity / Time.deltaTime;
        _moveVelocity = Vector2.ClampMagnitude(movement, _maxMoveSpeed);
    }

    private void FixedUpdate()
    {
        Vector2 nextPosition = _rigidbody.position + _moveVelocity * Time.fixedDeltaTime;

        if (_playArea != null)
            nextPosition = _playArea.ClosestPoint(nextPosition);

        _rigidbody.MovePosition(nextPosition);
    }
}