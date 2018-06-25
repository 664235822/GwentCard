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

            int playerPower = 0;
            int enemyPower = 0;
            for (int i = 2; i < 5; i++)
            {
                for (int ii = 0; ii < PlayerController.GetInstance().grids[i].childCount; ii++)
                    playerPower += PlayerController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().power;
                for (int ii = 0; ii < EnemyController.GetInstance().grids[i].childCount; ii++)
                    enemyPower += EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardProperty>().power;
            }
            if (HasCard(Global.Effect.clear_sky, out index) &&
                enemyPower > playerPower && (
                WeatherController.GetInstance().weather[0] ||
                WeatherController.GetInstance().weather[1] ||
                WeatherController.GetInstance().weather[2]))
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
                PowerController.GetInstance().enemy_total - 5 <= PowerController.GetInstance().player_total &&
                !IsWeather(cardList[index] as Transform))
                return index;//召集

            if (HasCard(Global.Effect.same_type_morale, out index) &&
                !IsWeather(cardList[index] as Transform))
                return index;//士气增长

            if (HasCard(Global.Effect.nurse, out index) &&
                EnemyController.GetInstance().grids[5].childCount > 3 &&
                !IsWeather(cardList[index] as Transform))
                return index;//护士

            if (PowerController.GetInstance().enemy_total >= PowerController.GetInstance().player_total)
            {
                int minPowerIndex = 0;
                int minPower = 15;
                for (int i = 0; i < cardList.Count; i++)
                    if ((cardList[i] as Transform).GetComponent<CardProperty>().power < minPower &&
                        (cardList[i] as Transform).GetComponent<CardProperty>().power > 0 &&
                        !IsWeather(cardList[i] as Transform))
                    {
                        minPower = (cardList[i] as Transform).GetComponent<CardProperty>().power;
                        minPowerIndex = i;
                    }
                return minPowerIndex;
            }
            else if(PowerController.GetInstance().enemy_total < PowerController.GetInstance().player_total)
            {
                int maxPowerIndex = 0;
                int maxPower = 0;
                for (int i = 0; i < cardList.Count; i++)
                    if ((cardList[i] as Transform).GetComponent<CardProperty>().power > maxPower &&
                        !IsWeather(cardList[i] as Transform))
                    {
                        maxPower = (cardList[i] as Transform).GetComponent<CardProperty>().power;
                        maxPowerIndex = i;
                    }
                return maxPowerIndex;
            }

            return Random.Range(0, grid.childCount);
        }

        public bool AITurn()
        {
            int playerHandCount = PlayerController.GetInstance().grids[1].childCount;
            int enemyHandCount = EnemyController.GetInstance().grids[1].childCount;
            if (enemyHandCount == 0)
                return true;

            if (TurnController.GetInstance().turnIndex == 0)
                if (TurnController.GetInstance().isTurned[0] && (
                    playerHandCount - enemyHandCount > 2 ||
                    PowerController.GetInstance().enemy_total > PowerController.GetInstance().player_total ||
                    PowerController.GetInstance().player_total - PowerController.GetInstance().enemy_total >= 15))
                    return true;
                else if (PowerController.GetInstance().enemy_total - PowerController.GetInstance().player_total >= 15)
                    return true;
            if (TurnController.GetInstance().turnIndex == 1)
                if ((int)GameOver.GetInstance().playerPowerList[0] > (int)GameOver.GetInstance().enemyPowerList[0] &&
                    PowerController.GetInstance().enemy_total > PowerController.GetInstance().player_total)
                    return true;
                else if (TurnController.GetInstance().isTurned[0] && (
                    playerHandCount - enemyHandCount > 2 ||
                    PowerController.GetInstance().enemy_total > PowerController.GetInstance().player_total ||
                    PowerController.GetInstance().player_total - PowerController.GetInstance().enemy_total >= 15))
                    return true;
                else if (PowerController.GetInstance().enemy_total - PowerController.GetInstance().player_total >= 15)
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

        bool IsWeather(Transform card)
        {
            if ((card.GetComponent<CardProperty>().line == Global.Line.melee &&
                WeatherController.GetInstance().weather[0]) ||
                (card.GetComponent<CardProperty>().line == Global.Line.ranged &&
                WeatherController.GetInstance().weather[1]) ||
                (card.GetComponent<CardProperty>().line == Global.Line.siege &&
                WeatherController.GetInstance().weather[2]))
                return true;

            return false;
        }
    }
}