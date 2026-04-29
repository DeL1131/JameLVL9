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
        if (GameSession.Instance != null && PentagramObject.instance != null && PedestalObject.instance != null)
            GameSession.Instance.SaveRitualState(PentagramObject.instance.GetInventorySnapshot(), PedestalObject.instance.HasSpawnedDemon);

        if (_gameFlowManager == null)
            _gameFlowManager = GameFlowManager.Instance;

        if (_gameFlowManager == null)
        {
            Debug.LogWarning("DemonObject could not find a GameFlowManager. Boss scene was not loaded.");
            return;
        }

        _gameFlowManager.LoadMiniGame(1);
    }
}
