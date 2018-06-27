using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyNilfgaardianBehavior4 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            int random = Random.Range(0, PlayerController.GetInstance().grids[5].childCount);
            PlayerController.GetInstance().grids[5].GetChild(random).SetTarget(EnemyController.GetInstance().grids[1]);

            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return PlayerController.GetInstance().grids[5].childCount > 0 && isEnabled;
            }
        }
    }
}