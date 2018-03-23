using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Movie : MonoBehaviour {
    VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnGUI()
    {
        if (videoPlayer.isPlaying && Input.GetMouseButtonDown(0))
        {
            videoPlayer.Stop();
            MusicController.GetInstance().Continue();
        }
    }

    public void OnClick()
    {
        videoPlayer.Play();
        MusicController.GetInstance().Pause();
    }
}
