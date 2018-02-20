using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagController : MonoBehaviour {
    public static TagController instance;
    public static Constants.Group group = Constants.Group.northern;
    public static Constants.List list = Constants.List.leader;
    public GameObject[] tagButtons;
    public GameObject[] groups;
    public UIPopupList popupList;
    public UILabel groupLabel;
    GameObject[] grids = new GameObject[4];
    readonly string[] groupLabelText = {
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

    private void Awake()
    {
        instance = this;
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
                group = (Constants.Group)System.Enum.Parse(typeof(Constants.Group), tagButtons[i].transform.name);
                groupLabel.text = groupLabelText[i];
            }
            else
            {
                groups[i].SetActive(false);
                tagButtons[i].GetComponent<UIButton>().isEnabled = true;
            }
        }

        SaveController.instance.LoadXML();
        NumberController.instance.Number();
    }

    public void OnValueChange()
    {
        int index = 0;
        switch (popupList.value)
        {
            case "领导牌":
                index = 0;
                list = Constants.List.leader;
                break;
            case "特殊牌":
                index = 1;
                list = Constants.List.special;
                break;
            case "生物牌":
                index = 2;
                list = Constants.List.monster;
                break;
            case "中立牌":
                index = 3;
                list = Constants.List.neutral;
                break;
            case null:
                switch(list)
                {
                    case Constants.List.leader:
                        popupList.value = "领导牌";
                        break;
                    case Constants.List.special:
                        popupList.value = "特殊牌";
                        break;
                    case Constants.List.monster:
                        popupList.value = "生物牌";
                        break;
                    case Constants.List.neutral:
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
