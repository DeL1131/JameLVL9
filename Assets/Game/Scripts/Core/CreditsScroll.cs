using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CreditsScroll : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _root;
    [SerializeField] private RectTransform _textTransform;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CanvasGroup _creditsCanvasGroup;

    [Header("Text")]
    [TextArea(10, 30)]
    [SerializeField] private string _creditsText;

    [Header("Scroll")]
    [SerializeField] private float _startY = -700f;
    [SerializeField] private float _endY = 1200f;
    [SerializeField] private float _speed = 100f;

    [Header("After Credits")]
    [SerializeField] private UnityEvent _onComplete;

    private Coroutine _scrollCoroutine;

    private void Awake()
    {
        CacheReferences();

        if (_text != null)
        {
            _text.text = _creditsText;
            _text.raycastTarget = false;
        }

        DisableCreditsRaycasts();
        ResetPosition();
    }

    public void Play()
    {
        Debug.Log("Credits started");

        CacheReferences();

        if (_root != null)
            _root.SetActive(true);

        DisableCreditsRaycasts();

        if (_text != null)
            _text.text = _creditsText;

        ResetPosition();

        if (_scrollCoroutine != null)
            StopCoroutine(_scrollCoroutine);

        _scrollCoroutine = StartCoroutine(ScrollRoutine());
    }

    private IEnumerator ScrollRoutine()
    {
        if (_textTransform == null)
        {
            Debug.LogWarning("CreditsScroll cannot play because _textTransform is not assigned.");
            yield break;
        }

        if (_speed <= 0f)
        {
            Debug.LogWarning("CreditsScroll speed must be greater than 0. Using fallback speed 100.");
            _speed = 100f;
        }

        if (_endY <= _startY)
        {
            Debug.LogWarning("CreditsScroll _endY must be greater than _startY. Using fallback end position.");
            _endY = _startY + 1200f;
        }

        while (_textTransform.anchoredPosition.y < _endY)
        {
            Vector2 pos = _textTransform.anchoredPosition;
            pos.y += (_speed * 0.5f) * Time.unscaledDeltaTime;
            _textTransform.anchoredPosition = pos;

            yield return null;
        }

        _scrollCoroutine = null;
        _onComplete?.Invoke();
    }

    private void CacheReferences()
    {
        if (_text == null)
            _text = GetComponentInChildren<TMP_Text>(true);

        if (_textTransform == null && _text != null)
            _textTransform = _text.rectTransform;
    }

    private void ResetPosition()
    {
        if (_textTransform == null)
            return;

        _textTransform.anchoredPosition = new Vector2(
            _textTransform.anchoredPosition.x,
            _startY
        );
    }

    private void DisableCreditsRaycasts()
    {
        if (_creditsCanvasGroup == null)
            return;

        _creditsCanvasGroup.interactable = false;
        _creditsCanvasGroup.blocksRaycasts = false;
    }
}
