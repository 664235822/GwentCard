using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Play
{
    public class ShowButton : MonoBehaviour
    {
        [SerializeField] ShowCards.ShowBehavior behavior;
        [SerializeField] Transform grid;

        public void OnClick()
        {
            ShowCards.GetInstance().Show(behavior, grid, false);
        }
    }
}
