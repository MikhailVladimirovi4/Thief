using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private float _recoveryRate;

    private AudioSource _audioSource;
    private readonly float _maximumVolume = 1f;
    private readonly float _minimumVolume = 0f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minimumVolume;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ThiefMovement thiefMovement))
        {
            _audioSource.Play();
            StartCoroutine(VolumeChange(_maximumVolume));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ThiefMovement thiefMovement))
            StartCoroutine(VolumeChange(_minimumVolume));
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
