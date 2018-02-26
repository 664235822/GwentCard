using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    public static MusicController instance;
    [SerializeField] AudioClip[] audioClip;
    AudioSource audioSource;
    bool pause = false;

    private void Awake()
    {
        instance = this;
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
