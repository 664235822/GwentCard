using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NilfgaardianBehavior3 : LeaderBehaviorBase {

    public sealed override void Play()
    {
        LeaderController.GetInstance().obj[1].GetComponent<LeaderBehaviorBase>().IsEnabled = false;
        isEnabled = false;
    }
}
