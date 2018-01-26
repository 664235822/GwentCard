using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour {
    public UILabel label;
    
	// Use this for initialization
	void Start () {
		
	}
	
	public void OnClick()
    {
        try
        {
            if (NumberController.instance.leaderCount != 1)
                throw new SaveException();
            if (NumberController.instance.specialCount > 10)
                throw new SaveException();
            if (NumberController.instance.monsterCount < 25 || NumberController.instance.monsterCount > 40)
                throw new SaveException();

            StartCoroutine(ShowLabel("保存成功"));
        }
        catch (SaveException)
        {
            StartCoroutine(ShowLabel("卡牌数量不满足条件，保存失败"));
        }
    }

    IEnumerator ShowLabel(string message)
    {
        label.gameObject.SetActive(true);
        label.text = message;
        yield return new WaitForSeconds(5f);
        label.gameObject.SetActive(false);
    }
}

class SaveException : System.Exception
{

}
