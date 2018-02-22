using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : MonoBehaviour {
    public ShowCards.Behaviour behaviour;
    public Transform grid;

    public void OnClick()
    {
        ShowCards.instance.Show(behaviour, grid);
    }
	
}
