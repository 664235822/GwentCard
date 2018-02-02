using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackShow : MonoBehaviour {
    public static BlackShow instance;

    private void Awake()
    {
        instance = this;
    }

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
