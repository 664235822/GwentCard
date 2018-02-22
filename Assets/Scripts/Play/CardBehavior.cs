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
                        Constants.SetParent(PlayerController.instance.grids[1], EnemyController.instance.grids[2], index);
                        break;
                    case Constants.Line.ranged:
                        Constants.SetParent(PlayerController.instance.grids[1], EnemyController.instance.grids[3], index);
                        break;
                    case Constants.Line.siege:
                        Constants.SetParent(PlayerController.instance.grids[1], EnemyController.instance.grids[4], index);
                        break;
                }
                PlayerController.instance.DrawCards(2);
                break;
            case Constants.Effect.clear_sky:
                Constants.SetParent(PlayerController.instance.grids[1], GameController.instance.grids[0], index);
                WeatherController.instance.ClearSky();
                break;
            case Constants.Effect.frost:
                if(!WeatherController.instance.frost)
                {
                    Constants.SetParent(PlayerController.instance.grids[1], WeatherController.instance.grid, index);
                    WeatherController.instance.Frost();
                }
                break;
            case Constants.Effect.fog:
                if(!WeatherController.instance.fog)
                {
                    Constants.SetParent(PlayerController.instance.grids[1], WeatherController.instance.grid, index);
                    WeatherController.instance.Fog();
                }
                break;
            case Constants.Effect.rain:
                if(!WeatherController.instance.rain)
                {
                    Constants.SetParent(PlayerController.instance.grids[1], WeatherController.instance.grid, index);
                    WeatherController.instance.Rain();
                }
                break;
            default:
                switch (cardProperty.line)
                {
                    case Constants.Line.melee:
                        Constants.SetParent(PlayerController.instance.grids[1], PlayerController.instance.grids[2], index);
                        break;
                    case Constants.Line.ranged:
                        Constants.SetParent(PlayerController.instance.grids[1], PlayerController.instance.grids[3], index);
                        break;
                    case Constants.Line.siege:
                        Constants.SetParent(PlayerController.instance.grids[1], PlayerController.instance.grids[4], index);
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
