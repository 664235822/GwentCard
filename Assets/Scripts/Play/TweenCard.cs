using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenCard : Singleton<TweenCard> {
    [SerializeField] GameObject[] obj;
    [HideInInspector] public Transform card;

    public IEnumerator Play(int index)
    {
        if (index == 1)
            yield return new WaitForSeconds(3.0f);
        obj[index].GetComponent<UISprite>().atlas = card.GetComponent<UISprite>().atlas;
        obj[index].GetComponent<UISprite>().spriteName = card.GetComponent<UISprite>().spriteName;
        obj[index].transform.Find("TweenEffect").GetComponent<UISprite>().spriteName = string.Format("card_effect_{0}", card.GetComponent<CardProperty>().effect.ToString());
        obj[index].GetComponent<TweenAlpha>().PlayForward();
        yield return new WaitForSeconds(0.5f);
        obj[index].transform.Find("TweenEffect").GetComponent<TweenScale>().PlayForward();
        yield return new WaitForSeconds(1.0f);
        obj[index].GetComponent<TweenAlpha>().PlayReverse();
        obj[index].transform.Find("TweenEffect").GetComponent<TweenScale>().PlayReverse();
        yield return new WaitForSeconds(1.5f);
    }

}
