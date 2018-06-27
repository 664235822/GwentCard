using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyMonsterBehavior4 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            Transform grid = EnemyController.GetInstance().grids[5];
            int random = Random.Range(0, grid.childCount);
            grid.GetChild(random).SetTarget(EnemyController.GetInstance().grids[1]);

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
}