using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard.Play
{
    public class GameOver : Singleton<GameOver>
    {
        [SerializeField] GameObject obj;
        [SerializeField] UILabel label;
        [SerializeField] UILabel[] player_labels;
        [SerializeField] UILabel[] enemy_labels;
        [HideInInspector] public ArrayList playerPowerList = new ArrayList();
        [HideInInspector] public ArrayList enemyPowerList = new ArrayList();

        public IEnumerator Show(bool isWin)
        {
            BlackShow.GetInstance().Show(true);
            PlayerController.GetInstance().obj.SetActive(false);
            EnemyController.GetInstance().obj.SetActive(false);
            obj.SetActive(true);

            if (isWin)
                label.text = "游戏结束，你赢了！";
            else
                label.text = "游戏结束，你输了";

            for (int i = 0; i < playerPowerList.Count; i++)
                player_labels[i].text = playerPowerList[i].ToString();
            for (int i = 0; i < enemyPowerList.Count; i++)
                enemy_labels[i].text = enemyPowerList[i].ToString();

            CoroutineManager.GetInstance().Finish();
            yield return null;
        }

        public void AddPower(int playerPower, int enemyPower)
        {
            playerPowerList.Add(playerPower);
            enemyPowerList.Add(enemyPower);
        }
    }
}
