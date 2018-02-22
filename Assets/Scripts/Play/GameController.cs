using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public UIAtlas[] atlas;
    public UISprite[] player_life_gem;
    public UISprite[] enemy_life_gem;
    public Transform[] grids;
    bool offensive;
    int player_fail = 0;
    int enemy_fail = 0;
    readonly Color black = new Color(0.112f, 0.255f, 0.255f, 0.255f);

    private void Awake()
    {
        instance = this;
    }

    public void StartGame(string PlayerGroup)
    {
        PlayerController.instance.Initialize(PlayerGroup);
        EnemyController.instance.Initialize();

        int random = Random.Range(0, 2);
        if (random == 0) offensive = true;
        else offensive = false;

        if (!offensive) EnemyController.instance.Play();
    }

    public void EndTurn()
    {
        int power = PowerNumberController.instance.player_total - PowerNumberController.instance.enemy_total;
        if (power > 0)
        {
            enemy_life_gem[enemy_fail].color = black;
            enemy_fail++;
        }
        else if (power < 0)
        {
            player_life_gem[player_fail].color = black;
            player_fail++;
        }
        else if (power == 0)
        {
            player_life_gem[player_fail].color = black;
            enemy_life_gem[enemy_fail].color = black;
            player_fail++;
            enemy_fail++;
        }

        NewTurn();
    }

    void NewTurn()
    {
        WeatherController.instance.ClearSky();

        for (int i = 2; i < PlayerController.instance.grids.Length; i++)
            for (int ii = 0; i < PlayerController.instance.grids[i].childCount; i++)
                Constants.SetParent(PlayerController.instance.grids[i], grids[0], ii);
        
        for (int i = 2; i < EnemyController.instance.grids.Length; i++)
            for (int ii = 0; i < EnemyController.instance.grids[i].childCount; i++)
                Constants.SetParent(EnemyController.instance.grids[i], grids[1], ii);

        PowerNumberController.instance.Number();

        offensive = !offensive;
        if (!offensive) EnemyController.instance.Play();
    }
}
