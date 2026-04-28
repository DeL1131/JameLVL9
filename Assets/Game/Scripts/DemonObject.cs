using UnityEngine;

public class DemonObject : InteractableObject
{
    [SerializeField] private GameFlowManager _gameFlowManager;

    public static DemonObject instance;
    void Awake()
    {
        instance = this;
    }
    protected override void Interact()
    {
        _gameFlowManager.LoadSceneByName("MiniGame_01");
    }
}
