using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandleObject : InteractableObject
{
    private void Awake()
    {
        candleAnimator = GetComponent<Animator>();
    }

    public bool isLit { get; private set; } = true;
    [SerializeField] private Light2D candleLight;
    private Animator candleAnimator;
    protected override void Interact()
    {
        if (isLit)
        {
            candleLight.enabled = false;
            isLit = false;
            candleAnimator.SetBool("IsLit", false);
            _audioSource.Play();
        }
        else
        {
            candleLight.enabled = true;
            isLit = true;
            candleAnimator.SetBool("IsLit", true);
            _audioSource.Play();
        }
    }
}
