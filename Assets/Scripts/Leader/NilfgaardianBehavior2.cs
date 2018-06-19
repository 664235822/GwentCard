using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class NilfgaardianBehavior2 : LeaderBehaviorBase
    {
        public sealed override void Play()
        {
            System.Random random = new System.Random();
            Transform grid = EnemyController.GetInstance().grids[1];
            int max = grid.childCount;
            int random1 = random.Next(0, max);
            int random2 = random.Next(0, max);
            int random3 = random.Next(0, max);
            ArrayList cardList = new ArrayList();
            cardList.Add(Instantiate(grid.GetChild(random1).gameObject));
            cardList.Add(Instantiate(grid.GetChild(random2).gameObject));
            cardList.Add(Instantiate(grid.GetChild(random3).gameObject));
            ShowCards.GetInstance().ShowLeader(cardList, grid, false, () => base.Play());
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return EnemyController.GetInstance().grids[1].childCount >= 3 && isEnabled;
            }
        }
    }
}