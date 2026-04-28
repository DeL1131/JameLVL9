using UnityEngine;

public class ItemObject : InteractableObject
{
    public enum ItemType
    {
        empty,
        Chalice,
        Mirror,
        BloodFlask,
        WaterFlask,
        Skull,
        Bone,
        Grimoir,
        Orb,
        VodooDoll,
        Dagger,
    }

    [Header("Item")]
    [SerializeField] private ItemType itemType;


    [Header("Hover Settings")]
    [SerializeField] private float hoverAmplitude = 0.05f;
    [SerializeField] private float hoverSpeed = 2f;

    [Header("Return Settings")]
    [SerializeField] private float returnSpeed = 5f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private bool isPickedUp = false;
    private bool isReturning = false;

    private float hoverOffset;

    protected override void Start()
    {
        base.Start();
        targetPosition = transform.position;
        initialPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        if (isPickedUp)
        {
            FollowPlayer();
            return;
        }

        if (isReturning)
        {
            ReturnToInitialPosition();
        }
        else
        {
            Hover();
        }
    }

    protected override void Interact()
    {
        isPickedUp = true;
        isReturning = false;
        Player.instance.PickUpItem(this);
        if (isInPentagram)
        {
            ResetTargetPos();
            PentagramObject.instance.RemoveItem(itemType);
        }
    }

    private void FollowPlayer()
    {
        if (Player.instance == null) return;

        transform.position = Player.instance.transform.position + new Vector3(0, 0.5f, 0);
    }

    private void ReturnToInitialPosition()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            returnSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isReturning = false;
        }
    }

    private void Hover()
    {
        hoverOffset += Time.deltaTime * hoverSpeed;

        float yOffset = Mathf.Sin(hoverOffset) * hoverAmplitude;

        transform.position = targetPosition + new Vector3(0, yOffset, 0);
    }

    public void Drop()
    {
        isPickedUp = false;
        isReturning = true;
        Player.instance.carriedItem = null;
    }
    private bool isInPentagram = false;
    public void SetPentagramTargetPos(Vector2 pos)
    {
        targetPosition = pos;
        isInPentagram = true;
    }
    public void ResetTargetPos()
    {
        targetPosition = initialPosition;
        isInPentagram = false;
    }
    public bool IsPickedUp()
    {
        return isPickedUp;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}