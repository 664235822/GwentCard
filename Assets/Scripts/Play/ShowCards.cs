using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCards : Singleton<ShowCards> {
    public enum Behaviour { draw, show, nurse, dummy, warhorn }
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

    public void Show(Behaviour behav, Transform showGrid, bool repeat)
    {
        behaviour = behav;

        if (!repeat)
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
            case Behaviour.nurse:
                label.text = "从墓地中打出卡牌";
                totalGrid = showGrid;
                popupList.gameObject.SetActive(false);
                OKButton.gameObject.SetActive(false);
                returnButton.GetComponent<HideButton>().isDraw = true;
                break;
            case Behaviour.dummy:
                label.text = "请选择要替换的牌";
                popupList.gameObject.SetActive(true);
                OKButton.gameObject.SetActive(false);
                returnButton.GetComponent<HideButton>().isDraw = false;
                break;
            case Behaviour.warhorn:
                label.text = "战争号角";
                popupList.gameObject.SetActive(true);
                OKButton.gameObject.SetActive(true);
                returnButton.GetComponent<HideButton>().isDraw = false;
                break;
            default:
                totalGrid = showGrid;
                popupList.gameObject.SetActive(false);
                OKButton.gameObject.SetActive(false);
                returnButton.GetComponent<HideButton>().isDraw = false;
                break;
        }

        if (repeat) grid.DestroyChildren();

        for (int i = 0; i < showGrid.childCount; i++)
        {
            GameObject card = Instantiate(showGrid.GetChild(i).gameObject, grid);
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
                case Behaviour.nurse:
                    EventDelegate.Add(cardButton.onClick, () => card.GetComponent<CardBehavior>().Play());
                    cardButton.enabled = true;
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

    public void Hide(bool isDraw)
    {
        totalGrid = null;
        grid.DestroyChildren();
        BlackShow.GetInstance().Show(false);
        PlayerController.GetInstance().player.SetActive(true);
        EnemyController.GetInstance().enemy.SetActive(true);
        show.SetActive(false);
        if(isDraw)
        {
            PlayerController.GetInstance().Number();
            PowerController.GetInstance().Number();
            EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);
        }
    }

    public void LineChanged()
    {
        totalLine = popupList.GetItemsInt();
        Show(behaviour, PlayerController.GetInstance().grids[totalLine + 2], true);
    }
}
