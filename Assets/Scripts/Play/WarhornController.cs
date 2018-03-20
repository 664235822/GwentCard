using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarhornController : MonoBehaviour {
    public static WarhornController instance;
    public Transform[] playerGrids;
    public Transform[] enemyGrids;
    [HideInInspector] public bool[] playerWarhorn = { false, false, false };
    [HideInInspector] public bool[] enemyWarhorn = { false, false, false };

    private void Awake()
    {
        instance = this;
    }

    public void PlayerWarhorn()
    {
        
    }

}
