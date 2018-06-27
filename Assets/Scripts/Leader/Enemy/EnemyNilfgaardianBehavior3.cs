using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyNilfgaardianBehavior3 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            LeaderController.GetInstance().obj[0].GetComponent<PlayerLeaderBehavior>().IsEnabled = false;
            isEnabled = false;
        }
    }
}