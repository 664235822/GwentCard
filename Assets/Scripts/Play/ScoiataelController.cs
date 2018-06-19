using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Play
{
    public class ScoiataelController : MonoBehaviour
    {
        UIButton button;
        UIPopupList popupList;

        // Use this for initialization
        void Start()
        {
            button.isEnabled = transform.parent.GetComponent<UIButton>().isEnabled;
            GameController.GetInstance().offensive = true;
        }

        private void Awake()
        {
            button = GetComponent<UIButton>();
            popupList = GetComponent<UIPopupList>();
        }

        public void OnValueChange()
        {
            if (popupList.value == "先手")
                GameController.GetInstance().offensive = true;
            else
                GameController.GetInstance().offensive = false;
        }
    }
}
