using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected float SmoothDecreaseDuration = 1f;

    protected CanvasGroup CanvasGroup;
    private float _alphaTarget;

    public event Action<string> OnButtonClicked;

    protected void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;
        _alphaTarget = 1;
        StartCoroutine(FadeCanvasGroup(_alphaTarget));
    }

    public void Close()
    {
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
        _alphaTarget = 0;
        StartCoroutine(FadeCanvasGroup(_alphaTarget));
    }

    protected void InvokeOnButtonClicked(string buttonName)
    {
        Debug.Log(buttonName);
        OnButtonClicked?.Invoke(buttonName);
    }

    private IEnumerator FadeCanvasGroup(float target)
    {
        float elapsedTime = 0f;
        float previousValve = CanvasGroup.alpha;

        while (elapsedTime < SmoothDecreaseDuration)
        {
            elapsedTime += Time.fixedDeltaTime;
            float normalizedPosition = elapsedTime / SmoothDecreaseDuration;
            float intermediateValue = Mathf.Lerp(previousValve, target, normalizedPosition);
            CanvasGroup.alpha = intermediateValue;

            yield return null;
        }
    }
}
