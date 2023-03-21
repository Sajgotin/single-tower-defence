using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
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

    public void PlayExplosion()
    {
        _audioSource.volume = 0.5f;
        _audioSource.PlayOneShot(audioClips[0]);
    }

    IEnumerator PlayEnemyDeath()
    {
            Instance._activeDeaths++;
            _audioSource.volume = 0.35f;
            _audioSource.PlayOneShot(audioClips[1]);
            yield return new WaitForSeconds(audioClips[1].length);
            Instance._activeDeaths--;
    }

    IEnumerator PlayHitCastle()
    {
            Instance._activeAttacks++;
            _audioSource.volume = 0.2f;
            _audioSource.PlayOneShot(audioClips[2]);
            yield return new WaitForSeconds(audioClips[2].length);
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
