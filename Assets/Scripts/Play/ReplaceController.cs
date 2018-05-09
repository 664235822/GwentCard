using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceController : Singleton<ReplaceController> {
    public UILabel label;
    public Transform grid;
    [SerializeField] GameObject obj;
    [SerializeField] UIScrollView scrollView;
    [HideInInspector] public int index = 0;

    public void Show(bool repeat)
    {
        if (!repeat)
        {
            obj.SetActive(true);
            PlayerController.GetInstance().obj.SetActive(false);
            EnemyController.GetInstance().obj.SetActive(false);
        }
        else grid.DestroyChildren();

        for (int i = 0; i < PlayerController.GetInstance().grids[1].childCount; i++)
        {
            GameObject card = Instantiate(PlayerController.GetInstance().grids[1].GetChild(i).gameObject, grid);
            UISprite sprite = card.GetComponent<UISprite>();
            sprite.width = 250;
            sprite.height = 450;
            UIButton cardButton = card.GetComponent<UIButton>();
            EventDelegate.Add(cardButton.onClick, () => card.GetComponent<CardBehavior>().Replace());
            cardButton.enabled = true;
            card.GetComponent<UIDragScrollView>().scrollView = scrollView;
            card.GetComponent<BoxCollider>().size = new Vector3(250, 450, 1);
        }

        grid.GetComponent<UIGrid>().Reposition();
    }

    public void Hide()
    {
        obj.SetActive(false);
        BlackShow.GetInstance().Show(false);
        PlayerController.GetInstance().obj.SetActive(true);
        EnemyController.GetInstance().obj.SetActive(true);
        GameController.GetInstance().StartGame();
    }
}
