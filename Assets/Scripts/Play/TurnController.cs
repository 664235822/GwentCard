﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Play
{
    public class TurnController : Singleton<TurnController>
    {
        [SerializeField] GameObject playerLabel;
        [SerializeField] GameObject enemyLabel;
        [HideInInspector] public int turnIndex = 0;
        [HideInInspector] public bool[] isTurned = { false, false };

        public void PlayerTurn()
        {
            isTurned[0] = true;
            playerLabel.SetActive(true);
            EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);

            if (isTurned[0] && isTurned[1])
                GameController.GetInstance().Turn();
        }

        public void EnemyTurn()
        {
            isTurned[1] = true;
            enemyLabel.SetActive(true);

            if (isTurned[0] && isTurned[1])
                GameController.GetInstance().Turn();
        }

        public void Clear()
        {
            playerLabel.SetActive(false);
            enemyLabel.SetActive(false);
            isTurned[0] = false;
            isTurned[1] = false;
            turnIndex++;
        }
    }
}