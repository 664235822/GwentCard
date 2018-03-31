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
        if (!playerWarhorn[ShowCards.GetInstance().totalLine])
        {
            playerWarhorn[ShowCards.GetInstance().totalLine] = true;
            ShowCards.GetInstance().totalGrid.SetParent(CardBehavior.index, playerGrids[ShowCards.GetInstance().totalLine]);
        }
        else
            ShowCards.GetInstance().totalGrid.SetParent(CardBehavior.index, PlayerController.GetInstance().grids[5]);

        ShowCards.GetInstance().Hide(true);
    }
}
