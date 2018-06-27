using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyScoiataelBehavior1 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            int index = -1;
            for (int i = 0; i < EnemyController.GetInstance().grids[0].childCount; i++)
                if (EnemyController.GetInstance().grids[0].GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.frost) index = i;

            if (index != -1)
            {
                WeatherController.GetInstance().Frost();
                EnemyController.GetInstance().grids[0].GetChild(index).SetTarget(WeatherController.GetInstance().grid);
            }

            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return PowerController.GetInstance().enemy[0] + 10 < PowerController.GetInstance().player[0] &&
                    !WeatherController.GetInstance().weather[0] &&
                    isEnabled;
            }
        }
    }
}