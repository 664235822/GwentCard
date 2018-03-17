using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public UIAtlas[] atlas;
    public GameObject cardPerfab;
    [SerializeField] UISprite[] player_life_gem;
    [SerializeField] UISprite[] enemy_life_gem;
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

        if (!offensive) EnemyController.instance.Play(EnemyController.instance.grids[1]);
    }

    public void EndTurn()
    {
        int power = PowerController.instance.player_total - PowerController.instance.enemy_total;
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

        WeatherController.instance.ClearSky();

        for (int i = 2; i < 5; i++)
        {
            for (int ii = PlayerController.instance.grids[i].childCount - 1; ii >= 0; ii--)
            {
                PlayerController.instance.grids[i].SetParent(ii, PlayerController.instance.grids[5]);
            }
        }
        for (int i = 2; i < 5; i++)
        {
            for (int ii = EnemyController.instance.grids[i].childCount - 1; ii >= 0; ii--)
            {
                EnemyController.instance.grids[i].SetParent(ii, EnemyController.instance.grids[5]);
            }
        }

        PlayerController.instance.grids[5].gameObject.SetActive(false);
        PlayerController.instance.grids[5].gameObject.SetActive(true);
        EnemyController.instance.grids[5].gameObject.SetActive(false);
        EnemyController.instance.grids[5].gameObject.SetActive(true);

        PowerController.instance.Number();

        offensive = !offensive;
        if (!offensive) EnemyController.instance.Play(EnemyController.instance.grids[1]);
    }
}
