using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCards : MonoBehaviour {
    public static ShowCards instance;
    public enum Behaviour { draw, show, dummy }
    public Transform grid;
    [SerializeField] GameObject show;
    [SerializeField] UILabel label;
    [SerializeField] UIScrollView scrollView;
    [SerializeField] UIPopupList popupList;
    [HideInInspector] public Transform totalGrid;
    Behaviour behaviour;

    private void Awake()
    {
        instance = this;
    }

    public void Show(Behaviour behav, Transform ShowGrid, bool Repeat)
    {
        behaviour = behav;
        totalGrid = ShowGrid;

        if (!Repeat)
        {
            BlackShow.instance.Show(true);
            PlayerController.instance.player.SetActive(false);
            EnemyController.instance.enemy.SetActive(false);
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
                popupList.value = "近战";
                break;
            default:
                popupList.gameObject.SetActive(false);
                break;
        }

        if (Repeat) grid.DestroyChildren();

        for (int i = 0; i < ShowGrid.childCount; i++)
        {
            GameObject card = Instantiate(ShowGrid.GetChild(i).gameObject, grid);
            UISprite sprite = card.GetComponent<UISprite>();
            sprite.width = 250;
            sprite.height = 450;
            UIButton button = card.GetComponent<UIButton>();
            if (behaviour == Behaviour.draw) button.enabled = true;
            else button.enabled = false;
            card.GetComponent<UIDragScrollView>().scrollView = scrollView;
            card.GetComponent<BoxCollider>().size = new Vector3(250, 450, 1);
        }

        grid.GetComponent<UIGrid>().Reposition();
    }

    public void Hide()
    {
        totalGrid = null;
        grid.DestroyChildren();
        BlackShow.instance.Show(false);
        PlayerController.instance.player.SetActive(true);
        EnemyController.instance.enemy.SetActive(true);
        show.SetActive(false);
    }

    public void LineChanged()
    {
        Show(behaviour, PlayerController.instance.grids[popupList.GetItemsInt() + 2], true);
    }
}
