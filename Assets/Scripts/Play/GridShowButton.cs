using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridShowButton : MonoBehaviour {

	public void OnClick()
    {
        ShowCards.instance.Show(ShowCards.Behaviour.draw);
    }
}
