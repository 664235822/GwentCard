using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerNumberController : MonoBehaviour {
    public static PowerNumberController instance;
    [SerializeField] UILabel player_power_total;
    [SerializeField] UILabel player_power_melee;
    [SerializeField] UILabel player_power_ranged;
    [SerializeField] UILabel player_power_siege;
    [SerializeField] UILabel enemy_power_total;
    [SerializeField] UILabel enemy_power_melee;
    [SerializeField] UILabel enemy_power_ranged;
    [SerializeField] UILabel enemy_power_siege;
    [HideInInspector] public int player_total;
    [HideInInspector] public int enemy_total;

    private void Awake()
    {
        instance = this;
    }

    public void Number()
    {
        int player_melee = 0;
        int player_ranged = 0;
        int player_siege = 0;
        int enemy_melee = 0;
        int enemy_ranged = 0;
        int enemy_siege = 0;

        for (int i = 0; i < PlayerController.instance.grids[2].childCount; i++)
        {
            if (WeatherController.instance.frost && !PlayerController.instance.grids[2].GetChild(i).GetComponent<CardProperty>().gold)
                player_melee += 1;
            else
                player_melee += PlayerController.instance.grids[2].GetChild(i).GetComponent<CardProperty>().power;
        }
        for (int i = 0; i < PlayerController.instance.grids[3].childCount; i++)
        {
            if (WeatherController.instance.fog && !PlayerController.instance.grids[3].GetChild(i).GetComponent<CardProperty>().gold)
                player_ranged += 1;
            else
                player_ranged += PlayerController.instance.grids[3].GetChild(i).GetComponent<CardProperty>().power;
        }
        for (int i = 0; i < PlayerController.instance.grids[4].childCount; i++)
        {
            if (WeatherController.instance.rain && !PlayerController.instance.grids[4].GetChild(i).GetComponent<CardProperty>().gold)
                player_siege += 1;
            else
                player_siege += PlayerController.instance.grids[4].GetChild(i).GetComponent<CardProperty>().power;
        }
        for (int i = 0; i < EnemyController.instance.grids[2].childCount; i++)
        {
            if (WeatherController.instance.frost && !EnemyController.instance.grids[2].GetChild(i).GetComponent<CardProperty>().gold)
                enemy_melee += 1;
            else
                enemy_melee += EnemyController.instance.grids[2].GetChild(i).GetComponent<CardProperty>().power;
        }
        for (int i = 0; i < EnemyController.instance.grids[3].childCount; i++)
        {
            if (WeatherController.instance.fog && !EnemyController.instance.grids[3].GetChild(i).GetComponent<CardProperty>().gold)
                enemy_ranged += 1;
            else
                enemy_ranged += EnemyController.instance.grids[3].GetChild(i).GetComponent<CardProperty>().power;
        }
        for (int i = 0; i < EnemyController.instance.grids[4].childCount; i++)
        {
            if (WeatherController.instance.rain && !EnemyController.instance.grids[4].GetChild(i).GetComponent<CardProperty>().gold)
                enemy_siege += 1;
            else
                enemy_siege += EnemyController.instance.grids[4].GetChild(i).GetComponent<CardProperty>().power;
        }

        player_total = player_melee + player_ranged + player_siege;
        enemy_total = enemy_melee + enemy_ranged + enemy_siege;

        player_power_melee.text = player_melee.ToString();
        player_power_ranged.text = player_ranged.ToString();
        player_power_siege.text = player_siege.ToString();
        enemy_power_melee.text = enemy_melee.ToString();
        enemy_power_ranged.text = enemy_ranged.ToString();
        enemy_power_siege.text = enemy_siege.ToString();
        player_power_total.text = player_total.ToString();
        enemy_power_total.text = enemy_total.ToString();
    }
}
