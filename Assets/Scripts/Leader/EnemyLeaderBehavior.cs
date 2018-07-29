using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyLeaderBehavior : LeaderBehaviorBase
    {
        public override void Play()
        {
            isEnabled = false;
            CoroutineManager.GetInstance().AddTask(TweenCard.GetInstance().Play(transform));
            StartCoroutine(LeaderController.GetInstance().EnemyTurnIndicator());
            EnemyController.GetInstance().Number();
            PowerController.GetInstance().Number();
            if (TurnController.GetInstance().isTurned[0])
                EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);
        }

        public override bool IsEnabled
        {
            get
            {
                return false;
            }
            set
            {
                isEnabled = value;
            }
        }
    }
}

