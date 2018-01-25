﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagController : MonoBehaviour {
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

    private void Start()
    {
        tagButtons[0].GetComponent<UIButton>().isEnabled = false;
        GetGrids(0);
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
                groupLabel.text = groupLabelText[i];
            }
            else
            {
                groups[i].SetActive(false);
                tagButtons[i].GetComponent<UIButton>().isEnabled = true;
            }
        }

        
    }

    public void OnValueChange()
    {
        int index = 0;
        switch (popupList.value)
        {
            case "领导牌":
                index = 0;
                break;
            case "特殊牌":
                index = 1;
                break;
            case "生物牌":
                index = 2;
                break;
            case "中立牌":
                index = 3;
                break;
            case null:
                popupList.value = "领导牌";
                //修复切换牌组后popupList标题不变的bug
                break;
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
