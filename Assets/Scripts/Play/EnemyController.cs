using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public static EnemyController instance;
    public GameObject enemy;
    public UISprite avatar_group;
    public UILabel group_label;
    public UISprite deck_realms;
    Constants.Group group;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        enemy.SetActive(true);
        int random = Random.Range(0, 4);
        switch(random)
        {
            case 1:
                group = Constants.Group.northern;
                avatar_group.spriteName = "player_faction_northern_realms";
                group_label.text = "北方领域";
                deck_realms.spriteName = "board_deck_northern_realms";
                break;
            case 2:
                group = Constants.Group.nilfgaardian;
                avatar_group.spriteName = "player_faction_northern_nilfgaard";
                group_label.text = "尼弗迦德";
                deck_realms.spriteName = "board_deck_nilfgaard";
                break;
            case 3:
                group = Constants.Group.monster;
                avatar_group.spriteName = "player_faction_northern_no_mans_land";
                group_label.text = "怪兽";
                deck_realms.spriteName = "board_deck_no_mans_land";
                break;
            case 4:
                group = Constants.Group.scoiatael;
                avatar_group.spriteName = "player_faction_scoiatael";
                group_label.text = "松鼠党";
                deck_realms.spriteName = "board_deck_scoiatael";
                break;
        }
    }
}
