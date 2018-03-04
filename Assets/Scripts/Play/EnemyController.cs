using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class EnemyController : MonoBehaviour {
    public static EnemyController instance;
    public Transform[] grids;
    public GameObject enemy;
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

    public void Initialize()
    {
        enemy.SetActive(true);
        int random = Random.Range(0, 4);
        UIAtlas totalAtlas = new UIAtlas();
        switch (random)
        {
            case 0:
                group = Constants.Group.northern;
                avatar_group.spriteName = "player_faction_northern_realms";
                group_label.text = "北方领域";
                deck_realms.spriteName = "board_deck_northern_realms";
                totalAtlas = GameController.instance.atlas[0];
                break;
            case 1:
                group = Constants.Group.nilfgaardian;
                avatar_group.spriteName = "player_faction_northern_nilfgaard";
                group_label.text = "尼弗迦德";
                deck_realms.spriteName = "board_deck_nilfgaard";
                totalAtlas = GameController.instance.atlas[1];
                break;
            case 2:
                group = Constants.Group.monster;
                avatar_group.spriteName = "player_faction_northern_no_mans_land";
                group_label.text = "怪兽";
                deck_realms.spriteName = "board_deck_no_mans_land";
                totalAtlas = GameController.instance.atlas[2];
                break;
            case 3:
                group = Constants.Group.scoiatael;
                avatar_group.spriteName = "player_faction_scoiatael";
                group_label.text = "松鼠党";
                deck_realms.spriteName = "board_deck_scoiatael";
                totalAtlas = GameController.instance.atlas[3];
                break;
        }

        XmlDocument xml = new XmlDocument();
        xml.Load(Constants.enemyPath);
        XmlElement root = xml.DocumentElement;
        XmlNode xmlNode = root.SelectSingleNode(string.Format("/root/{0}", group));

        int name = 0;
        XmlNodeList special = xmlNode.SelectSingleNode("special").ChildNodes;
        foreach (XmlNode cardNode in special)
        {
            for (int i = 0; i < int.Parse(cardNode.Attributes["max"].Value); i++)
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
            for (int i = 0; i < int.Parse(cardNode.Attributes["max"].Value); i++)
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
            for (int i = 0; i < int.Parse(cardNode.Attributes["max"].Value); i++)
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

    void DrawCards(int index)
    {
        for (int i = 0; i < index; i++)
        {
            int random = Random.Range(0, grids[0].childCount);
            grids[0].SetParent(random, grids[1]);
        }
    }

    void Number()
    {
        number_label.text = grids[1].childCount.ToString();
        deck_realms_label.text = grids[0].childCount.ToString();
    }

    public void Play(Transform grid)
    {
        if (grid.childCount == 0) return;
        int random = Random.Range(0, grid.childCount);
        CardProperty cardProperty = grid.GetChild(random).GetComponent<CardProperty>();

        switch (cardProperty.effect)
        {
            case Constants.Effect.spy:
                switch (cardProperty.line)
                {
                    case Constants.Line.melee:
                        grid.SetParent(random, PlayerController.instance.grids[2]);
                        break;
                    case Constants.Line.ranged:
                        grid.SetParent(random, PlayerController.instance.grids[3]);
                        break;
                    case Constants.Line.siege:
                        grid.SetParent(random, PlayerController.instance.grids[4]);
                        break;
                }
                DrawCards(2);
                break;
            case Constants.Effect.clear_sky:
                WeatherController.instance.ClearSky();
                goto default;
            case Constants.Effect.frost:
                if (!WeatherController.instance.frost) WeatherController.instance.Frost();
                goto default;
            case Constants.Effect.fog:
                if (!WeatherController.instance.fog) WeatherController.instance.Fog();
                goto default;
            case Constants.Effect.rain:
                if (!WeatherController.instance.fog)   WeatherController.instance.Rain();
                goto default;
            case Constants.Effect.scorch:
                int maxPower = 0;
                for (int i = 2; i < 5; i++)
                {
                    for (int ii = 0; ii < PlayerController.instance.grids[i].childCount; ii++)
                    {
                        int power = PlayerController.instance.grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                        if (power > maxPower) maxPower = power;
                    }
                }
                for (int i = 2; i < 5; i++)
                {
                    for (int ii = 0; ii < EnemyController.instance.grids[i].childCount; ii++)
                    {
                        int power = EnemyController.instance.grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                        if (power > maxPower) maxPower = power;
                    }
                }

                for (int i = 2; i < 5; i++)
                {
                    for (int ii = 0; ii < PlayerController.instance.grids[i].childCount; ii++)
                    {
                        if (PlayerController.instance.grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower == maxPower)
                            PlayerController.instance.grids[i].SetParent(ii, PlayerController.instance.grids[5]);
                    }
                }
                for (int i = 2; i < 5; i++)
                {
                    for (int ii = 0; ii < EnemyController.instance.grids[i].childCount; ii++)
                    {
                        if (EnemyController.instance.grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower == maxPower)
                            EnemyController.instance.grids[i].SetParent(ii, EnemyController.instance.grids[5]);
                    }
                }
                goto default;
            default:
                switch (cardProperty.line)
                {
                    case Constants.Line.melee:
                        grid.SetParent(random, grids[2]);
                        break;
                    case Constants.Line.ranged:
                        grid.SetParent(random, grids[3]);
                        break;
                    case Constants.Line.siege:
                        grid.SetParent(random, grids[4]);
                        break;
                    case Constants.Line.empty:
                        grid.SetParent(random, grids[5]);
                        break;
                }
                break;
        }

        Number();
        PowerController.instance.Number();
    }
}
