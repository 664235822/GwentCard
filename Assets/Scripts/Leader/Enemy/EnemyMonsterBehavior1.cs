using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyMonsterBehavior1 : EnemyLeaderBehavior
    {
        ArrayList cardList = new ArrayList();

        public sealed override void Play()
        {
            Transform grid = PlayerController.GetInstance().grids[0];
            for (int i = 0; i < grid.childCount; i++)
            {
                if (grid.GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.clear_sky ||
                    grid.GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.frost ||
                    grid.GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.fog ||
                    grid.GetChild(i).GetComponent<CardProperty>().effect == Global.Effect.rain)
                    cardList.Add(grid.GetChild(i));
            }

            int index = -1;
            index = HasWeather(Global.Effect.frost);
            if (index != -1 &&
                PowerController.GetInstance().enemy[0] + 10 < PowerController.GetInstance().player[0] &&
                !WeatherController.GetInstance().weather[0])
            {
                WeatherController.GetInstance().Frost();
                (cardList[index] as Transform).SetTarget(WeatherController.GetInstance().grid);
                base.Play();
                return;
            }//霜冻

            index = HasWeather(Global.Effect.fog);
            if (index != -1 &&
                PowerController.GetInstance().enemy[1] + 10 < PowerController.GetInstance().player[1] &&
                !WeatherController.GetInstance().weather[1])
            {
                WeatherController.GetInstance().Fog();
                (cardList[index] as Transform).SetTarget(WeatherController.GetInstance().grid);
                base.Play();
                return;
            }//雾天

            index = HasWeather(Global.Effect.rain);
            if (index != -1 &&
                  PowerController.GetInstance().enemy[2] + 10 < PowerController.GetInstance().player[2] &&
              !WeatherController.GetInstance().weather[2])
            {
                WeatherController.GetInstance().Rain();
                (cardList[index] as Transform).SetTarget(WeatherController.GetInstance().grid);
                base.Play();
                return;
            }//地形雨

            int playerPower = 0;
            int enemyPower = 0;
            for (int i = 2; i < 5; i++)
            {
                for (int ii = 0; ii < PlayerController.GetInstance().grids[i].childCount; ii++)
                    playerPower += PlayerController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().power;
                for (int ii = 0; ii < EnemyController.GetInstance().grids[i].childCount; ii++)
                    enemyPower += EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().power;
            }
            index = HasWeather(Global.Effect.clear_sky);
            if (index != -1 &&
                enemyPower > playerPower && (
                WeatherController.GetInstance().weather[0] ||
                WeatherController.GetInstance().weather[1] ||
                WeatherController.GetInstance().weather[2]))
            {
                WeatherController.GetInstance().ClearSky();
                (cardList[index] as Transform).SetTarget(EnemyController.GetInstance().grids[5]);
                base.Play();
                return;
            }//晴天
        }

        public sealed override bool IsEnabled
        {
            get
            {
                return (PowerController.GetInstance().enemy[0] + 10 < PowerController.GetInstance().player[0] &&
                    !WeatherController.GetInstance().weather[0]) ||
                    (PowerController.GetInstance().enemy[1] + 10 < PowerController.GetInstance().player[1] &&
                    !WeatherController.GetInstance().weather[1]) ||
                    (PowerController.GetInstance().enemy[2] + 10 < PowerController.GetInstance().player[2] &&
                    !WeatherController.GetInstance().weather[2]) &&
                    isEnabled;
            }
        }

        int HasWeather(Global.Effect weather)
        {
            for (int i = 0; i < cardList.Count; i++)
                if ((cardList[i] as Transform).GetComponent<CardProperty>().effect == weather)
                    return i;

            return -1;
        }
    }
}
