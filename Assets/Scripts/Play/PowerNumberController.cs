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
        int[] player = { 0, 0, 0 };
        int[] enemy = { 0, 0, 0 };
        int[] player_improve_neighbours = { 0, 0, 0 };
        int[] enemy_improve_neighbours = { 0, 0, 0 };
        Dictionary<string, int> player_same_type_morale = new Dictionary<string, int>();
        Dictionary<string, int> enemy_same_type_morale = new Dictionary<string, int>();

        for (int i = 2; i < 5; i++)
        {
            for (int ii = 0; ii < PlayerController.instance.grids[i].childCount; ii++)
            {
                CardProperty cardProperty = PlayerController.instance.grids[i].GetChild(ii).GetComponent<CardProperty>();
                switch (cardProperty.effect)
                {
                    case Constants.Effect.improve_neighbours:
                        player_improve_neighbours[i - 2]++;
                        break;
                    case Constants.Effect.same_type_morale:
                        string spriteName = cardProperty.GetComponent<UISprite>().spriteName;
                        if (!player_same_type_morale.ContainsKey(spriteName))
                            player_same_type_morale.Add(spriteName, 1);
                        else
                        {
                            int value = player_same_type_morale[spriteName];
                            value++;
                            player_same_type_morale[spriteName] = value;
                        }
                        break;
                }
            }
        }
        for (int i = 2; i < 5; i++)
        {
            for (int ii = 0; ii < EnemyController.instance.grids[i].childCount; ii++)
            {
                CardProperty cardProperty = EnemyController.instance.grids[i].GetChild(ii).GetComponent<CardProperty>();
                switch (cardProperty.effect)
                {
                    case Constants.Effect.improve_neighbours:
                        enemy_improve_neighbours[i - 2]++;
                        break;
                    case Constants.Effect.same_type_morale:
                        string spriteName = cardProperty.GetComponent<UISprite>().spriteName;
                        if (!enemy_same_type_morale.ContainsKey(spriteName))
                            enemy_same_type_morale.Add(spriteName, 1);
                        else
                        {
                            int value = enemy_same_type_morale[spriteName];
                            value++;
                            enemy_same_type_morale[spriteName] = value;
                        }
                        break;
                }
            }
        }

        for (int i = 0; i < PlayerController.instance.grids[2].childCount; i++)
        {
            CardProperty cardProperty = PlayerController.instance.grids[2].GetChild(i).GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.frost && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = cardProperty.GetComponent<UISprite>().spriteName;
            if (player_same_type_morale.ContainsKey(spriteName))
                power *= player_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += player_improve_neighbours[0];
            player[0] += power;

        }
        for (int i = 0; i < PlayerController.instance.grids[3].childCount; i++)
        {
            CardProperty cardProperty = PlayerController.instance.grids[3].GetChild(i).GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.fog && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = cardProperty.GetComponent<UISprite>().spriteName;
            if (player_same_type_morale.ContainsKey(spriteName))
                power *= player_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += player_improve_neighbours[1];
            player[1] += power;
        }
        for (int i = 0; i < PlayerController.instance.grids[4].childCount; i++)
        {
            CardProperty cardProperty = PlayerController.instance.grids[4].GetChild(i).GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.rain && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = cardProperty.GetComponent<UISprite>().spriteName;
            if (player_same_type_morale.ContainsKey(spriteName))
                power *= player_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += player_improve_neighbours[2];
            player[2] += power;
        }
        for (int i = 0; i < EnemyController.instance.grids[2].childCount; i++)
        {
            CardProperty cardProperty = EnemyController.instance.grids[2].GetChild(i).GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.frost && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = cardProperty.GetComponent<UISprite>().spriteName;
            if (enemy_same_type_morale.ContainsKey(spriteName))
                power *= enemy_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += enemy_improve_neighbours[0];
            enemy[0] += power;
        }
        for (int i = 0; i < EnemyController.instance.grids[3].childCount; i++)
        {
            CardProperty cardProperty = EnemyController.instance.grids[3].GetChild(i).GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.fog && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = cardProperty.GetComponent<UISprite>().spriteName;
            if (enemy_same_type_morale.ContainsKey(spriteName))
                power *= enemy_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += enemy_improve_neighbours[1];
            enemy[1] += power;
        }
        for (int i = 0; i < EnemyController.instance.grids[4].childCount; i++)
        {
            CardProperty cardProperty = EnemyController.instance.grids[4].GetChild(i).GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.rain && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = cardProperty.GetComponent<UISprite>().spriteName;
            if (enemy_same_type_morale.ContainsKey(spriteName))
                power *= enemy_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += enemy_improve_neighbours[2];
            enemy[2] += power;
        }

        player_total = player[0] + player[1] + player[2];
        enemy_total = enemy[0] + enemy[1] + enemy[2];

        player_power_melee.text = player[0].ToString();
        player_power_ranged.text = player[1].ToString();
        player_power_siege.text = player[2].ToString();
        enemy_power_melee.text = enemy[0].ToString();
        enemy_power_ranged.text = enemy[1].ToString();
        enemy_power_siege.text = enemy[2].ToString();
        player_power_total.text = player_total.ToString();
        enemy_power_total.text = enemy_total.ToString();
    }
}
