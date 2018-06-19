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
                {
                    GameObject card = Instantiate(grid.GetChild(i).gameObject);
                    UIButton button = card.GetComponent<UIButton>();
                    EventDelegate.Add(button.onClick, () => card.GetComponent<CardBehavior>().Play());
                    EventDelegate.Add(button.onClick, () => isEnabled = false);
                    cardList.Add(card);
                }
            }

            ShowCards.GetInstance().ShowLeader(cardList, grid, true, () => ShowCards.GetInstance().Hide());
        }
    }
}
