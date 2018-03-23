using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavior : MonoBehaviour {
    public static int index = 0;
    [HideInInspector] public int totalPower;
    CardProperty cardProperty;

    private void Awake()
    {
        cardProperty = GetComponent<CardProperty>();
    }

    public void Play()
    {
        Transform grid = ShowCards.GetInstance().totalGrid;
        for (int i = 0; i < grid.childCount; i++)
            if (ShowCards.GetInstance().grid.GetChild(i).name == name) index = i;

        switch (cardProperty.effect)
        {
            case Global.Effect.spy:
                grid.SetParent(index, EnemyController.GetInstance().grids[(int)cardProperty.line + 2]);
                PlayerController.GetInstance().DrawCards(2);
                break;
            case Global.Effect.clear_sky:
                WeatherController.GetInstance().ClearSky();
                goto default;
            case Global.Effect.frost:
                if (!WeatherController.GetInstance().frost) WeatherController.GetInstance().Frost();
                goto default;
            case Global.Effect.fog:
                if (!WeatherController.GetInstance().fog) WeatherController.GetInstance().Fog();
                goto default;
            case Global.Effect.rain:
                if (!WeatherController.GetInstance().rain) WeatherController.GetInstance().Rain();
                goto default;
            case Global.Effect.scorch:
                if (cardProperty.effect == Global.Effect.scorch)
                {
                    int maxPower = 0;
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = 0; ii < PlayerController.GetInstance().grids[i].childCount; ii++)
                        {
                            int power = PlayerController.GetInstance().grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                            if (power > maxPower) maxPower = power;
                        }
                    }
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = 0; ii < EnemyController.GetInstance().grids[i].childCount; ii++)
                        {
                            int power = EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                            if (power > maxPower) maxPower = power;
                        }
                    }

                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = PlayerController.GetInstance().grids[i].childCount - 1; ii >= 0; ii--)
                        {
                            if (PlayerController.GetInstance().grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower == maxPower)
                                PlayerController.GetInstance().grids[i].SetParent(ii, PlayerController.GetInstance().grids[5]);
                        }
                    }
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = EnemyController.GetInstance().grids[i].childCount - 1; ii >= 0; ii--)
                        {
                            if (EnemyController.GetInstance().grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower == maxPower)
                                EnemyController.GetInstance().grids[i].SetParent(ii, EnemyController.GetInstance().grids[5]);
                        }
                    }
                }
                goto default;
            case Global.Effect.dummy:
                ShowCards.GetInstance().Show(ShowCards.Behaviour.dummy, PlayerController.GetInstance().grids[2], true);
                return;
            case Global.Effect.warhorn:
                ShowCards.GetInstance().Show(ShowCards.Behaviour.warhorn, PlayerController.GetInstance().grids[2], true);
                return;
            default:
                grid.SetParent(index, PlayerController.GetInstance().grids[(int)cardProperty.line + 2]);
                break;
        }

        if (cardProperty.effect == Global.Effect.nurse)
            ShowCards.GetInstance().Show(ShowCards.Behaviour.draw, PlayerController.GetInstance().grids[5], true);
        else
            ShowCards.GetInstance().Hide();

        PlayerController.GetInstance().Number();
        PowerController.GetInstance().Number();
        EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);
    }

    public void Dummy()
    {
        Transform grid = ShowCards.GetInstance().totalGrid;
        int dummyIndex = 0;
        for (int i = 0; i < grid.childCount; i++)
            if (ShowCards.GetInstance().grid.GetChild(i).name == name) dummyIndex = i;

        PlayerController.GetInstance().grids[1].SetParent(index, grid);
        grid.SetParent(dummyIndex, PlayerController.GetInstance().grids[1]);

        ShowCards.GetInstance().Hide();
        PlayerController.GetInstance().Number();
        PowerController.GetInstance().Number();
        EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);
    }
}
