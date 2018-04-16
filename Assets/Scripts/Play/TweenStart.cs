using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenStart : Singleton<TweenStart> {
    [SerializeField] TweenAlpha obj;
    [SerializeField] UILabel label;
	
    public IEnumerator Play()
    {
        if (GameController.GetInstance().offensive)
            label.text = "你先手";
        else
            label.text = "对手先手";

        obj.PlayForward();
        yield return new WaitForSeconds(1.0f);
        obj.PlayReverse();
    }
}
