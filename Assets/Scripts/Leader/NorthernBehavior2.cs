using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthernBehavior2 : LeaderBehaviorBase {

    public sealed override void Play()
    {
        WeatherController.GetInstance().ClearSky();
        base.Play();
    }

    public sealed override bool GetEnabled()
    {
        bool[] weather = WeatherController.GetInstance().weather;
        return (weather[0] || weather[1] || weather[2]) && enabled;
    }
}
