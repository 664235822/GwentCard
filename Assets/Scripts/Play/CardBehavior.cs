using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavior : MonoBehaviour {
    CardProperty cardProperty;

    private void Awake()
    {
        cardProperty = GetComponent<CardProperty>();
    }

    public void Play()
    {
        int index = 0;
        for (int i = 0; i < PlayerController.instance.grids[1].childCount; i++)
            if (ShowCards.instance.grid.GetChild(i).name == name) index = i;

        switch (cardProperty.effect)
        {
            case Constants.Effect.spy:
                switch (cardProperty.line)
                {
                    case Constants.Line.melee:
                        PlayerController.instance.grids[1].SetParent(index, EnemyController.instance.grids[2]);
                        break;
                    case Constants.Line.ranged:
                        PlayerController.instance.grids[1].SetParent(index, EnemyController.instance.grids[3]);
                        break;
                    case Constants.Line.siege:
                        PlayerController.instance.grids[1].SetParent(index, EnemyController.instance.grids[4]);
                        break;
                }
                PlayerController.instance.DrawCards(2);
                break;
            case Constants.Effect.clear_sky:
                PlayerController.instance.grids[1].SetParent(index, PlayerController.instance.grids[5]);
                WeatherController.instance.ClearSky();
                break;
            case Constants.Effect.frost:
                if(!WeatherController.instance.frost)
                {
                    PlayerController.instance.grids[1].SetParent(index, WeatherController.instance.grid);
                    WeatherController.instance.Frost();
                }
                break;
            case Constants.Effect.fog:
                if(!WeatherController.instance.fog)
                {
                    PlayerController.instance.grids[1].SetParent(index, WeatherController.instance.grid);
                    WeatherController.instance.Fog();
                }
                break;
            case Constants.Effect.rain:
                if(!WeatherController.instance.rain)
                {
                    PlayerController.instance.grids[1].SetParent(index, WeatherController.instance.grid);
                    WeatherController.instance.Rain();
                }
                break;
            default:
                switch (cardProperty.line)
                {
                    case Constants.Line.melee:
                        PlayerController.instance.grids[1].SetParent(index, PlayerController.instance.grids[2]);
                        break;
                    case Constants.Line.ranged:
                        PlayerController.instance.grids[1].SetParent(index, PlayerController.instance.grids[3]);
                        break;
                    case Constants.Line.siege:
                        PlayerController.instance.grids[1].SetParent(index, PlayerController.instance.grids[4]);
                        break;
                }
                break;
        }

        ShowCards.instance.Hide();
        PlayerController.instance.Number();
        PowerNumberController.instance.Number();
        EnemyController.instance.Play();
    }
}
