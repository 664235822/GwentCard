using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderCardCheck : MonoBehaviour {

    public void Check()
    {
        SaveController.GetInstance().UpdateXML(transform.parent);
        NumberController.GetInstance().Number();
    }
}
