using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NilfgaardianBehavior4 : LeaderBehaviorBase {

    public sealed override void Play()
    {
        int random = Random.Range(0, EnemyController.GetInstance().grids[5].childCount);
        EnemyController.GetInstance().grids[5].GetChild(random).SetTarget(PlayerController.GetInstance().grids[1]);

        base.Play();
    }

    public sealed override bool IsEnabled
    {
        get
        {
            return EnemyController.GetInstance().grids[5].childCount > 0 && isEnabled;
        }
    }
}
