using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthernBehavior3 : LeaderBehaviorBase{

    public sealed override void Play()
    {
        if(!WarhornController.GetInstance().playerWarhorn[2])
        {
            WarhornController.GetInstance().playerWarhorn[2] = true;
            Instantiate(transform).SetTarget(WarhornController.GetInstance().playerGrids[2]);
        }
        base.Play();
    }
}
