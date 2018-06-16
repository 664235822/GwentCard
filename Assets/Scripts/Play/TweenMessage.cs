using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Play
{
    public class TweenMessage : Singleton<TweenMessage>
    {
        [SerializeField] TweenAlpha obj;
        [SerializeField] UILabel label;

        public IEnumerator Play(string message)
        {
            label.text = message;

            obj.PlayForward();
            yield return new WaitForSeconds(2.0f);
            obj.PlayReverse();
            yield return new WaitForSeconds(1.0f);
            CoroutineManager.GetInstance().Finish();
        }
    }
}