using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthernBehavior2 : LeaderBehaviorBase {

    public sealed override void Play()
    {
        WeatherController.GetInstance().ClearSky();
        base.Play();
    }
}
