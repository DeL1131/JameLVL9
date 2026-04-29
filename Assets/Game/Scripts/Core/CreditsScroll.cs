using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CreditsScroll : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _root;
    [SerializeField] private RectTransform _textTransform;
    [SerializeField] private TMP_Text _text;

    [Header("Text")]
    [TextArea(10, 30)]
    [SerializeField] private string _creditsText;

    [Header("Scroll")]
    [SerializeField] private float _startY = -700f;
    [SerializeField] private float _endY = 1200f;
    [SerializeField] private float _speed = 100f;

    [Header("After Credits")]
    [SerializeField] private UnityEvent _onComplete;

    private bool _isPlaying;

    private void Awake()
    {
        if (_text != null)
            _text.text = _creditsText;
    }

    private void Update()
    {
        Play();

        if (_isPlaying == false)
            return;

        Vector2 pos = _textTransform.anchoredPosition;
        pos.y += _speed * Time.unscaledDeltaTime;
        _textTransform.anchoredPosition = pos;

        if (pos.y >= _endY)
        {
            _isPlaying = false;
            _onComplete?.Invoke();
        }
    }

    public void Play()
    {
        Debug.Log("Credits started");

        if (_root != null)
            _root.SetActive(true);

        if (_text != null)
            _text.text = _creditsText;

        _textTransform.anchoredPosition = new Vector2(
            _textTransform.anchoredPosition.x,
            _startY
        );

        _isPlaying = true;
    }
}