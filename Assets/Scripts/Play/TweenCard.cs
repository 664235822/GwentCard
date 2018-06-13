using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenCard : Singleton<TweenCard> {
    [SerializeField] GameObject obj;

    public IEnumerator Play(Transform card)
    {
        obj.GetComponent<UISprite>().atlas = card.GetComponent<UISprite>().atlas;
        obj.GetComponent<UISprite>().spriteName = card.GetComponent<UISprite>().spriteName;
        if (card.GetComponent<CardProperty>() != null)
            obj.transform.Find("TweenEffect").GetComponent<UISprite>().spriteName = string.Format("card_effect_{0}", card.GetComponent<CardProperty>().effect.ToString());
        else
            obj.transform.Find("TweenEffect").GetComponent<UISprite>().spriteName = "";
        obj.GetComponent<TweenAlpha>().PlayForward();
        yield return new WaitForSeconds(0.5f);
        obj.transform.Find("TweenEffect").GetComponent<TweenScale>().PlayForward();
        yield return new WaitForSeconds(1.0f);
        obj.GetComponent<TweenAlpha>().PlayReverse();
        obj.transform.Find("TweenEffect").GetComponent<TweenScale>().PlayReverse();
        yield return new WaitForSeconds(1.5f);
        CoroutineManager.GetInstance().Finish();
    }

}
