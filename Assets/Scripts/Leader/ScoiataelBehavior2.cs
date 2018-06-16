using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class ScoiataelBehavior2 : LeaderBehaviorBase
    {

        public sealed override void Play()
        {
            PlayerController.GetInstance().DrawCards(1);
            isEnabled = false;
        }
    }
}