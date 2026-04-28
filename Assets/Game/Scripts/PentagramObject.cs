using UnityEngine;

public class PentagramObject : InteractableObject
{

    public static PentagramObject instance;
    void Awake()
    {
        instance = this;
        SetPentagramPoints();
    }
    [SerializeField] private ItemObject.ItemType[] inventory = new ItemObject.ItemType[5];
    private Vector2[] pentagramPoints = new Vector2[5];
    protected override void Interact()
    {
        if (Player.instance.carriedItem != null) //and enough capacity
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == ItemObject.ItemType.empty)
                {
                    inventory[i] = Player.instance.carriedItem.GetItemType();
                    Player.instance.carriedItem.SetPentagramTargetPos(pentagramPoints[i]);
                    Player.instance.carriedItem.Drop();

                    break;
                }
            }
        }
    }
    public void RemoveItem(ItemObject.ItemType itemType)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == itemType)
            {
                inventory[i] = ItemObject.ItemType.empty;
                break;
            }
        }
    }
    private void SetPentagramPoints()
    {
        float radius = 1.5f;
        for (int i = 0; i < 5; i++)
        {
            float angle = i * Mathf.PI * 2 / 5 + Mathf.PI / 2; // Start at the top
            pentagramPoints[i] = new Vector2(
                transform.position.x + Mathf.Cos(angle) * radius,
                transform.position.y + Mathf.Sin(angle) * radius
            );
        }
    }
    public bool HasItem(ItemObject.ItemType itemType)
    {
        foreach (var item in inventory)
        {
            if (item == itemType) return true;
        }
        return false;
    }
}
