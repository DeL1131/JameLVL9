using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CreditsScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _root;
    [SerializeField] private TMP_Text _creditsText;

    [Header("Text")]
    [TextArea(2, 5)]
    [SerializeField] private string[] _lines;

    [Header("Timing")]
    [SerializeField] private float _lineShowTime = 2.5f;
    [SerializeField] private float _delayBetweenLines = 0.4f;

    [Header("After Credits")]
    [SerializeField] private UnityEvent _completed;

    private Coroutine _creditsCoroutine;

    private void Awake()
    {
        if (_root != null)
            _root.SetActive(false);

        if (_creditsText != null)
            _creditsText.text = "";
    }

    public void Play()
    {
        if (_creditsCoroutine != null)
            return;

        _creditsCoroutine = StartCoroutine(CreditsRoutine());
    }

    private IEnumerator CreditsRoutine()
    {
        if (_root != null)
            _root.SetActive(true);

        for (int i = 0; i < _lines.Length; i++)
        {
            _creditsText.text = _lines[i];

            yield return new WaitForSecondsRealtime(_lineShowTime);

            _creditsText.text = "";

            yield return new WaitForSecondsRealtime(_delayBetweenLines);
        }

        _completed?.Invoke();
    }
}