using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer bgmMixer;
    public AudioMixer sfxMixer;

    public void ChangeBgmVolume(float value) {
        bgmMixer.SetFloat("BGM", value);
        GameManager.instance.player.bgm_volume = value;
    }

    public void ChangeSfxVolume(float value) {
        sfxMixer.SetFloat("SFX", value);
        GameManager.instance.player.sfx_volume = value;
    }
}
