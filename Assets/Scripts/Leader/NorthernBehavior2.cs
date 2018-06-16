using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class NorthernBehavior2 : LeaderBehaviorBase
    {
        public sealed override void Play()
        {
            WeatherController.GetInstance().ClearSky();
            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                bool[] weather = WeatherController.GetInstance().weather;
                return (weather[0] || weather[1] || weather[2]) && enabled;
            }
        }
    }
}