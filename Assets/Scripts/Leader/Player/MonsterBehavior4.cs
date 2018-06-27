using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class MonsterBehavior4 : PlayerLeaderBehavior
    {
        public sealed override void Play()
        {
            Transform grid = PlayerController.GetInstance().grids[5];
            ArrayList cardList = new ArrayList();
            for (int i = 0; i < grid.childCount; i++)
            {
                GameObject card = Instantiate(grid.GetChild(i).gameObject);
                EventDelegate.Callback callback = delegate
                {
                    int index = 0;
                    for (int ii = 0; ii < ShowCards.GetInstance().totalGrid.childCount; ii++)
                        if (string.Format("{0}(Clone)", ShowCards.GetInstance().totalGrid.GetChild(ii).name) == card.name) index = ii;
                    ShowCards.GetInstance().card = ShowCards.GetInstance().totalGrid.GetChild(index);

                    ShowCards.GetInstance().card.SetTarget(PlayerController.GetInstance().grids[1]);
                    base.Play();
                };
                EventDelegate.Add(card.GetComponent<UIButton>().onClick, callback);
                cardList.Add(card);
            }
            ShowCards.GetInstance().ShowLeader(cardList, grid, true, () => ShowCards.GetInstance().Hide());
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return PlayerController.GetInstance().grids[5].childCount > 0 && isEnabled;
            }
        }
    }
}