using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    public Transform[] grids;
    public GameObject player;
    [SerializeField] UISprite avatar_group;
    [SerializeField] UILabel group_label;
    [SerializeField] UISprite deck_realms;
    [SerializeField] UILabel number_label;
    [SerializeField] UILabel deck_realms_label;
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
                totalAtlas = GameController.instance.atlas[0];
                break;
            case Constants.Group.nilfgaardian:
                avatar_group.spriteName = "player_faction_northern_nilfgaard";
                group_label.text = "尼弗迦德";
                deck_realms.spriteName = "board_deck_nilfgaard";
                totalAtlas = GameController.instance.atlas[1];
                break;
            case Constants.Group.monster:
                avatar_group.spriteName = "player_faction_northern_no_mans_land";
                group_label.text = "怪兽";
                deck_realms.spriteName = "board_deck_no_mans_land";
                totalAtlas = GameController.instance.atlas[2];
                break;
            case Constants.Group.scoiatael:
                avatar_group.spriteName = "player_faction_scoiatael";
                group_label.text = "松鼠党";
                deck_realms.spriteName = "board_deck_scoiatael";
                totalAtlas = GameController.instance.atlas[3];
                break;
        }

        XmlDocument xml = new XmlDocument();
        xml.Load(Constants.path);
        XmlElement root = xml.DocumentElement;
        XmlNode xmlNode = root.SelectSingleNode(string.Format("/root/{0}", group));

        int name = 0;
        XmlNodeList special = xmlNode.SelectSingleNode("special").ChildNodes;
        foreach(XmlNode cardNode in special)
        {
            for (int i = 0; i < int.Parse(cardNode.Attributes["total"].Value); i++)
            {
                GameObject cardObject = Instantiate(GameController.instance.cardPerfab, grids[0]);
                cardObject.name = name.ToString();
                name++;
                UISprite cardSprite = cardObject.GetComponent<UISprite>();
                cardSprite.atlas = GameController.instance.atlas[4];
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
                GameObject cardObject = Instantiate(GameController.instance.cardPerfab, grids[0]);
                cardObject.name = name.ToString();
                name++;
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
                GameObject cardObject = Instantiate(GameController.instance.cardPerfab, grids[0]);
                cardObject.name = name.ToString();
                name++;
                UISprite cardSprite = cardObject.GetComponent<UISprite>();
                cardSprite.atlas = GameController.instance.atlas[4];
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

    public void DrawCards(int index)
    {
        for (int i = 0; i < index; i++)
        {
            int random = Random.Range(0, grids[0].childCount);
            grids[0].SetParent(random, grids[1]);
        }
    }

    public void Number()
    {
        number_label.text = grids[1].childCount.ToString();
        deck_realms_label.text = grids[0].childCount.ToString();
    }
}
