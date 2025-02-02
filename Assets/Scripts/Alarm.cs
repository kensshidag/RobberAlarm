using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _delta = 0.01f;
    private float _delay = 0.1f;
    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Robber robber))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _audioSource.volume = _minVolume;
            _audioSource.Play();
            _coroutine = StartCoroutine(VolumeBoost(_delay, _audioSource.volume, _maxVolume, _delta));
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        if (collider.gameObject.TryGetComponent(out Robber robber))
        {
            StartCoroutine(VolumeBoost(_delay, _audioSource.volume, _minVolume, _delta));
        }
    }

    private IEnumerator VolumeBoost(float delay, float currentVolume, float targetVolume, float delta)
    {
        WaitForSeconds wait = new (delay);

        while (currentVolume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(currentVolume, targetVolume, delta);
            currentVolume = _audioSource.volume;

            yield return wait;
        }

        if (currentVolume == _minVolume)
        {
            _audioSource.Stop();
        }
    }
}
