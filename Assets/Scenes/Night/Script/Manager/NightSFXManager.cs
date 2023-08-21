using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClipName
{
    none,
        autoAttack = 1, button,
        centryBall, chargingReaper, multiSlash, railPiercer,
        firstAde, barrior, hologramTrick, moveBack, booster, interceptDrone
}

public class NightSFXManager : MonoBehaviour
{
    public AudioSource nightAudioSource;

    [SerializeField]
    AudioClip[] sfxArr;

    public void PlayAudioClip(AudioClipName getName)
    {
        nightAudioSource.PlayOneShot(sfxArr[(int)getName]);
    }
}
