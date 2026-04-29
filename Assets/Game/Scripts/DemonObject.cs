using UnityEngine;
using System.Collections;

public class DemonObject : InteractableObject
{
    [SerializeField] private GameFlowManager _gameFlowManager;

    [Header("Boss Start Messages")]
    [SerializeField] private string _bossSummonedMessage = "Who dares summon me? Pathetic mortal!";
    [SerializeField] private string _startFightMessage = "E - Start fight";
    [SerializeField] private bool _showSummonedMessageAlways = true;

    [Header("Boss Text Style")]
    [SerializeField] private string _bossTextColor = "red";

    [Header("Boss Result Messages")]
    [SerializeField] private string _bossDefeatedMessage = "Ты доказал свою силу.";
    [SerializeField] private string _bossDefeatedInteractionMessage = "Ты доказал свою силу. Нет нужды нам сражаться вновь.";
    [SerializeField] private string _bossLostMessage = "Ничтожный человечишка, неужели ты думаешь, что меня так легко одолеть?";
    [SerializeField] private float _temporaryMessageDuration = 3f;
    [SerializeField] private bool _showBossResultMessageAlways = true;

    private Coroutine _temporaryMessageCoroutine;

    public static DemonObject instance;
    void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        RefreshBossMessage();
    }

    protected override void Interact()
    {
        if (GameSession.Instance != null && GameSession.Instance.HasBossFightResult && GameSession.Instance.HasWonBossFight)
        {
            ShowTemporaryDefeatedInteractionMessage();
            return;
        }

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

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        RefreshBossMessageForPlayerRange();
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        RefreshBossMessage();
    }

    private void RefreshBossMessage()
    {
        if (TryRefreshBossResultMessage())
            return;

        SetBossInteractionMessage(_bossSummonedMessage);

        if (_showSummonedMessageAlways)
            SetInteractionMessageVisible(true);
    }

    private void RefreshBossMessageForPlayerRange()
    {
        if (GameSession.Instance != null && GameSession.Instance.HasBossFightResult && GameSession.Instance.HasWonBossFight)
        {
            TryRefreshBossResultMessage();
            return;
        }

        SetBossInteractionMessage(_startFightMessage);
        SetInteractionMessageVisible(true);
    }

    private bool TryRefreshBossResultMessage()
    {
        if (GameSession.Instance == null || GameSession.Instance.HasBossFightResult == false)
            return false;

        SetBossInteractionMessage(GameSession.Instance.HasWonBossFight ? _bossDefeatedMessage : _bossLostMessage);

        if (_showBossResultMessageAlways)
            SetInteractionMessageVisible(true);

        return true;
    }

    private void ShowTemporaryDefeatedInteractionMessage()
    {
        if (_temporaryMessageCoroutine != null)
            StopCoroutine(_temporaryMessageCoroutine);

        _temporaryMessageCoroutine = StartCoroutine(ShowTemporaryMessage(_bossDefeatedInteractionMessage, _bossDefeatedMessage));
    }

    private IEnumerator ShowTemporaryMessage(string temporaryMessage, string restoredMessage)
    {
        SetBossInteractionMessage(temporaryMessage);
        SetInteractionMessageVisible(true);

        yield return new WaitForSeconds(_temporaryMessageDuration);

        SetBossInteractionMessage(restoredMessage);

        if (_showBossResultMessageAlways || PlayerInRange)
            SetInteractionMessageVisible(true);
    }

    private bool HasBossResultMessage()
    {
        return GameSession.Instance != null && GameSession.Instance.HasBossFightResult;
    }

    private void SetBossInteractionMessage(string text)
    {
        SetInteractionMessage($"<color={_bossTextColor}>{text}</color>");
    }
}
