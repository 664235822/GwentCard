using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBehaviorBase : MonoBehaviour {
    public string Message;
    protected bool isEnabled = true;

	// Use this for initialization
	void Start () {
        //EventDelegate.Add(button.onClick, () => Play());
	}

    public virtual void Play()
    {
        isEnabled = false;
        PlayerController.GetInstance().PlayOver(transform);
    }

    public virtual bool GetEnabled()
    {
        return isEnabled;
    }
}
