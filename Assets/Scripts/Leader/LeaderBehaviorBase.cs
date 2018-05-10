using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBehaviorBase : MonoBehaviour {
    public string Message;
    UIButton button;

	// Use this for initialization
	void Start () {
        EventDelegate.Add(button.onClick, () => Play());
	}

    private void Awake()
    {
        button = GetComponent<UIButton>();
    }

    public virtual void Play()
    {
        button.isEnabled = false;
        PlayerController.GetInstance().PlayOver(transform);
    }
}
