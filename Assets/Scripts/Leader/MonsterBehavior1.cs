using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class MonsterBehavior1 : LeaderBehaviorBase
    {
        public sealed override void Play()
        {
            Transform grid = PlayerController.GetInstance().grids[0];
            ArrayList cardList = new ArrayList();
            for (int i = 0; i < grid.childCount; i++)
            {
                if (grid.GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.clear_sky ||
                    grid.GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.frost ||
                    grid.GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.fog ||
                    grid.GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.rain)
                    cardList.Add(grid.GetChild(i).gameObject);
            }

            ShowCards.GetInstance().ShowLeader(cardList, () => isEnabled = false, () => ShowCards.GetInstance().Hide());
        }
    }
}
