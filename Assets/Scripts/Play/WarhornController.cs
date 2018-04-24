using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarhornController : Singleton<WarhornController> {
    public Transform[] playerGrids;
    public Transform[] enemyGrids;
    [HideInInspector] public bool[] playerWarhorn = { false, false, false };
    [HideInInspector] public bool[] enemyWarhorn = { false, false, false };

    public void Warhorn()
    {
        if (!playerWarhorn[ShowCards.GetInstance().totalLine])
        {
            playerWarhorn[ShowCards.GetInstance().totalLine] = true;
            ShowCards.GetInstance().card.SetTarget(playerGrids[ShowCards.GetInstance().totalLine]);
        }
        else
            ShowCards.GetInstance().card.SetTarget(PlayerController.GetInstance().grids[5]);

        ShowCards.GetInstance().Hide();
        PlayerController.GetInstance().PlayOver(ShowCards.GetInstance().card);
    }
}
