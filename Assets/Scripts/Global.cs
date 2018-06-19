using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard
{
    public static class Global
    {
        public enum Group { northern, nilfgaardian, monster, scoiatael }
        public enum List { leader, special, monster, neutral }
        public enum Line { melee, ranged, siege, empty }
        public enum Effect { empty, clear_sky, dummy, fog, frost, improve_neighbours, muster, nurse, rain, same_type_morale, scorch, spy, warhorn, agile }
        public static readonly string path = string.Format("{0}/PlayerCards.xml", Application.persistentDataPath);
        public static readonly string enemyPath = string.Format("{0}/EnemyCards.xml", Application.persistentDataPath);

        public static void SetTarget(this Transform card, Transform target)
        {
            Transform cardParent = card.parent;
            UIGrid cardGrid = null;
            if (cardParent != null) cardGrid = cardParent.GetComponent<UIGrid>();
            card.SetParent(target);
            card.localScale = new Vector3(1, 1, 1);
            UIGrid targetGrid = target.GetComponent<UIGrid>();
            if (cardGrid != null) cardGrid.Reposition();
            if (targetGrid != null) targetGrid.Reposition();
        }

        public static int GetItemsInt(this UIPopupList popupList)
        {
            int index = 0;
            for (int i = 0; i < popupList.items.Count; i++)
            {
                if (popupList.value == popupList.items[i]) index = i;
            }
            return index;
        }
    }
}