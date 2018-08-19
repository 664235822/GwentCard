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