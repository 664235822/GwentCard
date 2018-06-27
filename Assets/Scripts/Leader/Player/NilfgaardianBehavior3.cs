using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class NilfgaardianBehavior3 : PlayerLeaderBehavior
    {
        public sealed override void Play()
        {
            LeaderController.GetInstance().obj[1].GetComponent<PlayerLeaderBehavior>().IsEnabled = false;
            isEnabled = false;
        }
    }
}