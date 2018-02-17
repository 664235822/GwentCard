using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawShowButton : MonoBehaviour {
    public Transform grid;

	public void OnClick()
    {
        ShowCards.instance.Show(ShowCards.Behaviour.draw, grid);
    }
}
