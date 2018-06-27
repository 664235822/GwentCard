using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class MonsterBehavior2 : PlayerLeaderBehavior
    {
        public sealed override void Play()
        {
            WarhornController.GetInstance().playerWarhorn[0] = true;
            Instantiate(transform, WarhornController.GetInstance().playerGrids[0]);

            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return !WarhornController.GetInstance().playerWarhorn[0] && isEnabled;
            }
        }
    }
}