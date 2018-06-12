using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class EnemyController : Singleton<EnemyController> {
    public Transform[] grids;
    public GameObject obj;
    [SerializeField] UISprite avatar_group;
    [SerializeField] UILabel group_label;
    [SerializeField] UISprite deck_realms;
    [SerializeField] UILabel number_label;
    [SerializeField] UILabel deck_realms_label;
    [HideInInspector] public Global.Group group;

    public void Initialize()
    {
        obj.SetActive(true);
        int random = Random.Range(0, 4);
        UIAtlas totalAtlas = new UIAtlas();
        switch (random)
        {
            case 0:
                group = Global.Group.northern;
                avatar_group.spriteName = "player_faction_northern_realms";
                group_label.text = "北方领域";
                deck_realms.spriteName = "board_deck_northern_realms";
                totalAtlas = GameController.GetInstance().atlas[0];
                break;
            case 1:
                group = Global.Group.nilfgaardian;
                avatar_group.spriteName = "player_faction_northern_nilfgaard";
                group_label.text = "尼弗迦德";
                deck_realms.spriteName = "board_deck_nilfgaard";
                totalAtlas = GameController.GetInstance().atlas[1];
                break;
            case 2:
                group = Global.Group.monster;
                avatar_group.spriteName = "player_faction_northern_no_mans_land";
                group_label.text = "怪兽";
                deck_realms.spriteName = "board_deck_no_mans_land";
                totalAtlas = GameController.GetInstance().atlas[2];
                break;
            case 3:
                group = Global.Group.scoiatael;
                avatar_group.spriteName = "player_faction_scoiatael";
                group_label.text = "松鼠党";
                deck_realms.spriteName = "board_deck_scoiatael";
                totalAtlas = GameController.GetInstance().atlas[3];
                break;
        }

        XmlDocument xml = new XmlDocument();
        xml.Load(Global.enemyPath);
        XmlElement root = xml.DocumentElement;
        XmlNode xmlNode = root.SelectSingleNode(string.Format("/root/{0}", group));

        int name = 0;

        XmlNodeList leaderList = xmlNode.SelectSingleNode("leader").ChildNodes;
        XmlNode leaderNode = leaderList[Random.Range(0, leaderList.Count)];
        GameObject leaderObject = LeaderController.GetInstance().obj[1];
        UISprite leaderSprite = leaderObject.GetComponent<UISprite>();
        leaderSprite.atlas = totalAtlas;
        leaderSprite.spriteName = leaderNode.Attributes["sprite"].Value;
        if (leaderNode.Attributes["behavior"] != null)
        {
            leaderObject.AddComponent(System.Type.GetType(leaderNode.Attributes["behavior"].Value));
            leaderObject.GetComponent<LeaderBehaviorBase>().Message = leaderNode.Attributes["message"].Value;
        }

        XmlNodeList special = xmlNode.SelectSingleNode("special").ChildNodes;
        foreach (XmlNode cardNode in special)
        {
            for (int i = 0; i < int.Parse(cardNode.Attributes["max"].Value); i++)
            {
                GameObject cardObject = Instantiate(GameController.GetInstance().cardPerfab, grids[0]);
                cardObject.name = name.ToString();
                name++;
                UISprite cardSprite = cardObject.GetComponent<UISprite>();
                cardSprite.atlas = GameController.GetInstance().atlas[4];
                cardSprite.spriteName = cardNode.Attributes["sprite"].Value;
                CardProperty cardProperty = cardObject.GetComponent<CardProperty>();
                cardProperty.line = (Global.Line)System.Enum.Parse(typeof(Global.Line), cardNode.Attributes["line"].Value);
                cardProperty.effect = (Global.Effect)System.Enum.Parse(typeof(Global.Effect), cardNode.Attributes["effect"].Value);
                cardProperty.gold = bool.Parse(cardNode.Attributes["gold"].Value);
                cardProperty.power = int.Parse(cardNode.Attributes["power"].Value);
            }
        }

        XmlNodeList monster = xmlNode.SelectSingleNode("monster").ChildNodes;
        foreach (XmlNode cardNode in monster)
        {
            for (int i = 0; i < int.Parse(cardNode.Attributes["max"].Value); i++)
            {
                GameObject cardObject = Instantiate(GameController.GetInstance().cardPerfab, grids[0]);
                cardObject.name = name.ToString();
                name++;
                UISprite cardSprite = cardObject.GetComponent<UISprite>();
                cardSprite.atlas = totalAtlas;
                cardSprite.spriteName = cardNode.Attributes["sprite"].Value;
                CardProperty cardProperty = cardObject.GetComponent<CardProperty>();
                cardProperty.line = (Global.Line)System.Enum.Parse(typeof(Global.Line), cardNode.Attributes["line"].Value);
                cardProperty.effect = (Global.Effect)System.Enum.Parse(typeof(Global.Effect), cardNode.Attributes["effect"].Value);
                cardProperty.gold = bool.Parse(cardNode.Attributes["gold"].Value);
                cardProperty.power = int.Parse(cardNode.Attributes["power"].Value);
            }
        }

        XmlNodeList neutral = xmlNode.SelectSingleNode("neutral").ChildNodes;
        foreach (XmlNode cardNode in neutral)
        {
            for (int i = 0; i < int.Parse(cardNode.Attributes["max"].Value); i++)
            {
                GameObject cardObject = Instantiate(GameController.GetInstance().cardPerfab, grids[0]);
                cardObject.name = name.ToString();
                name++;
                UISprite cardSprite = cardObject.GetComponent<UISprite>();
                cardSprite.atlas = GameController.GetInstance().atlas[4];
                cardSprite.spriteName = cardNode.Attributes["sprite"].Value;
                CardProperty cardProperty = cardObject.GetComponent<CardProperty>();
                cardProperty.line = (Global.Line)System.Enum.Parse(typeof(Global.Line), cardNode.Attributes["line"].Value);
                cardProperty.effect = (Global.Effect)System.Enum.Parse(typeof(Global.Effect), cardNode.Attributes["effect"].Value);
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
            grids[0].GetChild(random).SetTarget(grids[1]);
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
        Transform card = grid.GetChild(random);
        CardProperty cardProperty = card.GetComponent<CardProperty>();

        switch (cardProperty.effect)
        {
            case Global.Effect.spy:
                card.SetTarget(PlayerController.GetInstance().grids[(int)cardProperty.line + 2]);
                DrawCards(2);
                break;
            case Global.Effect.clear_sky:
                WeatherController.GetInstance().ClearSky();
                goto default;
            case Global.Effect.frost:
                if (!WeatherController.GetInstance().weather[0])
                {
                    WeatherController.GetInstance().Frost();
                    card.SetTarget(WeatherController.GetInstance().grid);
                    break;
                }
                else goto default;
            case Global.Effect.fog:
                if (!WeatherController.GetInstance().weather[1])
                {
                    WeatherController.GetInstance().Fog();
                    card.SetTarget(WeatherController.GetInstance().grid);
                    break;
                }
                else goto default;
            case Global.Effect.rain:
                if (!WeatherController.GetInstance().weather[2])
                {
                    WeatherController.GetInstance().Rain();
                    card.SetTarget(WeatherController.GetInstance().grid);
                    break;
                }
                else goto default;
            case Global.Effect.nurse:
                Play(grids[5]);
                goto default;
            case Global.Effect.scorch:
                int maxPower = 0;
                for (int i = 2; i < 5; i++)
                {
                    for (int ii = 0; ii < PlayerController.GetInstance().grids[i].childCount; ii++)
                    {
                        int power = PlayerController.GetInstance().grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                        if (power > maxPower) maxPower = power;
                    }
                }
                for (int i = 2; i < 5; i++)
                {
                    for (int ii = 0; ii < grids[i].childCount; ii++)
                    {
                        int power = grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                        if (power > maxPower) maxPower = power;
                    }
                }

                for (int i = 2; i < 5; i++)
                {
                    for (int ii = PlayerController.GetInstance().grids[i].childCount - 1; ii >= 0; ii--)
                    {
                        Transform scorchCard = PlayerController.GetInstance().grids[i].GetChild(ii);
                        if (scorchCard.GetComponent<CardBehavior>().totalPower == maxPower && !scorchCard.GetComponent<CardProperty>().gold)
                            scorchCard.SetTarget(PlayerController.GetInstance().grids[5]);
                    }
                }
                for (int i = 2; i < 5; i++)
                {
                    for (int ii = EnemyController.GetInstance().grids[i].childCount - 1; ii >= 0; ii--)
                    {
                        Transform scorchCard = EnemyController.GetInstance().grids[i].GetChild(ii);
                        if (scorchCard.GetComponent<CardBehavior>().totalPower == maxPower && !scorchCard.GetComponent<CardProperty>().gold)
                            scorchCard.SetTarget(EnemyController.GetInstance().grids[5]);
                    }
                }
                goto default;
            case Global.Effect.dummy:
                for (int i = 2; i < 5; i++)
                {
                    if (grids[i].childCount == 0) continue;
                    int dummyRandom = Random.Range(0, grids[i].childCount);
                    card.SetTarget(grids[i]);
                    grids[i].GetChild(dummyRandom).SetTarget(grid);
                    break;
                }
                break;
            case Global.Effect.warhorn:
                if (cardProperty.line == Global.Line.empty)
                {
                    int line = Random.Range(0, 3);
                    if (!WarhornController.GetInstance().enemyWarhorn[line])
                    {
                        WarhornController.GetInstance().enemyWarhorn[line] = true;
                        card.SetTarget(WarhornController.GetInstance().enemyGrids[line]);
                    }
                    else
                        card.SetTarget(grids[5]);
                    break;
                }
                else
                {
                    WarhornController.GetInstance().enemyWarhorn[(int)cardProperty.line] = true;
                    goto default;
                }
            case Global.Effect.muster:
                int musterIndex = 0;
                for (int i = 0; i < MusterController.GetInstance().musterCards.Length; i++)
                    for (int ii = 0; ii < MusterController.GetInstance().musterCards[i].Length; ii++)
                        if (grid.GetChild(random).GetComponent<UISprite>().spriteName == MusterController.GetInstance().musterCards[i][ii])
                            musterIndex = i;

                for (int i = 0; i < MusterController.GetInstance().musterCards[musterIndex].Length; i++)
                    for (int ii = grids[0].childCount - 1; ii >= 0; ii--)
                    {
                        Transform musterCard = grids[0].GetChild(ii);
                        if (musterCard.GetComponent<UISprite>().spriteName == MusterController.GetInstance().musterCards[musterIndex][i])
                            musterCard.SetTarget(grids[(int)card.GetComponent<CardProperty>().line + 2]);
                    }

                goto default;
            case Global.Effect.agile:
                int agileRandom = Random.Range(0, 2);
                card.SetTarget(grids[agileRandom + 2]);
                break;
            default:
                card.SetTarget(grids[(int)cardProperty.line + 2]);
                break;
        }

        CoroutineManager.GetInstance().AddTask(TweenCard.GetInstance().Play(card));
        Number();
        PowerController.GetInstance().Number();
    }
}
