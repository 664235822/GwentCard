using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MonoBehaviour {
    public bool isDraw;

	public void OnClick()
    {
        ShowCards.GetInstance().Hide(isDraw);
    }
}
