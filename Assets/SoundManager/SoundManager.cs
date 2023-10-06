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
    public Slider BGMSlider;
    public Slider SFXSlider;

    void Start() {
        Debug.Log(GameManager.instance.player.bgm_volume);
        Debug.Log(GameManager.instance.player.sfx_volume);
        bgmMixer.SetFloat("BGM", MapVolumeToDecibel(GameManager.instance.player.bgm_volume));
        sfxMixer.SetFloat("SFX", MapVolumeToDecibel(GameManager.instance.player.sfx_volume));
        BGMSlider.value = GameManager.instance.player.bgm_volume;
        SFXSlider.value = GameManager.instance.player.sfx_volume;
    }

    public void ChangeBgmVolume(float value) {
        bgmMixer.SetFloat("BGM", MapVolumeToDecibel(value));
        GameManager.instance.player.bgm_volume = value;
        GameManager.instance.SavePlayerData();
    }

    public void ChangeSfxVolume(float value) {
        sfxMixer.SetFloat("SFX", MapVolumeToDecibel(value));
        GameManager.instance.player.sfx_volume = value;
        GameManager.instance.SavePlayerData();
    }

    float MapVolumeToDecibel(float normalizedValue) {
        float minDecibel = -80f;
        float maxDecibel = 0f;
        return minDecibel + normalizedValue * (maxDecibel - minDecibel);
    }
}
