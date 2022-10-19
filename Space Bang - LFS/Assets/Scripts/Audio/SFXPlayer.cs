using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    #region Declarations
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip playerDamageClip;
    [SerializeField][Range(0f, 1f)] float playerDamageVolume = 1f;

    [SerializeField] AudioClip enemyDamageClip;
    [SerializeField][Range(0f, 1f)] float enemyDamageVolume = 1f;

    [Header("Flying")]
    [SerializeField] AudioClip flyingClip;
    [SerializeField][Range(0f, 1f)] float flyingVolume = 1f;

    [Header("AudioSource")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource flyingAudioSource;
    #endregion

    #region Play Clips
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayPlayerDamageClip()
    {
        PlayClip(playerDamageClip, playerDamageVolume);
    }

    public void PlayEnemyDamageClip()
    {
        PlayClip(enemyDamageClip, enemyDamageVolume);
    }

    public void PlayFlyingClip()
    {
        StartCoroutine(CoPlayDelayedClip(flyingClip, flyingVolume, flyingClip.length));
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    IEnumerator CoPlayDelayedClip(AudioClip clip, float volume, float delay)
    {
        yield return new WaitForSeconds(delay-1.5f);
        if(!flyingAudioSource.isPlaying)
//            PlayClip(clip, volume);
            flyingAudioSource.PlayOneShot(clip, volume);
    }

    #endregion
}