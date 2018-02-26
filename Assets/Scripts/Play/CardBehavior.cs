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
        Transform grid = ShowCards.instance.totalGrid;
        int index = 0;
        for (int i = 0; i < grid.childCount; i++)
            if (ShowCards.instance.grid.GetChild(i).name == name) index = i;

        switch (cardProperty.effect)
        {
            case Constants.Effect.spy:
                switch (cardProperty.line)
                {
                    case Constants.Line.melee:
                        grid.SetParent(index, EnemyController.instance.grids[2]);
                        break;
                    case Constants.Line.ranged:
                        grid.SetParent(index, EnemyController.instance.grids[3]);
                        break;
                    case Constants.Line.siege:
                        grid.SetParent(index, EnemyController.instance.grids[4]);
                        break;
                }
                PlayerController.instance.DrawCards(2);
                break;
            case Constants.Effect.clear_sky:
                grid.SetParent(index, PlayerController.instance.grids[5]);
                WeatherController.instance.ClearSky();
                break;
            case Constants.Effect.frost:
                if(!WeatherController.instance.frost)
                {
                    grid.SetParent(index, WeatherController.instance.grid);
                    WeatherController.instance.Frost();
                }
                break;
            case Constants.Effect.fog:
                if(!WeatherController.instance.fog)
                {
                    grid.SetParent(index, WeatherController.instance.grid);
                    WeatherController.instance.Fog();
                }
                break;
            case Constants.Effect.rain:
                if(!WeatherController.instance.rain)
                {
                    grid.SetParent(index, WeatherController.instance.grid);
                    WeatherController.instance.Rain();
                }
                break;
            default:
                switch (cardProperty.line)
                {
                    case Constants.Line.melee:
                        grid.SetParent(index, PlayerController.instance.grids[2]);
                        break;
                    case Constants.Line.ranged:
                        grid.SetParent(index, PlayerController.instance.grids[3]);
                        break;
                    case Constants.Line.siege:
                        grid.SetParent(index, PlayerController.instance.grids[4]);
                        break;
                }
                break;
        }

        ShowCards.instance.Hide();
        if (cardProperty.effect == Constants.Effect.nurse) ShowCards.instance.Show(ShowCards.Behaviour.draw, PlayerController.instance.grids[5]);

        PlayerController.instance.Number();
        PowerNumberController.instance.Number();
        EnemyController.instance.Play(EnemyController.instance.grids[0]);
    }
}
