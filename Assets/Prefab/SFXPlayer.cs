using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {
    AudioSource audioSource;
    public AudioClip ButtonSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonSound() {
        audioSource.PlayOneShot(ButtonSound);
    }
}
