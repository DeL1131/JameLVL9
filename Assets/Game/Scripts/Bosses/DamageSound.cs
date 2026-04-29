using UnityEngine;

public class DamageSound : MonoBehaviour
{
    [SerializeField] private Boss1 _boss1;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _boss1.Damaged += PlayDamageSound;
    }

    private void OnDisable()
    {
        _boss1.Damaged -= PlayDamageSound;

    }

    public void PlayDamageSound(float damage)
    {
        _audioSource.Play();
    }
}
