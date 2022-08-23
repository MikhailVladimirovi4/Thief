using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private float _recoveryRate;

    private AudioSource _audioSource;
    private readonly float _maximumVolume = 1f;
    private readonly float _minimumVolume = 0f;
    private Coroutine _volumeChange;

    public void TurnOn()
    {
        if (_volumeChange != null)
            StopCoroutine(_volumeChange);

        _audioSource.Play();
        _volumeChange = StartCoroutine(VolumeChange(_maximumVolume));
    }

    public void TurnOff()
    {
        if (_volumeChange != null)
            StopCoroutine(_volumeChange);

        _volumeChange = StartCoroutine(VolumeChange(_minimumVolume));
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minimumVolume;
    }

    private IEnumerator VolumeChange(float targetVolume)
    {
        for (float i = 0; i < _maximumVolume; i += Time.deltaTime)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _recoveryRate * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume == _minimumVolume)
            _audioSource.Stop();
    }
}
