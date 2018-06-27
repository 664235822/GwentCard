using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyNorthernBehavior3 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            WarhornController.GetInstance().enemyWarhorn[2] = true;
            Instantiate(transform, WarhornController.GetInstance().enemyGrids[2]);

            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return EnemyController.GetInstance().grids[4].childCount > 3 &&
                    !WarhornController.GetInstance().enemyWarhorn[2] &&
                    isEnabled;
            }
        }
    }
}