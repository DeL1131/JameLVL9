using UnityEngine;
using System.Collections;

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

    protected override void Start()
    {
        base.Start();
        StartCoroutine(RestoreSavedStateNextFrame());
    }

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

    public ItemObject.ItemType[] GetInventorySnapshot()
    {
        ItemObject.ItemType[] snapshot = new ItemObject.ItemType[inventory.Length];
        System.Array.Copy(inventory, snapshot, inventory.Length);
        return snapshot;
    }

    private IEnumerator RestoreSavedStateNextFrame()
    {
        yield return null;

        if (GameSession.Instance == null || GameSession.Instance.HasRitualState == false)
            yield break;

        RestoreInventory(GameSession.Instance.GetPentagramItems());
    }

    private void RestoreInventory(ItemObject.ItemType[] savedInventory)
    {
        if (savedInventory == null)
            return;

        int itemCount = Mathf.Min(inventory.Length, savedInventory.Length);

        for (int i = 0; i < inventory.Length; i++)
            inventory[i] = ItemObject.ItemType.empty;

        ItemObject[] sceneItems = FindObjectsByType<ItemObject>(FindObjectsSortMode.None);

        for (int i = 0; i < itemCount; i++)
        {
            ItemObject.ItemType itemType = savedInventory[i];

            if (itemType == ItemObject.ItemType.empty)
                continue;

            inventory[i] = itemType;
            ItemObject sceneItem = FindSceneItem(sceneItems, itemType);

            if (sceneItem != null)
                sceneItem.SetPentagramTargetPos(pentagramPoints[i]);
        }
    }

    private ItemObject FindSceneItem(ItemObject[] sceneItems, ItemObject.ItemType itemType)
    {
        foreach (ItemObject item in sceneItems)
        {
            if (item.GetItemType() == itemType)
                return item;
        }

        Debug.LogWarning($"Saved ritual item '{itemType}' was not found in the ritual scene.");
        return null;
    }
}
