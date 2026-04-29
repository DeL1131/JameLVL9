using UnityEngine;

public class PedestalObject : InteractableObject
{
    public static PedestalObject instance;

    private AudioSource _audioSource;

    [SerializeField] private GameObject demonObject;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        instance = this;
    }
    protected override void Interact()
    {
        if (RitualConditionsUI.instance.GetIfAllTrue() && !hasSpawnedDemon)
        {
            hasSpawnedDemon = true;
            demonObject.SetActive(true);
        }
    }
    private bool hasSpawnedDemon = false;

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
}
