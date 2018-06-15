using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoiataelBehavior2 : LeaderBehaviorBase {

    public sealed override void Play()
    {
        PlayerController.GetInstance().DrawCards(1);
        isEnabled = false;
    }
}
