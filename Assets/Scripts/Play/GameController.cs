using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
    public UIAtlas[] atlas;
    public GameObject cardPerfab;
    [SerializeField] TweenAlpha[] player_life_gem;
    [SerializeField] TweenAlpha[] enemy_life_gem;
    bool offensive;
    int player_fail = 0;
    int enemy_fail = 0;

    public void StartGame(string playerGroup)
    {
        PlayerController.GetInstance().Initialize(playerGroup);
        EnemyController.GetInstance().Initialize();

        int random = Random.Range(0, 2);
        if (random == 0) offensive = true;
        else offensive = false;

        if (!offensive) EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);
    }

    public void EndTurn()
    {
        int power = PowerController.GetInstance().player_total - PowerController.GetInstance().enemy_total;
        if (power > 0)
        {
            enemy_life_gem[enemy_fail].PlayForward();
            enemy_fail++;
        }
        else if (power < 0)
        {
            player_life_gem[player_fail].PlayForward();
            player_fail++;
        }
        else if (power == 0)
        {
            player_life_gem[player_fail].PlayForward();
            enemy_life_gem[enemy_fail].PlayForward();
            player_fail++;
            enemy_fail++;
        }

        WeatherController.GetInstance().ClearSky();

        for (int i = 2; i < 5; i++)
        {
            for (int ii = PlayerController.GetInstance().grids[i].childCount - 1; ii >= 0; ii--)
            {
                PlayerController.GetInstance().grids[i].SetParent(ii, PlayerController.GetInstance().grids[5]);
            }
        }
        for (int i = 2; i < 5; i++)
        {
            for (int ii = EnemyController.GetInstance().grids[i].childCount - 1; ii >= 0; ii--)
            {
                EnemyController.GetInstance().grids[i].SetParent(ii, EnemyController.GetInstance().grids[5]);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (WarhornController.GetInstance().playerWarhorn[i])
            {
                WarhornController.GetInstance().playerGrids[i].SetParent(0, PlayerController.GetInstance().grids[5]);
                WarhornController.GetInstance().playerWarhorn[i] = false;
            }
            if (WarhornController.GetInstance().enemyWarhorn[i])
            {
                WarhornController.GetInstance().enemyGrids[i].SetParent(0, EnemyController.GetInstance().grids[5]);
                WarhornController.GetInstance().enemyWarhorn[i] = false;
            }
        }

        PlayerController.GetInstance().grids[5].gameObject.SetActive(false);
        PlayerController.GetInstance().grids[5].gameObject.SetActive(true);
        EnemyController.GetInstance().grids[5].gameObject.SetActive(false);
        EnemyController.GetInstance().grids[5].gameObject.SetActive(true);

        PowerController.GetInstance().Number();

        offensive = !offensive;
        if (!offensive) EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);
    }
}
