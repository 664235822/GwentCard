using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyScoiataelBehavior2 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            EnemyController.GetInstance().DrawCards(1);
            isEnabled = false;
        }
    }
}