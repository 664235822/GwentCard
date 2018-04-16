using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenCard : Singleton<TweenCard> {
    [SerializeField] GameObject obj;

    public IEnumerator Play(Transform card)
    {
        obj.GetComponent<UISprite>().atlas = card.GetComponent<UISprite>().atlas;
        obj.GetComponent<UISprite>().spriteName = card.GetComponent<UISprite>().spriteName;
        obj.transform.Find("TweenEffect").GetComponent<UISprite>().spriteName = string.Format("card_effect_{0}", card.GetComponent<CardProperty>().effect.ToString());
        obj.GetComponent<TweenAlpha>().PlayForward();
        yield return new WaitForSeconds(2.0f);
        obj.transform.Find("TweenEffect").GetComponent<TweenAlpha>().PlayForward();
        yield return new WaitForSeconds(4.0f);
        obj.GetComponent<TweenAlpha>().PlayReverse();
    }

}
