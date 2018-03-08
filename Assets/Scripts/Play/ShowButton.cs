using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : MonoBehaviour {
    [SerializeField] ShowCards.Behaviour behaviour;
    [SerializeField] Transform grid;

    public void OnClick()
    {
        ShowCards.instance.Show(behaviour, grid, false);
    }
	
}
