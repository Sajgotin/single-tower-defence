using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] List<AudioClip> _audioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> _mumbles = new List<AudioClip>();
    [SerializeField] AudioClip _backgroundMusic;
    [SerializeField] int _activeDeaths;
    [SerializeField] int _activeAttacks;
    public static SoundController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayDialogueSound()
    {
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(_mumbles[Random.Range(0, _mumbles.Count)]);
    }

    public void PlayExplosion()
    {
        _audioSource.volume = 0.5f;
        _audioSource.PlayOneShot(_audioClips[0]);
    }

    IEnumerator PlayEnemyDeath()
    {
            Instance._activeDeaths++;
            _audioSource.volume = 0.35f;
            _audioSource.PlayOneShot(_audioClips[1]);
            yield return new WaitForSeconds(_audioClips[1].length);
            Instance._activeDeaths--;
    }

    IEnumerator PlayHitCastle()
    {
            Instance._activeAttacks++;
            _audioSource.volume = 0.2f;
            _audioSource.PlayOneShot(_audioClips[2]);
            yield return new WaitForSeconds(_audioClips[2].length);
            Instance._activeAttacks--;
    }

    public void ActivateDeathSound()
    {
        if (Instance._activeDeaths < 10) StartCoroutine(PlayEnemyDeath());
    }

    public void ActivateHitSound()
    {
        if (Instance._activeAttacks < 10) StartCoroutine(PlayHitCastle());
    }
}
