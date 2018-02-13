using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class ChooseGroup : MonoBehaviour {
	// Use this for initialization
	void Start () {
        UIButton button = GetComponent<UIButton>();
        if (!File.Exists(Constants.path))
        {
            button.isEnabled = false;
            return;
        }

        XmlDocument xml = new XmlDocument();
        xml.Load(Constants.path);
        XmlElement root = xml.DocumentElement;
        XmlNode group = root.SelectSingleNode(string.Format("/root/{0}", name));
        if (group.Attributes["save"].Value == true.ToString())
            button.isEnabled = true;
        else
            button.isEnabled = false;
	}

    public void OnClick()
    {
        BlackShow.instance.Show(false);
        transform.parent.gameObject.SetActive(false);
        PlayerController.instance.Initialize(name);
        EnemyController.instance.Initialize();
    }
}
