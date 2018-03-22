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
        Transform grid = ShowCards.instance.totalGrid;
        for (int i = 0; i < grid.childCount; i++)
            if (ShowCards.instance.grid.GetChild(i).name == name) index = i;

        switch (cardProperty.effect)
        {
            case Global.Effect.spy:
                grid.SetParent(index, EnemyController.instance.grids[(int)cardProperty.line + 2]);
                PlayerController.instance.DrawCards(2);
                break;
            case Global.Effect.clear_sky:
                WeatherController.instance.ClearSky();
                goto default;
            case Global.Effect.frost:
                if (!WeatherController.instance.frost) WeatherController.instance.Frost();
                goto default;
            case Global.Effect.fog:
                if (!WeatherController.instance.fog) WeatherController.instance.Fog();
                goto default;
            case Global.Effect.rain:
                if (!WeatherController.instance.rain) WeatherController.instance.Rain();
                goto default;
            case Global.Effect.scorch:
                if (cardProperty.effect == Global.Effect.scorch)
                {
                    int maxPower = 0;
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = 0; ii < PlayerController.instance.grids[i].childCount; ii++)
                        {
                            int power = PlayerController.instance.grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                            if (power > maxPower) maxPower = power;
                        }
                    }
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = 0; ii < EnemyController.instance.grids[i].childCount; ii++)
                        {
                            int power = EnemyController.instance.grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower;
                            if (power > maxPower) maxPower = power;
                        }
                    }

                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = PlayerController.instance.grids[i].childCount - 1; ii >= 0; ii--)
                        {
                            if (PlayerController.instance.grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower == maxPower)
                                PlayerController.instance.grids[i].SetParent(ii, PlayerController.instance.grids[5]);
                        }
                    }
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = EnemyController.instance.grids[i].childCount - 1; ii >= 0; ii--)
                        {
                            if (EnemyController.instance.grids[i].GetChild(ii).GetComponent<CardBehavior>().totalPower == maxPower)
                                EnemyController.instance.grids[i].SetParent(ii, EnemyController.instance.grids[5]);
                        }
                    }
                }
                goto default;
            case Global.Effect.dummy:
                ShowCards.instance.Show(ShowCards.Behaviour.dummy, PlayerController.instance.grids[2], true);
                return;
            case Global.Effect.warhorn:
                ShowCards.instance.Show(ShowCards.Behaviour.warhorn, PlayerController.instance.grids[2], true);
                return;
            default:
                grid.SetParent(index, PlayerController.instance.grids[(int)cardProperty.line + 2]);
                break;
        }

        if (cardProperty.effect == Global.Effect.nurse)
            ShowCards.instance.Show(ShowCards.Behaviour.draw, PlayerController.instance.grids[5], true);
        else
            ShowCards.instance.Hide();

        PlayerController.instance.Number();
        PowerController.instance.Number();
        EnemyController.instance.Play(EnemyController.instance.grids[1]);
    }

    public void Dummy()
    {
        Transform grid = ShowCards.instance.totalGrid;
        int dummyIndex = 0;
        for (int i = 0; i < grid.childCount; i++)
            if (ShowCards.instance.grid.GetChild(i).name == name) dummyIndex = i;

        PlayerController.instance.grids[1].SetParent(index, grid);
        grid.SetParent(dummyIndex, PlayerController.instance.grids[1]);

        ShowCards.instance.Hide();
        PlayerController.instance.Number();
        PowerController.instance.Number();
        EnemyController.instance.Play(EnemyController.instance.grids[1]);
    }
}
