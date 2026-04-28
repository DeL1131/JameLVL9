using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchObject : InteractableObject
{

    private void Awake()
    {
        torchAnimator = GetComponent<Animator>();
    }
    public bool isLit { get; private set; } = true;
    [SerializeField] private Light2D torchLight;
    private Animator torchAnimator;
    protected override void Interact()
    {
        if (isLit)
        {
            torchLight.enabled = false;
            isLit = false;
            torchAnimator.SetBool("IsLit", false);
        }
        else
        {
            torchLight.enabled = true;
            isLit = true;
            torchAnimator.SetBool("IsLit", true);
        }
    }
}
