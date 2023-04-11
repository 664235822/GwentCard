using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace GwentCard.Main
{
    public class Movie : MonoBehaviour
    {
        VideoPlayer videoPlayer;
        bool bPlay = false;
        double time;

        private void Update()
        {
            if (bPlay)
            {
                time += Time.deltaTime;

                if (time >= videoPlayer.clip.length)
                {
                    videoPlayer.Stop();
                    MusicManager.GetInstance().Continue();
                    bPlay = false;
                }
            }
        }

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        private void OnGUI()
        {
            if (bPlay && Input.GetMouseButtonDown(0))
            {
                videoPlayer.Stop();
                MusicManager.GetInstance().Continue();
                bPlay = false;
            }
        }

        public void OnClick()
        {
            videoPlayer.Play();
            MusicManager.GetInstance().Pause();
            time = 0;
            bPlay = true;
        }
    }
}