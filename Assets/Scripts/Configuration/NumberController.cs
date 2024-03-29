﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Configuration
{
    public class NumberController : Singleton<NumberController>
    {
        [SerializeField] UILabel[] labels;
        [HideInInspector] public int leaderCount = 0;
        [HideInInspector] public int specialCount = 0;
        [HideInInspector] public int monsterCount = 0;

        public void Number()
        {
            Transform group = transform.Find(TagController.GetInstance().group.ToString());
            leaderCount = 0;
            specialCount = 0;
            monsterCount = 0;

            Transform leader = group.transform.Find("leader");
            for (int i = 0; i < leader.childCount; i++)
            {
                Transform card = leader.GetChild(i);
                if (card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value)
                    leaderCount++;
            }

            Transform special = group.transform.Find("special");
            for (int i = 0; i < special.childCount; i++)
            {
                Transform card = special.GetChild(i);
                if (card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value)
                {
                    for (int ii = 0; ii < card.GetComponent<CardPlus>().total; ii++)
                        specialCount++;
                }
            }

            Transform monster = group.transform.Find("monster");
            for (int i = 0; i < monster.childCount; i++)
            {
                Transform card = monster.GetChild(i);
                if (card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value)
                {
                    for (int ii = 0; ii < card.GetComponent<CardPlus>().total; ii++)
                        monsterCount++;
                }
            }

            Transform neutral = group.transform.Find("neutral");
            for (int i = 0; i < neutral.childCount; i++)
            {
                Transform card = neutral.GetChild(i);
                if (card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value)
                {
                    for (int ii = 0; ii < card.GetComponent<CardPlus>().total; ii++)
                        monsterCount++;
                }
            }

            labels[0].text = string.Format("领导牌：{0}/1", leaderCount);
            labels[1].text = string.Format("特殊牌：{0}/10", specialCount);
            labels[2].text = string.Format("生物牌：25/{0}/40", monsterCount);
        }
    }
}
