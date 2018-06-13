using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderController : Singleton<LeaderController> {
    public GameObject[] obj;
    public Transform grid;
    [SerializeField] UISprite[] turn_indicator;
    [SerializeField] Transform leaderShow;
    [SerializeField] UIScrollView scrollView;
    readonly string[] turn_indicator_string = { "player_turn_indicator_opponent", "player_turn_indicator_self" };

    public void PlayerTurnIndicator()
    {
        turn_indicator[0].gameObject.SetActive(true);
        turn_indicator[1].gameObject.SetActive(false);
        LeaderBehaviorBase leaderBehavior = obj[0].GetComponent<LeaderBehaviorBase>();
        if (leaderBehavior != null && leaderBehavior.GetEnabled())
            turn_indicator[0].spriteName = turn_indicator_string[1];
        else
            turn_indicator[0].spriteName = turn_indicator_string[0];
    }

    public IEnumerator EnemyTurnIndicator()
    {
        turn_indicator[1].gameObject.SetActive(true);
        turn_indicator[0].gameObject.SetActive(false);
        LeaderBehaviorBase leaderBehavior = obj[1].GetComponent<LeaderBehaviorBase>();
        if (leaderBehavior != null && leaderBehavior.GetEnabled())
            turn_indicator[1].spriteName = turn_indicator_string[1];
        else
            turn_indicator[1].spriteName = turn_indicator_string[0];
        yield return new WaitForSeconds(3.0f);
        PlayerTurnIndicator();
    }

    public void Show()
    {
        LeaderBehaviorBase leaderBehavior = obj[0].GetComponent<LeaderBehaviorBase>();
        if (leaderBehavior == null)
            return;

        BlackShow.GetInstance().Show(true);
        PlayerController.GetInstance().obj.SetActive(false);
        EnemyController.GetInstance().obj.SetActive(false);
        leaderShow.gameObject.SetActive(true);

        GameObject card = Instantiate(obj[0], grid);
        UISprite sprite = card.GetComponent<UISprite>();
        sprite.width = 250;
        sprite.height = 450;
        card.GetComponent<UIDragScrollView>().scrollView = scrollView;
        card.GetComponent<BoxCollider>().size = new Vector3(250, 450, 1);
        grid.GetComponent<UIGrid>().Reposition();

        leaderShow.Find("Message").GetComponent<UILabel>().text =leaderBehavior.Message;
        UIButton okbutton = leaderShow.Find("Control - Colored Button").GetComponent<UIButton>();
        okbutton.isEnabled = leaderBehavior.GetEnabled();
        okbutton.onClick.Clear();
        EventDelegate.Add(okbutton.onClick, () => leaderBehavior.Play());
    }

    public void Hide()
    {
        grid.DestroyChildren();
        BlackShow.GetInstance().Show(false);
        PlayerController.GetInstance().obj.SetActive(true);
        EnemyController.GetInstance().obj.SetActive(true);
        leaderShow.gameObject.SetActive(false);
    }
}
