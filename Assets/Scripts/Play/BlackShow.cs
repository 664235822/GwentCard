using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackShow : Singleton<BlackShow> {

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
