using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GwentCard.Configuration
{
    public class TagController : Singleton<TagController>
    {
        public GameObject[] groups;
        [SerializeField] GameObject[] tagButtons;
        [SerializeField] UIPopupList popupList;
        [SerializeField] UILabel groupLabel;
        [HideInInspector] public Global.Group group = Global.Group.northern;
        [HideInInspector] public Global.List list = Global.List.leader;
        GameObject[] grids = new GameObject[4];
        readonly string[] groupLabelText = 
        {
            "赢得一局后可从牌组中抽一张牌",
            "平手时获得胜利",
            "每局过后随机选择一张怪物单位牌待在战场上",
            "战斗开始时，你可以决定谁先行动"
        };

        // Use this for initialization
        void Start()
        {
            OnClick(tagButtons[0]);
        }

        public void OnClick(GameObject tagButton)
        {
            for (int i = 0; i < tagButtons.Length; i++)
            {
                if (tagButton == tagButtons[i])
                {
                    groups[i].SetActive(true);
                    GetGrids(i);
                    OnValueChange();
                    tagButtons[i].GetComponent<UIButton>().isEnabled = false;
                    group = (Global.Group)System.Enum.Parse(typeof(Global.Group), tagButtons[i].transform.name);
                    groupLabel.text = groupLabelText[i];
                }
                else
                {
                    groups[i].SetActive(false);
                    tagButtons[i].GetComponent<UIButton>().isEnabled = true;
                }
            }

            if (File.Exists(Global.path))
            {
                SaveController.GetInstance().LoadXML();
                NumberController.GetInstance().Number();
            }
        }

        public void OnValueChange()
        {
            int index = 0;
            if (popupList.value != null)
            {
                index = popupList.GetItemsInt();
                list = (Global.List)index;
            }
            else
            {
                switch (list)
                {
                    case Global.List.leader:
                        popupList.value = "领导牌";
                        break;
                    case Global.List.special:
                        popupList.value = "特殊牌";
                        break;
                    case Global.List.monster:
                        popupList.value = "生物牌";
                        break;
                    case Global.List.neutral:
                        popupList.value = "中立牌";
                        break;
                }
                return;
            }

            for (int i = 0; i < popupList.items.Count; i++)
            {
                if (i == index)
                    grids[i].SetActive(true);
                else
                    grids[i].SetActive(false);
            }
        }

        void GetGrids(int index)
        {
            for (int i = 0; i < groups[index].transform.childCount; i++)
                grids[i] = groups[index].transform.GetChild(i).gameObject;
        }
    }
}