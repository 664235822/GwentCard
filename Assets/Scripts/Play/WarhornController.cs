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
        int line = ShowCards.GetInstance().popupList.GetItemsInt();
        playerWarhorn[line] = true;
        ShowCards.GetInstance().totalGrid.SetParent(CardBehavior.index, playerGrids[line]);

        ShowCards.GetInstance().Hide();
        PlayerController.GetInstance().Number();
        PowerController.GetInstance().Number();
        EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);
    }

}
