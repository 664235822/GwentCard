using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : Singleton<MusicController> {
    [SerializeField] AudioClip[] audioClip;
    AudioSource audioSource;
    bool pause = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!audioSource.isPlaying && !pause)
        {
            audioSource.clip = audioClip[Random.Range(0, audioClip.Length)];
            audioSource.Play();
        }
    }

    public void Pause()
    {
        audioSource.Pause();
        pause = true;
    }

    public void Continue()
    {
        audioSource.Play();
        pause = false;
    }
}
