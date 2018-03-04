using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerController : MonoBehaviour {
    public static PowerController instance;
    [SerializeField] UILabel[] player_power_label;
    [SerializeField] UILabel[] enemy_power_label;
    [HideInInspector] public int player_total = 0;
    [HideInInspector] public int enemy_total = 0;
    
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
            Transform card = PlayerController.instance.grids[2].GetChild(i);
            CardProperty cardProperty = card.GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.frost && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = card.GetComponent<UISprite>().spriteName;
            if (player_same_type_morale.ContainsKey(spriteName))
                power *= player_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += player_improve_neighbours[0];
            card.GetComponent<CardBehavior>().totalPower = power;
            player[0] += power;
        }
        for (int i = 0; i < PlayerController.instance.grids[3].childCount; i++)
        {
            Transform card = PlayerController.instance.grids[3].GetChild(i);
            CardProperty cardProperty = card.GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.fog && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = card.GetComponent<UISprite>().spriteName;
            if (player_same_type_morale.ContainsKey(spriteName))
                power *= player_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += player_improve_neighbours[1];
            card.GetComponent<CardBehavior>().totalPower = power;
            player[1] += power;
        }
        for (int i = 0; i < PlayerController.instance.grids[4].childCount; i++)
        {
            Transform card = PlayerController.instance.grids[4].GetChild(i);
            CardProperty cardProperty = card.GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.rain && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = card.GetComponent<UISprite>().spriteName;
            if (player_same_type_morale.ContainsKey(spriteName))
                power *= player_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += player_improve_neighbours[2];
            card.GetComponent<CardBehavior>().totalPower = power;
            player[2] += power;
        }
        for (int i = 0; i < EnemyController.instance.grids[2].childCount; i++)
        {
            Transform card = EnemyController.instance.grids[2].GetChild(i);
            CardProperty cardProperty = card.GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.frost && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = card.GetComponent<UISprite>().spriteName;
            if (enemy_same_type_morale.ContainsKey(spriteName))
                power *= enemy_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += enemy_improve_neighbours[0];
            card.GetComponent<CardBehavior>().totalPower = power;
            enemy[0] += power;
        }
        for (int i = 0; i < EnemyController.instance.grids[3].childCount; i++)
        {
            Transform card = EnemyController.instance.grids[3].GetChild(i);
            CardProperty cardProperty = card.GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.frost && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = card.GetComponent<UISprite>().spriteName;
            if (enemy_same_type_morale.ContainsKey(spriteName))
                power *= enemy_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += enemy_improve_neighbours[1];
            card.GetComponent<CardBehavior>().totalPower = power;
            enemy[1] += power;
        }
        for (int i = 0; i < EnemyController.instance.grids[4].childCount; i++)
        {
            Transform card = EnemyController.instance.grids[4].GetChild(i);
            CardProperty cardProperty = card.GetComponent<CardProperty>();
            int power = 0;
            if (WeatherController.instance.frost && !cardProperty.gold)
                power += 1;
            else
                power += cardProperty.power;
            string spriteName = card.GetComponent<UISprite>().spriteName;
            if (enemy_same_type_morale.ContainsKey(spriteName))
                power *= enemy_same_type_morale[spriteName];
            if (cardProperty.effect != Constants.Effect.improve_neighbours)
                power += enemy_improve_neighbours[2];
            card.GetComponent<CardBehavior>().totalPower = power;
            enemy[2] += power;
        }

        for (int i = 0; i < 3; i++)
        {
            player_power_label[i].text = player[i].ToString();
            enemy_power_label[i].text = enemy[i].ToString();
        }
        player_total = player[0] + player[1] + player[2];
        enemy_total = enemy[0] + enemy[1] + enemy[2];
        player_power_label[3].text = player_total.ToString();
        enemy_power_label[3].text = enemy_total.ToString();
    }
}
