using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class SaveController : MonoBehaviour {
    public static SaveController instance;
    [SerializeField] UILabel label;
    [SerializeField] Transform scrollView;
    XmlDocument xml = new XmlDocument();

    // Use this for initialization
    void Start () {
        if (File.Exists(Global.path))
            LoadXML();
        else
            Initialize();
	}

    private void Awake()
    {
        instance = this;
    }

    void Initialize()
    {
        XmlElement root = xml.CreateElement("root");

        XmlElement northern = xml.CreateElement("northern");
        northern.SetAttribute("save", false.ToString());

        XmlElement leader1 = xml.CreateElement("leader");
        XmlElement special1 = xml.CreateElement("special");
        XmlElement monster1 = xml.CreateElement("monster");
        XmlElement neutral1 = xml.CreateElement("neutral");

        northern.AppendChild(leader1);
        northern.AppendChild(special1);
        northern.AppendChild(monster1);
        northern.AppendChild(neutral1);

        XmlElement nilfgaardian = xml.CreateElement("nilfgaardian");
        nilfgaardian.SetAttribute("save", false.ToString());

        XmlElement leader2 = xml.CreateElement("leader");
        XmlElement special2 = xml.CreateElement("special");
        XmlElement monster2 = xml.CreateElement("monster");
        XmlElement neutral2 = xml.CreateElement("neutral");

        nilfgaardian.AppendChild(leader2);
        nilfgaardian.AppendChild(special2);
        nilfgaardian.AppendChild(monster2);
        nilfgaardian.AppendChild(neutral2);

        XmlElement monster = xml.CreateElement("monster");
        monster.SetAttribute("save", false.ToString());

        XmlElement leader3 = xml.CreateElement("leader");
        XmlElement special3 = xml.CreateElement("special");
        XmlElement monster3 = xml.CreateElement("monster");
        XmlElement neutral3 = xml.CreateElement("neutral");

        monster.AppendChild(leader3);
        monster.AppendChild(special3);
        monster.AppendChild(monster3);
        monster.AppendChild(neutral3);

        XmlElement scoiatael = xml.CreateElement("scoiatael");
        scoiatael.SetAttribute("save", false.ToString());

        XmlElement leader4 = xml.CreateElement("leader");
        XmlElement special4 = xml.CreateElement("special");
        XmlElement monster4 = xml.CreateElement("monster");
        XmlElement neutral4 = xml.CreateElement("neutral");

        scoiatael.AppendChild(leader4);
        scoiatael.AppendChild(special4);
        scoiatael.AppendChild(monster4);
        scoiatael.AppendChild(neutral4);

        root.AppendChild(northern);
        root.AppendChild(nilfgaardian);
        root.AppendChild(monster);
        root.AppendChild(scoiatael);

        xml.AppendChild(root);
        xml.Save(Global.path);
    }

    public void LoadXML()
    {
        string group = TagController.instance.group.ToString();
        xml.Load(Global.path);
        XmlElement root = xml.DocumentElement;
        XmlNode groupNode = root.SelectSingleNode("/root/" + group);
        Transform groupTransform = scrollView.Find(group);
        if (groupNode.Attributes["save"].Value == false.ToString())
            return;

        Transform leaderTransform = groupTransform.Find("leader");
        XmlNode leaderNode = groupNode.SelectSingleNode("leader");
        XmlNodeList leaderNodeList = leaderNode.ChildNodes;
        foreach (XmlNode cardNode in leaderNodeList)
        {
            Transform card = leaderTransform.Find(cardNode.Name);
            card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value = true;
        }

        Transform specialTransform = groupTransform.Find("special");
        XmlNode specialNode = groupNode.SelectSingleNode("special");
        XmlNodeList specialNodeList = specialNode.ChildNodes;
        foreach (XmlNode cardNode in specialNodeList)
        {
            Transform card = specialTransform.Find(cardNode.Name);
            card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value = true;
            card.GetComponent<CardPlus>().WriteTotal(int.Parse(cardNode.Attributes["total"].Value));
        }

        Transform monsterTransform = groupTransform.Find("monster");
        XmlNode monsterNode = groupNode.SelectSingleNode("monster");
        XmlNodeList monsterNodeList = monsterNode.ChildNodes;
        foreach (XmlNode cardNode in monsterNodeList)
        {
            Transform card = monsterTransform.Find(cardNode.Name);
            card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value = true;
            card.GetComponent<CardPlus>().WriteTotal(int.Parse(cardNode.Attributes["total"].Value));
        }

        Transform neutralTransform = groupTransform.Find("neutral");
        XmlNode neutralNode = groupNode.SelectSingleNode("neutral");
        XmlNodeList neutralNodeList = neutralNode.ChildNodes;
        foreach (XmlNode cardNode in neutralNodeList)
        {
            Transform card = neutralTransform.Find(cardNode.Name);
            card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value = true;
            card.GetComponent<CardPlus>().WriteTotal(int.Parse(cardNode.Attributes["total"].Value));
        }
    }

    public void UpdateXML(Transform card)
    {
        string group = TagController.instance.group.ToString();
        string list = TagController.instance.list.ToString();
        XmlElement root = xml.DocumentElement;
        XmlNode node = root.SelectSingleNode(string.Format("/root/{0}/{1}", group, list));

        if (node.SelectSingleNode(card.name) == null)
        {
            if (card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value)
            {
                XmlElement cardElement = xml.CreateElement(card.name);
                if (list != "leader")
                {
                    cardElement.SetAttribute("total", card.GetComponent<CardPlus>().total.ToString());
                    cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                    CardProperty cardProperty = card.GetComponent<CardProperty>();
                    cardElement.SetAttribute("line", cardProperty.line.ToString());
                    cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                    cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                    cardElement.SetAttribute("power", cardProperty.power.ToString());
                    if (cardProperty.muster.Length != 0)
                    {
                        for (int i = 0; i < cardProperty.muster.Length; i++)
                        {
                            XmlElement musterElement = xml.CreateElement(cardProperty.muster[i].name);
                            cardElement.AppendChild(musterElement);
                        }
                    }
                }
                node.AppendChild(cardElement);
            }
        }
        else
        {
            XmlNode cardNode = node.SelectSingleNode(card.name);
            if (card.Find("Control - Simple Checkbox").GetComponent<UIToggle>().value)
            {
                if (list != "leader")
                    cardNode.Attributes["total"].Value = card.GetComponent<CardPlus>().total.ToString();
            }
            else
                node.RemoveChild(cardNode);
        }

        NumberController.instance.Number();
    }

    public void OnClick()
    {
        try
        {
            if (NumberController.instance.leaderCount != 1)
                throw new SaveException();
            if (NumberController.instance.specialCount > 10)
                throw new SaveException();
            if (NumberController.instance.monsterCount < 25 || NumberController.instance.monsterCount > 40)
                throw new SaveException();

            XmlElement root = xml.DocumentElement;
            XmlNode groupNode = root.SelectSingleNode(string.Format("/root/{0}", TagController.instance.group.ToString()));
            groupNode.Attributes["save"].Value = true.ToString();
            xml.Save(Global.path);
            StartCoroutine(ShowLabel("保存成功"));
        }
        catch (SaveException)
        {
            StartCoroutine(ShowLabel("卡牌数量不满足条件，保存失败"));
        }
    }

    IEnumerator ShowLabel(string message)
    {
        label.gameObject.SetActive(true);
        label.text = message;
        yield return new WaitForSeconds(5f);
        label.gameObject.SetActive(false);
    }
}

class SaveException : System.Exception
{

}
