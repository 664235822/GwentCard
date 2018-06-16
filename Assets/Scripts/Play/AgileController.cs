using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Play
{
    public class AgileController : Singleton<AgileController>
    {
        public void Agile()
        {
            ShowCards.GetInstance().card.SetTarget(PlayerController.GetInstance().grids[(int)ShowCards.GetInstance().totalLine + 2]);
            ShowCards.GetInstance().Hide();
            PlayerController.GetInstance().PlayOver(ShowCards.GetInstance().card);
        }
    }
}
