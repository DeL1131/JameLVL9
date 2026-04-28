using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    void Awake()
    {
        instance = this;
    }
    Rigidbody2D rb;
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

        if (rb.linearVelocity.magnitude > friction * Time.fixedDeltaTime)
        {
            rb.AddForce(-rb.linearVelocity.normalized * friction);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }


        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

    }
    void Update()
    {
        DropItem();
    }
    public ItemObject carriedItem;
    public void PickUpItem(ItemObject item)
    {
        if (carriedItem != null)
        {
            carriedItem.Drop();
        }
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
