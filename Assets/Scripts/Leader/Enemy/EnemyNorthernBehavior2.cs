using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Play;

namespace GwentCard.Leader
{
    public class EnemyNorthernBehavior2 : EnemyLeaderBehavior
    {
        public sealed override void Play()
        {
            WeatherController.GetInstance().ClearSky();
            base.Play();
        }

        public sealed override bool IsEnabled
        {
            get
            {
                int playerPower = 0;
                int enemyPower = 0;
                for (int i = 2; i < 5; i++)
                {
                    for (int ii = 0; ii < PlayerController.GetInstance().grids[i].childCount; ii++)
                        playerPower += PlayerController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().power;
                    for (int ii = 0; ii < EnemyController.GetInstance().grids[i].childCount; ii++)
                        enemyPower += EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().power;
                }
                return enemyPower > playerPower &&
                    (WeatherController.GetInstance().weather[0] ||
                    WeatherController.GetInstance().weather[1] ||
                    WeatherController.GetInstance().weather[2]) &&
                    isEnabled;
            }
        }
    }
}