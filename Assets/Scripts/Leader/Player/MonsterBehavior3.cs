using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class MonsterBehavior3 : PlayerLeaderBehavior
    {
        public sealed override void Play()
        {
            ShowCards.GetInstance().replaceInt = 0;
            Throw();
        }

        void Throw()
        {
            Transform grid = PlayerController.GetInstance().grids[1];
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

                    ShowCards.GetInstance().card.SetTarget(PlayerController.GetInstance().grids[5]);
                    ShowCards.GetInstance().replaceInt++;

                    if (ShowCards.GetInstance().replaceInt < 2)
                        Throw();
                    else
                        Draw();
                };
                EventDelegate.Add(card.GetComponent<UIButton>().onClick, callback);
                cardList.Add(card);
            }
            ShowCards.GetInstance().ShowLeader(cardList, grid, true, () => base.Play());
        }

        void Draw()
        {
            Transform grid = PlayerController.GetInstance().grids[0];
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
            ShowCards.GetInstance().ShowLeader(cardList, grid, true, () => base.Play());
        }
    }
}
