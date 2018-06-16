using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Configuration
{
    public class CardPlus : MonoBehaviour
    {
        [SerializeField] UILabel label;
        [SerializeField] UIToggle toggle;
        [HideInInspector] public int total;
        [HideInInspector] public int max;

        private void Awake()
        {
            string[] text = label.text.Split('/');
            total = int.Parse(text[0]);
            max = int.Parse(text[1]);
        }

        public void Plus()
        {
            if (total == max)
                return;

            total++;
            label.text = string.Format("{0}/{1}", total, max);
            NumberController.GetInstance().Number();
            SaveController.GetInstance().UpdateXML(transform);
        }

        public void Minus()
        {
            if (total == 0)
                return;

            total--;
            if (total == 0)
                toggle.value = false;

            label.text = string.Format("{0}/{1}", total, max);
            NumberController.GetInstance().Number();
            SaveController.GetInstance().UpdateXML(transform);
        }

        public void WriteTotal(int index)
        {
            total = index;
            label.text = string.Format("{0}/{1}", total, max);
        }

        public void Check()
        {
            if (toggle.value == true && total == 0)
            {
                total++;
                label.text = string.Format("{0}/{1}", total, max);
            }

            NumberController.GetInstance().Number();
            SaveController.GetInstance().UpdateXML(transform);
        }
    }
}
