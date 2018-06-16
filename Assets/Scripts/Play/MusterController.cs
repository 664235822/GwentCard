using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Play
{
    public class MusterController : Singleton<MusterController>
    {
        [HideInInspector]
        public readonly string[][] musterCards =
        {
            new string[]
            {
                "arachas1",
                "arachas2",
                "arachas3",
                "arachas_behemoth"
            },
            new string[]
            {
                "crone_brewess",
               "crone_weavess",
               "crone_whispess"
            },
            new string[]
            {
                "ghoul1",
                "ghoul2",
                "ghoul3"
            },
            new string[]
            {
                "nekker1",
                "nekker2",
                "nekker3"
            },
            new string[]
            {
                "vampire_bruxa",
               "vampire_ekimmara",
                "vampire_fleder",
                "vampire_garkain"
            },
            new string[]
            {
                "elven_skirmisher1",
                "elven_skirmisher2",
                "elven_skirmisher3"
            },
            new string[]
            {
                "skirmisher1",
                "skirmisher2",
                "skirmisher3"
            },
            new string[]
            {
               "smuggler1",
                "smuggler2",
                "smuggler3"
            }
        };

        public void Muster()
        {
            int index = 0;
            for (int i = 0; i < musterCards.Length; i++)
                for (int ii = 0; ii < musterCards[i].Length; ii++)
                    if (ShowCards.GetInstance().card.GetComponent<UISprite>().spriteName == musterCards[i][ii])
                        index = i;

            for (int i = 0; i < musterCards[index].Length; i++)
                for (int ii = PlayerController.GetInstance().grids[0].childCount - 1; ii >= 0; ii--)
                {
                    Transform card = PlayerController.GetInstance().grids[0].GetChild(ii);
                    if (card.GetComponent<UISprite>().spriteName == musterCards[index][i])
                        card.SetTarget(PlayerController.GetInstance().grids[(int)card.GetComponent<CardProperty>().line + 2]);
                }
        }
    }
}
