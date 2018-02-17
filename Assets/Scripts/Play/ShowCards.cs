using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCards : MonoBehaviour {
    public static ShowCards instance;
    public enum Behaviour { draw, replace, show }
    public GameObject show;
    public UILabel label;
    public Transform grid;
    public UIScrollView scrollView;
    Behaviour behaviour;

    private void Awake()
    {
        instance = this;
    }

    public void Show(Behaviour behav,Transform ShowGrid)
    {
        behaviour = behav;
        BlackShow.instance.Show(true);
        PlayerController.instance.player.SetActive(false);
        EnemyController.instance.enemy.SetActive(false);
        show.SetActive(true);
        switch (behaviour)
        {
            case Behaviour.draw:
                label.text = "请选择要打出的牌";
                break;
            case Behaviour.replace:
                label.text = "请选择要替换的牌";
                break;
            case Behaviour.show:
                label.text = "显示卡牌";
                break;
        }

        for (int i = 0; i < ShowGrid.childCount; i++)
        {
            GameObject card = Instantiate(ShowGrid.GetChild(i).gameObject, grid);
            UISprite sprite = card.GetComponent<UISprite>();
            sprite.width = 250;
            sprite.height = 450;
            if (behaviour == Behaviour.draw)
                card.GetComponent<UIButton>().enabled = true;
            else
                card.GetComponent<UIButton>().enabled = false;
            card.GetComponent<UIDragScrollView>().scrollView = scrollView;
            card.GetComponent<BoxCollider>().size = new Vector3(250, 450, 1);
        }

        grid.GetComponent<UIGrid>().Reposition();
    }

    public void Hide()
    {
        grid.DestroyChildren();
        BlackShow.instance.Show(false);
        PlayerController.instance.player.SetActive(true);
        EnemyController.instance.enemy.SetActive(true);
        show.SetActive(false);
    }
}
