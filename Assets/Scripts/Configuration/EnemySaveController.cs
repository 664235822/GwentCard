using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class EnemySaveController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(!File.Exists(Constants.enemyPath))
        {
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("root");

            XmlElement northern = xml.CreateElement("northern");
            Transform groupTransform = TagController.instance.groups[0].transform;

            XmlElement leader1 = xml.CreateElement("leader");
            Transform leader1Transform = groupTransform.Find("leader");
            for (int i = 0; i < leader1Transform.childCount; i++)
            {
                Transform card = leader1Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                leader1.AppendChild(cardElement);
            }

            XmlElement special1 = xml.CreateElement("special");
            Transform special1Transform = groupTransform.Find("special");
            for (int i = 0; i < special1Transform.childCount; i++)
            {
                Transform card = special1Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                special1.AppendChild(cardElement);
            }

            XmlElement monster1 = xml.CreateElement("monster");
            Transform monster1Transform = groupTransform.Find("monster");
            for (int i = 0; i < monster1Transform.childCount; i++)
            {
                Transform card = monster1Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                monster1.AppendChild(cardElement);
            }

            XmlElement neutral1 = xml.CreateElement("neutral");
            Transform neutral1Transform = groupTransform.Find("neutral");
            for (int i = 0; i < neutral1Transform.childCount; i++)
            {
                Transform card = neutral1Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                neutral1.AppendChild(cardElement);
            }

            northern.AppendChild(leader1);
            northern.AppendChild(special1);
            northern.AppendChild(monster1);
            northern.AppendChild(neutral1);

            XmlElement nilfgaardian = xml.CreateElement("nilfgaardian");
            groupTransform= TagController.instance.groups[1].transform;

            XmlElement leader2 = xml.CreateElement("leader");
            Transform leader2Transform = groupTransform.Find("leader");
            for (int i = 0; i < leader2Transform.childCount; i++)
            {
                Transform card = leader2Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                leader2.AppendChild(cardElement);
            }

            XmlElement special2 = xml.CreateElement("special");
            Transform special2Transform = groupTransform.Find("special");
            for (int i = 0; i < special2Transform.childCount; i++)
            {
                Transform card = special2Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                special2.AppendChild(cardElement);
            }

            XmlElement monster2 = xml.CreateElement("monster");
            Transform monster2Transform = groupTransform.Find("monster");
            for (int i = 0; i < monster2Transform.childCount; i++)
            {
                Transform card = monster2Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                monster2.AppendChild(cardElement);
            }

            XmlElement neutral2 = xml.CreateElement("neutral");
            Transform neutral2Transform = groupTransform.Find("neutral");
            for (int i = 0; i < neutral2Transform.childCount; i++)
            {
                Transform card = neutral2Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                neutral2.AppendChild(cardElement);
            }

            nilfgaardian.AppendChild(leader2);
            nilfgaardian.AppendChild(special2);
            nilfgaardian.AppendChild(monster2);
            nilfgaardian.AppendChild(neutral2);

            XmlElement monster = xml.CreateElement("monster");
            groupTransform = TagController.instance.groups[2].transform;

            XmlElement leader3 = xml.CreateElement("leader");
            Transform leader3Transform = groupTransform.Find("leader");
            for (int i = 0; i < leader3Transform.childCount; i++)
            {
                Transform card = leader3Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                leader3.AppendChild(cardElement);
            }

            XmlElement special3 = xml.CreateElement("special");
            Transform special3Transform = groupTransform.Find("special");
            for (int i = 0; i < special3Transform.childCount; i++)
            {
                Transform card = special3Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                special3.AppendChild(cardElement);
            }

            XmlElement monster3 = xml.CreateElement("monster");
            Transform monster3Transform = groupTransform.Find("monster");
            for (int i = 0; i < monster3Transform.childCount; i++)
            {
                Transform card = monster3Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                monster3.AppendChild(cardElement);
            }

            XmlElement neutral3 = xml.CreateElement("neutral");
            Transform neutral3Transform = groupTransform.Find("neutral");
            for (int i = 0; i < neutral3Transform.childCount; i++)
            {
                Transform card = neutral3Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                neutral3.AppendChild(cardElement);
            }

            monster.AppendChild(leader3);
            monster.AppendChild(special3);
            monster.AppendChild(monster3);
            monster.AppendChild(neutral3);

            XmlElement scoiatael = xml.CreateElement("scoiatael");
            groupTransform = TagController.instance.groups[3].transform;

            XmlElement leader4 = xml.CreateElement("leader");
            Transform leader4Transform = groupTransform.Find("leader");
            for (int i = 0; i < leader4Transform.childCount; i++)
            {
                Transform card = leader4Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                leader4.AppendChild(cardElement);
            }

            XmlElement special4 = xml.CreateElement("special");
            Transform special4Transform = groupTransform.Find("special");
            for (int i = 0; i < special4Transform.childCount; i++)
            {
                Transform card = special4Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                special4.AppendChild(cardElement);
            }

            XmlElement monster4 = xml.CreateElement("monster");
            Transform monster4Transform = groupTransform.Find("monster");
            for (int i = 0; i < monster4Transform.childCount; i++)
            {
                Transform card = monster4Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                monster4.AppendChild(cardElement);
            }

            XmlElement neutral4 = xml.CreateElement("neutral");
            Transform neutral4Transform = groupTransform.Find("neutral");
            for (int i = 0; i < neutral4Transform.childCount; i++)
            {
                Transform card = neutral4Transform.GetChild(i);
                XmlElement cardElement = xml.CreateElement(card.name);
                cardElement.SetAttribute("max", card.GetComponent<CardPlus>().max.ToString());
                cardElement.SetAttribute("sprite", card.GetComponent<UISprite>().spriteName);
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                cardElement.SetAttribute("line", cardProperty.line.ToString());
                cardElement.SetAttribute("effect", cardProperty.effect.ToString());
                cardElement.SetAttribute("gold", cardProperty.gold.ToString());
                cardElement.SetAttribute("power", cardProperty.power.ToString());
                if (cardProperty.muster.Length != 0)
                {
                    for (int ii = 0; ii < cardProperty.muster.Length; ii++)
                    {
                        XmlElement musterElement = xml.CreateElement(cardProperty.muster[ii].name);
                        cardElement.AppendChild(musterElement);
                    }
                }
                neutral4.AppendChild(cardElement);
            }

            scoiatael.AppendChild(leader4);
            scoiatael.AppendChild(special4);
            scoiatael.AppendChild(monster4);
            scoiatael.AppendChild(neutral4);

            root.AppendChild(northern);
            root.AppendChild(nilfgaardian);
            root.AppendChild(monster);
            root.AppendChild(scoiatael);

            xml.AppendChild(root);
            xml.Save(Constants.enemyPath);
        }
	}
	
	
}
