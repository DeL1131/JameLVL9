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
    private float _minVolume = -80f;

    public bool IsMuted { get; private set; }

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        _mixer.audioMixer.GetFloat(CommandMasterVolume, out _currentVolume);
        IsMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        _toggle.isOn = IsMuted;
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
            _mixer.audioMixer.SetFloat(CommandMasterVolume, _currentVolume);
        }

        PlayerPrefs.SetInt("IsMuted", IsMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}