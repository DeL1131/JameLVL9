using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]

public class ToggleMuteChange : MonoBehaviour
{
    protected const string CommandMasterVolume = "Master";

    [SerializeField] private AudioMixerGroup _mixer;

    private Toggle _toggle;

    private float _currentVolume;
    private float _minVolumeValue = 0.0001f;
    private float _minVolume = -80f;
    private float _decibelConversionFactor = 100f;

    public bool IsMuted { get; private set; }

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        _mixer.audioMixer.GetFloat(CommandMasterVolume, out _currentVolume);

        if (GameSession.Instance != null && GameSession.Instance.TryGetMuteState(out bool savedIsMuted))
            IsMuted = savedIsMuted;

        _toggle.SetIsOnWithoutNotify(IsMuted);

        if (IsMuted)
            _mixer.audioMixer.SetFloat(CommandMasterVolume, _minVolume);
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(MuteAllMusic);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(MuteAllMusic);
    }

    public void MuteAllMusic(bool enabled)
    {
        if (enabled)
        {
            IsMuted = true;
            _mixer.audioMixer.GetFloat(CommandMasterVolume, out _currentVolume);
            _mixer.audioMixer.SetFloat(CommandMasterVolume, _minVolume);
        }
        else
        {
            IsMuted= false;

            if (GameSession.Instance != null && GameSession.Instance.TryGetAudioVolume(CommandMasterVolume, out float savedMasterVolume))
                _mixer.audioMixer.SetFloat(CommandMasterVolume, Mathf.Log10(Mathf.Clamp(savedMasterVolume, _minVolumeValue, 1f)) * _decibelConversionFactor);
            else
                _mixer.audioMixer.SetFloat(CommandMasterVolume, _currentVolume);
        }

        if (GameSession.Instance != null)
            GameSession.Instance.SaveMuteState(IsMuted);
    }
}
