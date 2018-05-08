using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoiataelController : MonoBehaviour {
    UIButton button;
    UIPopupList popupList;

	// Use this for initialization
	void Start () {
        if (transform.parent.GetComponent<UIButton>().isEnabled)
            button.isEnabled = true;
        else
            button.isEnabled = false;

        OnValueChange();
    }

    private void Awake()
    {
        button = GetComponent<UIButton>();
        popupList = GetComponent<UIPopupList>();
    }

    public void OnValueChange()
    {
        if (popupList.value == "先手")
            GameController.GetInstance().offensive = true;
        else
            GameController.GetInstance().offensive = false;
    }
}
