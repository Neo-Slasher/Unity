using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void ChangeBgmVolume(float value) {
        audioMixer.SetFloat("BGM", value);
        GameManager.instance.player.bgm_volume = value;
    }

    public void ChangeSfxVolume(float value) {
        audioMixer.SetFloat("SFX", value);
        GameManager.instance.player.sfx_volume = value;
    }
}
