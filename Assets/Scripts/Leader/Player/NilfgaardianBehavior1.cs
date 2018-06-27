using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class NilfgaardianBehavior1 : PlayerLeaderBehavior
    {
        public sealed override void Play()
        {
            int index = -1;
            for (int i = 0; i < PlayerController.GetInstance().grids[0].childCount; i++)
                if (PlayerController.GetInstance().grids[0].GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.rain) index = i;

            if (index != -1)
            {
                WeatherController.GetInstance().Rain();
                PlayerController.GetInstance().grids[0].GetChild(index).SetTarget(WeatherController.GetInstance().grid);
            }

            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return (!WeatherController.GetInstance().weather[2] && isEnabled);
            }
        }
    }
}
