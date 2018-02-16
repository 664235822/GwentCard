using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    public GameObject player;
    public UISprite avatar_group;
    public UILabel group_label;
    public UISprite deck_realms;
    public UILabel number_label;
    public UILabel deck_realms_label;
    public GameObject cardPerfab;
    public UIAtlas[] atlas;
    public Transform[] grids;
    Constants.Group group;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize(string Group)
    {
        player.SetActive(true);
        group = (Constants.Group)System.Enum.Parse(typeof(Constants.Group), Group);
        UIAtlas totalAtlas = new UIAtlas();
        switch (group)
        {
            case Constants.Group.northern:
                avatar_group.spriteName = "player_faction_northern_realms";
                group_label.text = "北方领域";
                deck_realms.spriteName = "board_deck_northern_realms";
                totalAtlas = atlas[0];
                break;
            case Constants.Group.nilfgaardian:
                avatar_group.spriteName = "player_faction_northern_nilfgaard";
                group_label.text = "尼弗迦德";
                deck_realms.spriteName = "board_deck_nilfgaard";
                totalAtlas = atlas[1];
                break;
            case Constants.Group.monster:
                avatar_group.spriteName = "player_faction_northern_no_mans_land";
                group_label.text = "怪兽";
                deck_realms.spriteName = "board_deck_no_mans_land";
                totalAtlas = atlas[2];
                break;
            case Constants.Group.scoiatael:
                avatar_group.spriteName = "player_faction_scoiatael";
                group_label.text = "松鼠党";
                deck_realms.spriteName = "board_deck_scoiatael";
                totalAtlas = atlas[3];
                break;
        }

        XmlDocument xml = new XmlDocument();
        xml.Load(Constants.path);
        XmlElement root = xml.DocumentElement;
        XmlNode xmlNode = root.SelectSingleNode(string.Format("/root/{0}", Group));

        XmlNodeList special = xmlNode.SelectSingleNode("special").ChildNodes;
        foreach(XmlNode cardNode in special)
        {
            for (int i = 0; i < int.Parse(cardNode.Attributes["total"].Value); i++)
            {
                GameObject cardObject = Instantiate(cardPerfab, grids[0]);
                cardObject.name = cardNode.Name;
                UISprite cardSprite = cardObject.GetComponent<UISprite>();
                cardSprite.atlas = atlas[4];
                cardSprite.spriteName = cardNode.Attributes["sprite"].Value;
                CardProperty cardProperty = cardObject.GetComponent<CardProperty>();
                cardProperty.line = (Constants.Line)System.Enum.Parse(typeof(Constants.Line), cardNode.Attributes["line"].Value);
                cardProperty.effect = (Constants.Effect)System.Enum.Parse(typeof(Constants.Effect), cardNode.Attributes["effect"].Value);
                cardProperty.gold = bool.Parse(cardNode.Attributes["gold"].Value);
                cardProperty.power = int.Parse(cardNode.Attributes["power"].Value);
            }
        }

        XmlNodeList monster = xmlNode.SelectSingleNode("monster").ChildNodes;
        foreach (XmlNode cardNode in monster)
        {
            for (int i = 0; i < int.Parse(cardNode.Attributes["total"].Value); i++)
            {
                GameObject cardObject = Instantiate(cardPerfab, grids[0]);
                cardObject.name = cardNode.Name;
                UISprite cardSprite = cardObject.GetComponent<UISprite>();
                cardSprite.atlas = totalAtlas;
                cardSprite.spriteName = cardNode.Attributes["sprite"].Value;
                CardProperty cardProperty = cardObject.GetComponent<CardProperty>();
                cardProperty.line = (Constants.Line)System.Enum.Parse(typeof(Constants.Line), cardNode.Attributes["line"].Value);
                cardProperty.effect = (Constants.Effect)System.Enum.Parse(typeof(Constants.Effect), cardNode.Attributes["effect"].Value);
                cardProperty.gold = bool.Parse(cardNode.Attributes["gold"].Value);
                cardProperty.power = int.Parse(cardNode.Attributes["power"].Value);
            }
        }

        XmlNodeList neutral = xmlNode.SelectSingleNode("neutral").ChildNodes;
        foreach (XmlNode cardNode in neutral)
        {
            for (int i = 0; i < int.Parse(cardNode.Attributes["total"].Value); i++)
            {
                GameObject cardObject = Instantiate(cardPerfab, grids[0]);
                cardObject.name = cardNode.Name;
                UISprite cardSprite = cardObject.GetComponent<UISprite>();
                cardSprite.atlas = atlas[4];
                cardSprite.spriteName = cardNode.Attributes["sprite"].Value;
                CardProperty cardProperty = cardObject.GetComponent<CardProperty>();
                cardProperty.line = (Constants.Line)System.Enum.Parse(typeof(Constants.Line), cardNode.Attributes["line"].Value);
                cardProperty.effect = (Constants.Effect)System.Enum.Parse(typeof(Constants.Effect), cardNode.Attributes["effect"].Value);
                cardProperty.gold = bool.Parse(cardNode.Attributes["gold"].Value);
                cardProperty.power = int.Parse(cardNode.Attributes["power"].Value);
            }
        }

        DrawCards(10);
        Number();
    }

    void DrawCards(int index)
    {
        for (int i = 0; i < index; i++)
        {
            int random = Random.Range(0, grids[0].childCount);
            grids[0].GetChild(random).SetParent(grids[1]);
        }
    }

    void Number()
    {
        number_label.text = grids[1].childCount.ToString();
        deck_realms_label.text = grids[0].childCount.ToString();
    }
}
