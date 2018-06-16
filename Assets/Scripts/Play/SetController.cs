using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard;

namespace GwentCard.Play
{
    public class SetController : Singleton<SetController>
    {
        [SerializeField] GameObject obj;
        [SerializeField] UIToggle toggle;
        [SerializeField] UIButton button;

        public void Show()
        {
            BlackShow.GetInstance().Show(true);
            obj.SetActive(true);
            PlayerController.GetInstance().obj.SetActive(false);
            EnemyController.GetInstance().obj.SetActive(false);
        }

        public void Check()
        {
            if (toggle.value)
                MusicManager.GetInstance().Continue();
            else
                MusicManager.GetInstance().Pause();
        }

        public void Return()
        {
            BlackShow.GetInstance().Show(false);
            obj.SetActive(false);
            PlayerController.GetInstance().obj.SetActive(true);
            EnemyController.GetInstance().obj.SetActive(true);
        }
    }
}
