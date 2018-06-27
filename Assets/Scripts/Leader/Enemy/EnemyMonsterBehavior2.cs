using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyMonsterBehavior2 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            WarhornController.GetInstance().enemyWarhorn[0] = true;
            Instantiate(transform, WarhornController.GetInstance().enemyGrids[0]);

            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return EnemyController.GetInstance().grids[2].childCount > 3 &&
                    !WarhornController.GetInstance().enemyWarhorn[0] &&
                    isEnabled;
            }
        }
    }
}