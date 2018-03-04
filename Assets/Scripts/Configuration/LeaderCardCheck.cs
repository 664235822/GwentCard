using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderCardCheck : MonoBehaviour {

    public void Check()
    {
        SaveController.instance.UpdateXML(transform.parent);
        NumberController.instance.Number();
    }
}
