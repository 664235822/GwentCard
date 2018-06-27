using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyScoiataelBehavior3 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            WarhornController.GetInstance().enemyWarhorn[1] = true;
            Instantiate(transform, WarhornController.GetInstance().enemyGrids[1]);

            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return EnemyController.GetInstance().grids[3].childCount > 3 &&
                    !WarhornController.GetInstance().enemyWarhorn[1] &&
                    isEnabled;
            }
        }
    }
}