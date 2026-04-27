using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public abstract class SliderVolume : MonoBehaviour
{
    [SerializeField] private ToggleMuteChange _toggle;
    [SerializeField] private AudioMixerGroup _mixer;

    private Slider _slider;

    private float _minVolumeValue = 0.0001f;
    private float _maxVolumeValue = 1f;
    private float _decibelConversionFactor = 100f;

    protected abstract string VolumeParameter { get; }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        
        ChangeVolume(_slider.value);     
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        volume = Mathf.Clamp(volume, _minVolumeValue, _maxVolumeValue);

        if (_toggle.IsMuted == false)
            _mixer.audioMixer.SetFloat(VolumeParameter, Mathf.Log10(volume) * _decibelConversionFactor);

        PlayerPrefs.SetFloat(VolumeParameter, volume);       
        PlayerPrefs.Save();
    }
}