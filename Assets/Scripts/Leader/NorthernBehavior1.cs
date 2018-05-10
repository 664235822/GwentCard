using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthernBehavior1 : LeaderBehaviorBase {

    public sealed override void Play()
    {
        int index = -1;
        for (int i = 0; i < PlayerController.GetInstance().grids[0].childCount; i++)
            if (PlayerController.GetInstance().grids[0].GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.fog) index = i;

        if (index != -1)
        {
            WeatherController.GetInstance().Fog();
            PlayerController.GetInstance().grids[0].GetChild(index).SetTarget(WeatherController.GetInstance().grid);
        }

        base.Play();
    }
}
