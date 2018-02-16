using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCards : MonoBehaviour {
    public static ShowCards instance;
    public enum Behaviour { draw, replace, show }
    public GameObject show;
    public UILabel label;
    public Transform grid;
    public UIScrollView ScrollView;
    Behaviour behaviour;

    private void Awake()
    {
        instance = this;
    }

    public void Show(Behaviour behav)
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
        GetCards();
    }

    public void Hide()
    {
        grid.DestroyChildren();
        BlackShow.instance.Show(false);
        PlayerController.instance.player.SetActive(true);
        EnemyController.instance.enemy.SetActive(true);
        show.SetActive(false);
    }

    void GetCards()
    {
        for (int i = 0; i < PlayerController.instance.grids[1].childCount; i++)
        {
            GameObject card = Instantiate(PlayerController.instance.grids[1].GetChild(i).gameObject, grid);
            UISprite sprite = card.GetComponent<UISprite>();
            sprite.width = 250;
            sprite.height = 450;
            card.GetComponent<UIButton>().enabled = true;
            card.GetComponent<UIDragScrollView>().scrollView = ScrollView;
            card.GetComponent<BoxCollider>().size = new Vector3(250, 450, 1);
        }

        grid.GetComponent<UIGrid>().Reposition();
    }
}
