using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MonoBehaviour {

	public void OnClick()
    {
        ShowCards.instance.Hide();
    }
}
