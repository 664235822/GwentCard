using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlus : MonoBehaviour {
    UILabel label;
    UIToggle toggle;
    public int total;
    int max;

    private void Awake()
    {
        label = transform.Find("Label").GetComponent<UILabel>();
        toggle = transform.Find("Control - Simple Checkbox").GetComponent<UIToggle>();
        string[] text = label.text.Split('/');
        total = int.Parse(text[0]);
        max = int.Parse(text[1]);
    }

    public void Plus()
    {
        if (total == max)
            return;

        total++;
        PrintLabel();
        NumberController.instance.Number();
        SaveController.instance.UpdateXML(transform);
    }

    public void Minus()
    {
        if (total == 0)
            return;

        total--;
        if (total == 0)
            toggle.value = false;

        PrintLabel();
        NumberController.instance.Number();
        SaveController.instance.UpdateXML(transform);
    }

    public void WriteTotal(int index)
    {
        total = index;
        PrintLabel();
    }

    public void Check()
    {
        if (toggle.value == true && total == 0)
        {
            total++;
            PrintLabel();
        }

        NumberController.instance.Number();
        SaveController.instance.UpdateXML(transform);
    }

    void PrintLabel()
    {
        label.text = string.Format("{0}/{1}", total, max);
    }
}
