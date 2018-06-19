using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

namespace GwentCard.Play
{
    public class ChooseGroup : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            UIButton button = GetComponent<UIButton>();
            if (!File.Exists(Global.path))
            {
                button.isEnabled = false;
                return;
            }

            XmlDocument xml = new XmlDocument();
            xml.Load(Global.path);
            XmlElement root = xml.DocumentElement;
            XmlNode group = root.SelectSingleNode(string.Format("/root/{0}", name));
            button.isEnabled = bool.Parse(group.Attributes["save"].Value);
        }

        public void OnClick()
        {
            transform.parent.gameObject.SetActive(false);
            GameController.GetInstance().Initialize(name);
        }
    }
}