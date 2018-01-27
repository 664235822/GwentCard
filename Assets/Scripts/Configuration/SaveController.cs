using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class SaveController : MonoBehaviour {
    public static SaveController instance;
    public UILabel label;
    public Transform scrollView;
    XmlDocument xml;
    
	// Use this for initialization
	void Start () {
        if (File.Exists(Constants.path))
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
        xml = new XmlDocument();
        xml.Load(Constants.path);

        XmlElement northern = xml.CreateElement("northern");

        XmlElement leader1 = xml.CreateElement("leader");
        XmlElement special1 = xml.CreateElement("special");
        XmlElement monster1 = xml.CreateElement("monster");
        XmlElement neutral1 = xml.CreateElement("neutral");

        northern.AppendChild(leader1);
        northern.AppendChild(special1);
        northern.AppendChild(monster1);
        northern.AppendChild(neutral1);

        XmlElement nilfgaardian = xml.CreateElement("nilfgaardian");

        XmlElement leader2 = xml.CreateElement("leader");
        XmlElement special2 = xml.CreateElement("special");
        XmlElement monster2 = xml.CreateElement("monster");
        XmlElement neutral2 = xml.CreateElement("neutral");

        nilfgaardian.AppendChild(leader2);
        nilfgaardian.AppendChild(special2);
        nilfgaardian.AppendChild(monster2);
        nilfgaardian.AppendChild(neutral2);

        XmlElement monster = xml.CreateElement("monster");

        XmlElement leader3 = xml.CreateElement("leader");
        XmlElement special3 = xml.CreateElement("special");
        XmlElement monster3 = xml.CreateElement("monster");
        XmlElement neutral3 = xml.CreateElement("neutral");

        monster.AppendChild(leader3);
        monster.AppendChild(special3);
        monster.AppendChild(monster3);
        monster.AppendChild(neutral3);

        XmlElement scoiatael = xml.CreateElement("scoiatael");

        XmlElement leader4 = xml.CreateElement("leader");
        XmlElement special4 = xml.CreateElement("special");
        XmlElement monster4 = xml.CreateElement("monster");
        XmlElement neutral4 = xml.CreateElement("neutral");

        scoiatael.AppendChild(leader4);
        scoiatael.AppendChild(special4);
        scoiatael.AppendChild(monster4);
        scoiatael.AppendChild(neutral4);

        xml.AppendChild(northern);
        xml.AppendChild(nilfgaardian);
        xml.AppendChild(monster);
        xml.AppendChild(scoiatael);

        xml.Save(Constants.path);
    }

    public void LoadXML()
    {
        
    }

    public void UpdateXML()
    {

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
