using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class GameMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _clipPhase1;
    [SerializeField] private AudioClip _clipPhase2;
    [SerializeField] private AudioClip _clipPhase3;
    [SerializeField] private AudioClip _clipGameOver;


    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayPhase1Music();
    }

    private void OnDisable()
    {

    }

    public void PlayPhase1Music()
    {
        _audioSource.clip = _clipPhase1;
        _audioSource.Play();
    }

    public void PlayPhase2Music()
    {
        _audioSource.clip = _clipPhase2;
       _audioSource.Play();
    }

    public void PlayGameCompleteMusic()
    {
        _audioSource.clip = _clipPhase3;
        _audioSource.Play();
    }

    public void PlayGameOverMusic()
    {
        _audioSource.clip = _clipGameOver;
        _audioSource.Play();
    }
}