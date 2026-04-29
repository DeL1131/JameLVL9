using UnityEngine;
using TMPro;

public abstract class InteractableObject : MonoBehaviour
{
    [Header("UI")]
    private TextMeshPro interactionText;
    [SerializeField] private string message = "Press E to interact";

    [Header("Settings")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [SerializeField] protected AudioSource _audioSource;

    private bool playerInRange = false;

    protected virtual void Start()
    {
        interactionText = Instantiate(PrefabReference.instance.tmpText3D, transform).GetComponent<TextMeshPro>();
        interactionText.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        //interactionText.transform.localPosition = new Vector3(0, -0.2f, 0);
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
            interactionText.text = message;
        }
    }

    protected virtual void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            playerInRange = true;

            if (interactionText != null)
            {
                interactionText.text = message;
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    protected abstract void Interact();
}