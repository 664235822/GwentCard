using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoiataelBehavior3 : LeaderBehaviorBase {

    public sealed override void Play()
    {
        WarhornController.GetInstance().playerWarhorn[1] = true;
        Instantiate(transform, WarhornController.GetInstance().playerGrids[1]);

        base.Play();
    }

    public sealed override bool IsEnabled
    {
        get
        {
            return (!WarhornController.GetInstance().playerWarhorn[1] && isEnabled);
        }
    }
}
