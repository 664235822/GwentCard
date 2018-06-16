using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace GwentCard.Main
{
    public class Movie : MonoBehaviour
    {
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
                MusicManager.GetInstance().Continue();
            }
        }

        public void OnClick()
        {
            videoPlayer.Play();
            MusicManager.GetInstance().Pause();
        }
    }
}