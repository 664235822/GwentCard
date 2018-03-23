using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarhornController : Singleton<WarhornController> {
    public Transform[] playerGrids;
    public Transform[] enemyGrids;
    [HideInInspector] public bool[] playerWarhorn = { false, false, false };
    [HideInInspector] public bool[] enemyWarhorn = { false, false, false };

    public void PlayerWarhorn()
    {
        
    }

}
