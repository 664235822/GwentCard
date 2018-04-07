using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetController : Singleton<SetController> {
    [SerializeField] GameObject set;
    [SerializeField] UIToggle toggle;
    [SerializeField] UIButton button;

    public void Show()
    {
        BlackShow.GetInstance().Show(true);
        set.SetActive(true);
        PlayerController.GetInstance().player.SetActive(false);
        EnemyController.GetInstance().enemy.SetActive(false);
    }

    public void Check()
    {
        if (toggle.value)
            MusicController.GetInstance().Continue();
        else
            MusicController.GetInstance().Pause();
    }

    public void Return()
    {
        BlackShow.GetInstance().Show(false);
        set.SetActive(false);
        PlayerController.GetInstance().player.SetActive(true);
        EnemyController.GetInstance().enemy.SetActive(true);
    }
}
