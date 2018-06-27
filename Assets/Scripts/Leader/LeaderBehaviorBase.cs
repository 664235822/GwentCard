using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Leader
{
    public abstract class LeaderBehaviorBase : MonoBehaviour
    {
        protected bool isEnabled = true;

        public abstract void Play();
        public abstract bool IsEnabled { get; set; }
    }
}