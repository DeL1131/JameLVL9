using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _clipGameOver;
    [SerializeField] private AudioClip _clipGameComplete;
    [SerializeField] private AudioClip _clipMiniGameComplete;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameOverMusic()
    {
        PlayClip(_clipGameOver);
    }

    public void PlayGameCompleteMusic()
    {
        PlayClip(_clipGameComplete);
    }

    public void PlayMiniGameCompleteMusic()
    {
        PlayClip(_clipMiniGameComplete);
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Music clip is missing.");
            return;
        }

        _audioSource.Stop();          // выключает текущий фон
        _audioSource.clip = clip;     // ставит новую музыку
        _audioSource.loop = false;    // конец игры обычно не лупится
        _audioSource.Play();          // запускает новую
    }
}