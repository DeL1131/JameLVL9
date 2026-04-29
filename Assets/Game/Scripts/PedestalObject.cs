using UnityEngine;

public class PedestalObject : InteractableObject
{
    public static PedestalObject instance;

    [SerializeField] private GameObject demonObject;
    private bool hasSpawnedDemon = false;

    public bool HasSpawnedDemon => hasSpawnedDemon;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        RestoreSavedState();
    }

    protected override void Interact()
    {
        if (RitualConditionsUI.instance.GetIfAllTrue() && !hasSpawnedDemon)
        {
            SetDemonSpawned(true);
        }
    }

    //[SerializeField] private GameObject infoScroll;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        PrefabReference.instance.infoScroll.SetActive(true);

        if (Time.timeScale != 0f)
        {
            _audioSource.Play();
        }
    }
    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        PrefabReference.instance.infoScroll.SetActive(false);
    }

    public void SetDemonSpawned(bool spawned)
    {
        hasSpawnedDemon = spawned;

        if (demonObject != null)
            demonObject.SetActive(spawned);
    }

    private void RestoreSavedState()
    {
        if (GameSession.Instance == null || GameSession.Instance.HasRitualState == false)
            return;

        SetDemonSpawned(GameSession.Instance.HasSpawnedDemon);
    }
}
