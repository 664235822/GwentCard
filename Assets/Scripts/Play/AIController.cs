using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Play
{
    public class AIController : Singleton<AIController>
    {
        ArrayList cardList = new ArrayList();

        public int AICard(Transform grid)
        {
            cardList.Clear();
            for (int i = 0; i < grid.childCount; i++)
                cardList.Add(grid.GetChild(i));

            int index = -1;

            if (HasCard(Global.Effect.spy, out index))
                return index;//间谍
            if (HasCard(Global.Effect.frost, out index) &&
                PowerController.GetInstance().enemy[0] + 10 < PowerController.GetInstance().player[0] &&
                !WeatherController.GetInstance().weather[0])
                return index;//霜冻
            if (HasCard(Global.Effect.fog, out index) &&
                PowerController.GetInstance().enemy[1] + 10 < PowerController.GetInstance().player[1] &&
                !WeatherController.GetInstance().weather[1])
                return index;//雾天
            if (HasCard(Global.Effect.rain, out index) &&
                PowerController.GetInstance().enemy[2] + 10 < PowerController.GetInstance().player[2] &&
                !WeatherController.GetInstance().weather[2])
                return index;//地形雨

            int[] playerPower = { 0, 0, 0 };
            int[] enemyPower = { 0, 0, 0 };
            for (int i = 2; i < 5; i++)
            {
                for (int ii = 0; ii < PlayerController.GetInstance().grids[i].childCount; ii++)
                    playerPower[i - 2] += PlayerController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().power;
                for (int ii = 0; ii < EnemyController.GetInstance().grids[i].childCount; ii++)
                    enemyPower[i - 2] += EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().power;
            }
            if (HasCard(Global.Effect.clear_sky, out index) && (
                (WeatherController.GetInstance().weather[0] && enemyPower[0] > playerPower[0]) ||
                (WeatherController.GetInstance().weather[1] && enemyPower[1] > playerPower[1]) ||
                (WeatherController.GetInstance().weather[2] && enemyPower[2] > playerPower[2])))
                return index;//晴天

            if (HasCard(Global.Effect.warhorn, out index) && (
                EnemyController.GetInstance().grids[2].childCount > 3 ||
                EnemyController.GetInstance().grids[3].childCount > 3 ||
                EnemyController.GetInstance().grids[4].childCount > 3))
                return index;//战斗号角

            bool isDummy = false;
            for (int i = 2; i < 5; i++)
                for (int ii = 0; ii < EnemyController.GetInstance().grids[i].childCount; ii++)
                    if (EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().effect == Global.Effect.spy ||
                        EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().effect == Global.Effect.nurse ||
                        EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().effect == Global.Effect.scorch ||
                        EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().effect == Global.Effect.warhorn)
                        isDummy = true;
            if (HasCard(Global.Effect.dummy, out index) && isDummy)
                return index;//稻草人

            int playerMaxPower = 0;
            int enemyMaxPower = 0;
            for (int i = 2; i < 5; i++)
            {
                for (int ii = 0; ii < PlayerController.GetInstance().grids[i].childCount; ii++)
                {
                    if (!PlayerController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().gold)
                    {
                        int power = PlayerController.GetInstance().grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                        if (power > playerMaxPower) playerMaxPower = power;
                    }
                }
                for (int ii = 0; ii < EnemyController.GetInstance().grids[i].childCount; ii++)
                {
                    if (!EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().gold)
                    {
                        int power = EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                        if (power > enemyMaxPower) enemyMaxPower = power;
                    }
                }
            }
            if (HasCard(Global.Effect.scorch, out index) &&
                playerMaxPower >= 10 &&
                playerMaxPower > enemyMaxPower)
                return index;//毒药

            if (HasCard(Global.Effect.muster, out index) &&
                PowerController.GetInstance().enemy_total - 5 <= PowerController.GetInstance().player_total)
                return index;//召集

            if (HasCard(Global.Effect.same_type_morale, out index))
                return index;//士气增长

            if (HasCard(Global.Effect.nurse, out index) &&
                EnemyController.GetInstance().grids[5].childCount > 3)
                return index;//护士

            int maxPowerIndex = 0;
            int minPowerIndex = 0;
            int maxPower = 0;
            int minPower = 15;
            for (int i = 0; i < cardList.Count; i++)
                if ((cardList[i] as Transform).GetComponent<CardProperty>().power > maxPower)
                {
                    maxPower = (cardList[i] as Transform).GetComponent<CardProperty>().power;
                    maxPowerIndex = i;
                }
            for (int i = 0; i < cardList.Count; i++)
                if ((cardList[i] as Transform).GetComponent<CardProperty>().power < minPower &&
                (cardList[i] as Transform).GetComponent<CardProperty>().power > 0)
                {
                    minPower = (cardList[i] as Transform).GetComponent<CardProperty>().power;
                    minPowerIndex = i;
                }
            if (PowerController.GetInstance().enemy_total >= PowerController.GetInstance().player_total)
                return maxPowerIndex;
            else
                return minPowerIndex;
        }

        public bool AITurn()
        {
            int playerHandNum = PlayerController.GetInstance().grids[1].childCount;
            int enemyHandNum = EnemyController.GetInstance().grids[1].childCount;
            if (enemyHandNum == 0)
                return true;

            if (TurnController.GetInstance().turnIndex == 0)
                if (TurnController.GetInstance().isTurned[0] && (
                    playerHandNum - enemyHandNum > 2 ||
                    PowerController.GetInstance().enemy_total > PowerController.GetInstance().player_total))
                    return true;
                else if (PowerController.GetInstance().enemy_total - PowerController.GetInstance().player_total > 25)
                    return true;
            if (TurnController.GetInstance().turnIndex == 1)
                if ((int)GameOver.GetInstance().playerPowerList[0] > (int)GameOver.GetInstance().enemyPowerList[0] &&
                    PowerController.GetInstance().enemy_total > PowerController.GetInstance().player_total)
                    return true;
                else if (TurnController.GetInstance().isTurned[0] && (
                        playerHandNum - enemyHandNum > 2 ||
                        PowerController.GetInstance().enemy_total > PowerController.GetInstance().player_total))
                    return true;
                else if (PowerController.GetInstance().enemy_total - PowerController.GetInstance().player_total > 25)
                    return true;
            if (TurnController.GetInstance().turnIndex == 2)
                if (PowerController.GetInstance().enemy_total > PowerController.GetInstance().player_total)
                    return true;

            return false;
        }

        bool HasCard(Global.Effect effect, out int index)
        {
            for (int i = 0; i < cardList.Count; i++)
                if ((cardList[i] as Transform).GetComponent<CardProperty>().effect == effect)
                {
                    index = i;
                    return true;
                }

            index = -1;
            return false;
        }
    }
}