using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFalseButton : MonoBehaviour {

	public void OnClick()
    {
        ShowCards.instance.Hide();
    }
}
