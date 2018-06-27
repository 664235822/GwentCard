using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class PlayerLeaderBehavior : LeaderBehaviorBase
    {
        public string message;

        public override void Play()
        {
            isEnabled = false;
            ShowCards.GetInstance().Hide();
            PlayerController.GetInstance().PlayOver(transform);
        }

        public override bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
            }
        }
    }
}