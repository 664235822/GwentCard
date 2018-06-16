using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class ScoiataelBehavior1 : LeaderBehaviorBase
    {
        public sealed override void Play()
        {
            int index = -1;
            for (int i = 0; i < PlayerController.GetInstance().grids[0].childCount; i++)
                if (PlayerController.GetInstance().grids[0].GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.frost) index = i;

            if (index != -1)
            {
                WeatherController.GetInstance().Frost();
                PlayerController.GetInstance().grids[0].GetChild(index).SetTarget(WeatherController.GetInstance().grid);
            }

            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return (!WeatherController.GetInstance().weather[0] && isEnabled);
            }
        }
    }
}