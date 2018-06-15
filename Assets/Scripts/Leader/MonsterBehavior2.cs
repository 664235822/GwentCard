using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior2 : LeaderBehaviorBase {

    public sealed override void Play()
    {
        WarhornController.GetInstance().playerWarhorn[0] = true;
        Instantiate(transform).SetTarget(WarhornController.GetInstance().playerGrids[0]);

        base.Play();
    }

    public sealed override bool IsEnabled
    {
        get
        {
            return !WarhornController.GetInstance().playerWarhorn[0] && isEnabled;
        }
    }
}
