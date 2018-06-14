using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderController : Singleton<LeaderController> {
    public GameObject[] obj;
    [SerializeField] UISprite[] turn_indicator;
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

}
