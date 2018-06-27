using System.Collections;
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

            if (isTurned[0] && isTurned[1])
            {
                GameController.GetInstance().Turn();
                return;
            }

            EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);
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
            turnIndex++;
            playerLabel.SetActive(false);
            enemyLabel.SetActive(false);
            isTurned[0] = false;
            isTurned[1] = false;
        }
    }
}