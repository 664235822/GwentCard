using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyScoiataelBehavior4 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            int max = 0;
            for (int i = 0; i < PlayerController.GetInstance().grids[2].childCount; i++)
            {
                Transform card = PlayerController.GetInstance().grids[2].GetChild(i);
                if (!card.GetComponent<CardProperty>().gold)
                {
                    int power = card.GetComponent<CardBehavior>().totalPower;
                    if (power > max)
                        max = power;
                }
            }
            for (int i = PlayerController.GetInstance().grids[2].childCount - 1; i > +0; i--)
            {
                Transform card = PlayerController.GetInstance().grids[2].GetChild(i);
                if (card.GetComponent<CardBehavior>().totalPower == max && !card.GetComponent<CardProperty>().gold)
                    card.SetTarget(PlayerController.GetInstance().grids[5]);
            }
            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                int max = 0;
                for (int i = 0; i < PlayerController.GetInstance().grids[2].childCount; i++)
                {
                    Transform card = PlayerController.GetInstance().grids[2].GetChild(i);
                    if (!card.GetComponent<CardProperty>().gold)
                    {
                        int power = card.GetComponent<CardBehavior>().totalPower;
                        if (power > max)
                            max = power;
                    }
                }
                return max >= 10 && isEnabled;
            }
        }
    }
}
