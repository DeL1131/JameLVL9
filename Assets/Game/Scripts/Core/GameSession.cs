using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    [SerializeField] private bool _logStateChanges = true;

    private ItemObject.ItemType[] _pentagramItems = new ItemObject.ItemType[5];
    private bool _isGameStarted;
    private bool _hasRitualState;
    private bool _hasSpawnedDemon;
    private bool _hasBossFightResult;
    private bool _hasWonBossFight;
    private readonly Dictionary<string, bool> _torchStates = new Dictionary<string, bool>();

    public bool IsGameStarted => _isGameStarted;
    public bool HasRitualState => _hasRitualState;
    public bool HasSpawnedDemon => _hasSpawnedDemon;
    public bool HasBossFightResult => _hasBossFightResult;
    public bool HasWonBossFight => _hasWonBossFight;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (Instance != null)
            return;

        GameObject gameSessionObject = new GameObject(nameof(GameSession));
        gameSessionObject.AddComponent<GameSession>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveRitualState(ItemObject.ItemType[] pentagramItems, bool hasSpawnedDemon)
    {
        if (pentagramItems == null)
        {
            Debug.LogWarning("Cannot save ritual state because pentagram items are null.");
            return;
        }

        EnsurePentagramBufferSize(pentagramItems.Length);
        System.Array.Copy(pentagramItems, _pentagramItems, pentagramItems.Length);

        _hasSpawnedDemon = hasSpawnedDemon;
        _hasRitualState = true;
        MarkGameStarted();

        if (_logStateChanges)
            Debug.Log("Ritual state saved before leaving the ritual scene.");
    }

    public void MarkGameStarted()
    {
        if (_isGameStarted)
            return;

        _isGameStarted = true;

        if (_logStateChanges)
            Debug.Log("Game session marked as started.");
    }

    public ItemObject.ItemType[] GetPentagramItems()
    {
        ItemObject.ItemType[] copy = new ItemObject.ItemType[_pentagramItems.Length];
        System.Array.Copy(_pentagramItems, copy, _pentagramItems.Length);
        return copy;
    }

    public void RecordBossFightResult(bool hasWon)
    {
        _hasBossFightResult = true;
        _hasWonBossFight = hasWon;

        if (_logStateChanges)
            Debug.Log(hasWon ? "Boss fight won. Returning to ritual state." : "Boss fight lost. Returning to ritual state.");
    }

    public void SaveTorchState(string torchId, bool isLit)
    {
        if (string.IsNullOrWhiteSpace(torchId))
        {
            Debug.LogWarning("Cannot save torch state because torch id is empty.");
            return;
        }

        _torchStates[torchId] = isLit;
        MarkGameStarted();

        if (_logStateChanges)
            Debug.Log($"Torch state saved: {torchId} = {(isLit ? "lit" : "unlit")}.");
    }

    public bool TryGetTorchState(string torchId, out bool isLit)
    {
        isLit = false;

        if (string.IsNullOrWhiteSpace(torchId))
            return false;

        return _torchStates.TryGetValue(torchId, out isLit);
    }

    private void EnsurePentagramBufferSize(int size)
    {
        if (_pentagramItems.Length == size)
            return;

        _pentagramItems = new ItemObject.ItemType[size];
    }
}
