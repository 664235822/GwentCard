using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    public GameObject player;
    public UISprite avatar_group;
    public UILabel group_label;
    public UISprite deck_realms;
    Constants.Group group;

    public void Awake()
    {
        instance = this;
    }

    public void Initialize(string Group)
    {
        player.SetActive(true);
        group = (Constants.Group)System.Enum.Parse(typeof(Constants.Group), Group);
        switch (group)
        {
            case Constants.Group.northern:
                avatar_group.spriteName = "player_faction_northern_realms";
                group_label.text = "北方领域";
                deck_realms.spriteName = "board_deck_northern_realms";
                break;
            case Constants.Group.nilfgaardian:
                avatar_group.spriteName = "player_faction_northern_nilfgaard";
                group_label.text = "尼弗迦德";
                deck_realms.spriteName = "board_deck_nilfgaard";
                break;
            case Constants.Group.monster:
                avatar_group.spriteName = "player_faction_northern_no_mans_land";
                group_label.text = "怪兽";
                deck_realms.spriteName = "board_deck_no_mans_land";
                break;
            case Constants.Group.scoiatael:
                avatar_group.spriteName = "player_faction_scoiatael";
                group_label.text = "松鼠党";
                deck_realms.spriteName = "board_deck_scoiatael";
                break;
        }
    }
}
