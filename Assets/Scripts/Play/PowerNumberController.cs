using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerNumberController : MonoBehaviour {
    public static PowerNumberController instance;
    public UILabel player_power_total;
    public UILabel player_power_melee;
    public UILabel player_power_ranged;
    public UILabel player_power_siege;
    public UILabel enemy_power_total;
    public UILabel enemy_power_melee;
    public UILabel enemy_power_ranged;
    public UILabel enemy_power_siege;

    private void Awake()
    {
        instance = this;
    }

    public void Number()
    {
        int player_melee = 0;
        for(int i=0;i< PlayerController.instance.grids[2].childCount;i++)
            player_melee += PlayerController.instance.grids[2].GetChild(i).GetComponent<CardProperty>().power;
        player_power_melee.text = player_melee.ToString();

        int player_ranged = 0;
        for (int i = 0; i < PlayerController.instance.grids[3].childCount; i++)
            player_ranged += PlayerController.instance.grids[3].GetChild(i).GetComponent<CardProperty>().power;
        player_power_ranged.text = player_ranged.ToString();

        int player_siege = 0;
        for (int i = 0; i < PlayerController.instance.grids[4].childCount; i++)
            player_siege += PlayerController.instance.grids[4].GetChild(i).GetComponent<CardProperty>().power;
        player_power_siege.text = player_siege.ToString();

        player_power_total.text = (player_melee + player_ranged + player_siege).ToString();

        int enemy_melee = 0;
        for (int i = 0; i < EnemyController.instance.grids[0].childCount; i++)
            enemy_melee += EnemyController.instance.grids[0].GetChild(i).GetComponent<CardProperty>().power;
        enemy_power_melee.text = enemy_melee.ToString();

        int enemy_ranged = 0;
        for (int i = 0; i < EnemyController.instance.grids[1].childCount; i++)
            enemy_ranged += EnemyController.instance.grids[1].GetChild(i).GetComponent<CardProperty>().power;
        enemy_power_ranged.text = enemy_ranged.ToString();

        int enemy_siege = 0;
        for (int i = 0; i < EnemyController.instance.grids[2].childCount; i++)
            enemy_siege += EnemyController.instance.grids[2].GetChild(i).GetComponent<CardProperty>().power;
        enemy_power_siege.text = enemy_siege.ToString();

        enemy_power_total.text = (enemy_melee + enemy_ranged + enemy_siege).ToString();
    }

}
