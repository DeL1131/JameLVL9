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

    [Header("Hide During Credits")]
    [SerializeField] private GameObject[] _objectsToHide;

    [Header("Text")]
    [TextArea(10, 30)]
    [SerializeField] private string _creditsText;

    [Header("Scroll")]
    [SerializeField] private float _startY = -700f;
    [SerializeField] private float _endY = 1200f;
    [SerializeField] private float _speed = 50f;

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
        CacheReferences();

        if (_root != null)
            _root.SetActive(true);

        HideObjects();

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
            Debug.LogWarning("CreditsScroll: Text Transform не назначен.");
            yield break;
        }

        if (_speed <= 0f)
            _speed = 50f;

        if (_endY <= _startY)
            _endY = _startY + 1200f;

        while (_textTransform.anchoredPosition.y < _endY)
        {
            Vector2 pos = _textTransform.anchoredPosition;
            pos.y += _speed * Time.unscaledDeltaTime;
            _textTransform.anchoredPosition = pos;

            yield return null;
        }

        _scrollCoroutine = null;
        _onComplete?.Invoke();
    }

    private void HideObjects()
    {
        for (int i = 0; i < _objectsToHide.Length; i++)
        {
            if (_objectsToHide[i] != null)
                _objectsToHide[i].SetActive(false);
        }
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