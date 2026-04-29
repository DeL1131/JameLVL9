using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }

    private Rigidbody2D _rb;
    private Animator _animator;

    [SerializeField] private float _maxSpeed = 1.5f;
    [SerializeField] private float _acceleration = 350;
    [SerializeField] private float _friction = 250;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        _rb.AddForce(input * _acceleration);

        if (_rb.linearVelocity.magnitude > _friction * Time.fixedDeltaTime)
        {
            _rb.AddForce(-_rb.linearVelocity.normalized * _friction);
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }

        if (_rb.linearVelocity.magnitude > _maxSpeed)
            _rb.linearVelocity = _rb.linearVelocity.normalized * _maxSpeed;

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        float speed = _rb.linearVelocity.magnitude;

        // параметр в Animator
        _animator.SetFloat("Speed", speed);
    }

    private void Update()
    {
        DropItem();
    }

    // ---------------- Items ----------------

    public ItemObject carriedItem;

    public void PickUpItem(ItemObject item)
    {
        if (carriedItem != null)
            carriedItem.Drop();

        carriedItem = item;
    }

    private void DropItem()
    {
        if (carriedItem != null && Input.GetKeyDown(KeyCode.Q))
        {
            carriedItem.Drop();
            carriedItem = null;
        }
    }
}