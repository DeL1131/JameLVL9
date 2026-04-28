using UnityEngine;

public class DemonObject : InteractableObject
{
    public static DemonObject instance;
    void Awake()
    {
        instance = this;
    }
    protected override void Interact()
    {

    }
}
