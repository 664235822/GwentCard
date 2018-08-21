using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Play
{
    public class TurnController : Singleton<TurnController>
    {
        [SerializeField] GameObject playerLabel;
        [SerializeField] GameObject enemyLabel;
        [SerializeField] UIButton turnButton;
        [HideInInspector] public int turnIndex = 0;
        [HideInInspector] public bool[] isTurned = { false, false };

        private void FixedUpdate()
        {
            if (!isTurned[0] && CoroutineManager.GetInstance().GetFinish())
                turnButton.isEnabled = true;
            else
                turnButton.isEnabled = false;
        }

        public void PlayerTurn()
        {
            isTurned[0] = true;
            playerLabel.SetActive(true);

            if (isTurned[0] && isTurned[1])
            {
                CoroutineManager.GetInstance().AddTask(GameController.GetInstance().Turn());
                return;
            }

            CoroutineManager.GetInstance().AddTask(EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]));
        }

        public IEnumerator EnemyTurn()
        {
            isTurned[1] = true;
            enemyLabel.SetActive(true);

            if (isTurned[0] && isTurned[1])
                yield return GameController.GetInstance().Turn();

            CoroutineManager.GetInstance().Finish();
            yield return null;
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