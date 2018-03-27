using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCards : Singleton<ShowCards> {
    public enum Behaviour { draw, show, dummy, warhorn }
    public Transform grid;
    public UIPopupList popupList;
    [SerializeField] GameObject show;
    [SerializeField] UILabel label;
    [SerializeField] UIScrollView scrollView;
    [SerializeField] UIButton OKButton;
    [SerializeField] UIButton returnButton;
    [HideInInspector] public Transform totalGrid;
    [HideInInspector] public int totalLine = 0;
    Behaviour behaviour;

    public void Show(Behaviour behav, Transform ShowGrid, bool Repeat)
    {
        behaviour = behav;

        if (!Repeat)
        {
            BlackShow.GetInstance().Show(true);
            PlayerController.GetInstance().player.SetActive(false);
            EnemyController.GetInstance().enemy.SetActive(false);
            show.SetActive(true);
        }

        switch (behaviour)
        {
            case Behaviour.draw:
                label.text = "请选择要打出的牌";
                goto default;
            case Behaviour.show:
                label.text = "显示卡牌";
                goto default;
            case Behaviour.dummy:
                label.text = "请选择要替换的牌";
                popupList.gameObject.SetActive(true);
                OKButton.gameObject.SetActive(false);
                break;
            case Behaviour.warhorn:
                label.text = "战争号角";
                popupList.gameObject.SetActive(true);
                OKButton.gameObject.SetActive(true);
                break;
            default:
                totalGrid = ShowGrid;
                popupList.gameObject.SetActive(false);
                OKButton.gameObject.SetActive(false);
                break;
        }

        if (Repeat) grid.DestroyChildren();

        for (int i = 0; i < ShowGrid.childCount; i++)
        {
            GameObject card = Instantiate(ShowGrid.GetChild(i).gameObject, grid);
            UISprite sprite = card.GetComponent<UISprite>();
            sprite.width = 250;
            sprite.height = 450;
            UIButton cardButton = card.GetComponent<UIButton>();
            switch (behaviour)
            {
                case Behaviour.draw:
                    EventDelegate.Add(cardButton.onClick, () => card.GetComponent<CardBehavior>().Play());
                    cardButton.enabled = true;
                    break;
                case Behaviour.show:
                    cardButton.enabled = false;
                    break;
                case Behaviour.dummy:
                    EventDelegate.Add(cardButton.onClick, () => card.GetComponent<CardBehavior>().Dummy());
                    cardButton.enabled = true;
                    break;
                case Behaviour.warhorn:
                    cardButton.enabled = false;
                    break;
            }
            card.GetComponent<UIDragScrollView>().scrollView = scrollView;
            card.GetComponent<BoxCollider>().size = new Vector3(250, 450, 1);
        }

        grid.GetComponent<UIGrid>().Reposition();
    }

    public void Hide()
    {
        totalGrid = null;
        grid.DestroyChildren();
        BlackShow.GetInstance().Show(false);
        PlayerController.GetInstance().player.SetActive(true);
        EnemyController.GetInstance().enemy.SetActive(true);
        show.SetActive(false);
    }

    public void LineChanged()
    {
        totalLine = popupList.GetItemsInt();
        Show(behaviour, PlayerController.GetInstance().grids[totalLine + 2], true);
    }
}
