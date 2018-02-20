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
                        PlayerController.instance.grids[1].GetChild(index).SetParent(EnemyController.instance.grids[2]);
                        EnemyController.instance.grids[2].GetComponent<UIGrid>().Reposition();
                        break;
                    case Constants.Line.ranged:
                        PlayerController.instance.grids[1].GetChild(index).SetParent(EnemyController.instance.grids[3]);
                        EnemyController.instance.grids[3].GetComponent<UIGrid>().Reposition();
                        break;
                    case Constants.Line.siege:
                        PlayerController.instance.grids[1].GetChild(index).SetParent(EnemyController.instance.grids[4]);
                        EnemyController.instance.grids[4].GetComponent<UIGrid>().Reposition();
                        break;
                }
                PlayerController.instance.DrawCards(2);
                break;
            case Constants.Effect.clear_sky:
                Destroy(PlayerController.instance.grids[1].GetChild(index).gameObject);
                WeatherController.instance.ClearSky();
                break;
            case Constants.Effect.frost:
                PlayerController.instance.grids[1].GetChild(index).SetParent(WeatherController.instance.grid);
                WeatherController.instance.Frost();
                break;
            case Constants.Effect.fog:
                PlayerController.instance.grids[1].GetChild(index).SetParent(WeatherController.instance.grid);
                WeatherController.instance.Fog();
                break;
            case Constants.Effect.rain:
                PlayerController.instance.grids[1].GetChild(index).SetParent(WeatherController.instance.grid);
                WeatherController.instance.Rain();
                break;
            default:
                switch (cardProperty.line)
                {
                    case Constants.Line.melee:
                        PlayerController.instance.grids[1].GetChild(index).SetParent(PlayerController.instance.grids[2]);
                        PlayerController.instance.grids[2].GetComponent<UIGrid>().Reposition();
                        break;
                    case Constants.Line.ranged:
                        PlayerController.instance.grids[1].GetChild(index).SetParent(PlayerController.instance.grids[3]);
                        PlayerController.instance.grids[3].GetComponent<UIGrid>().Reposition();
                        break;
                    case Constants.Line.siege:
                        PlayerController.instance.grids[1].GetChild(index).SetParent(PlayerController.instance.grids[4]);
                        PlayerController.instance.grids[4].GetComponent<UIGrid>().Reposition();
                        break;
                }
                break;
        }

        PlayerController.instance.grids[1].GetComponent<UIGrid>().Reposition();
        ShowCards.instance.Hide();
        PlayerController.instance.Number();
        PowerNumberController.instance.Number();
        EnemyController.instance.Play();
    }
}
