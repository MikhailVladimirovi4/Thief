using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private float _recoveryRate;

    private AudioSource _audioSource;
    private float _maximumVolume;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
        _maximumVolume = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _audioSource.Play();
        StartCoroutine(VolumeChange(_maximumVolume));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(VolumeChange(0f));
    }

    private IEnumerator VolumeChange(float targetVolume)
    {
        for (float i = 0; i < _maximumVolume; i += Time.deltaTime)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _recoveryRate * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }
}
