using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    AudioSource bgm_player;
    AudioSource sfx_player;

    public Slider bgm_slider;
    public Slider sfx_slider;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        bgm_player = GameObject.Find("BGM Player").GetComponent<AudioSource>();
        sfx_player = GameObject.Find("Sfx Player").GetComponent<AudioSource>();
    }

    public void ChangeBgmVolume(float value) {
        bgm_player.volume = GameManager.instance.player.bgm_volume = value;
    }

    public void ChangeSfxVolume(float value) {
        sfx_player.volume = GameManager.instance.player.sfx_volume = value;
    }
}
