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
        for (int i = 0; i < ShowCards.GetInstance().totalGrid.childCount; i++)
            if (ShowCards.GetInstance().grid.GetChild(i).name == name) index = i;

        switch (cardProperty.effect)
        {
            case Global.Effect.spy:
                ShowCards.GetInstance().totalGrid.SetParent(index, EnemyController.GetInstance().grids[(int)cardProperty.line + 2]);
                PlayerController.GetInstance().DrawCards(2);
                break;
            case Global.Effect.clear_sky:
                WeatherController.GetInstance().ClearSky();
                goto default;
            case Global.Effect.frost:
                if (!WeatherController.GetInstance().weather[0])
                {
                    WeatherController.GetInstance().Frost();
                    ShowCards.GetInstance().totalGrid.SetParent(index, WeatherController.GetInstance().grid);
                    break;
                }
                else goto default;
            case Global.Effect.fog:
                if (!WeatherController.GetInstance().weather[1])
                {
                    WeatherController.GetInstance().Fog();
                    ShowCards.GetInstance().totalGrid.SetParent(index, WeatherController.GetInstance().grid);
                    break;
                }
                else goto default;
            case Global.Effect.rain:
                if (!WeatherController.GetInstance().weather[2])
                {
                    WeatherController.GetInstance().Rain();
                    ShowCards.GetInstance().totalGrid.SetParent(index, WeatherController.GetInstance().grid);
                    break;
                }
                else goto default;
            case Global.Effect.scorch:
                if (cardProperty.effect == Global.Effect.scorch)
                {
                    int maxPower = 10;
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
                            Transform card = PlayerController.GetInstance().grids[i].GetChild(ii);
                            if (card.GetComponent<CardBehavior>().totalPower == maxPower && !card.GetComponent<CardProperty>().gold)
                                PlayerController.GetInstance().grids[i].SetParent(ii, PlayerController.GetInstance().grids[5]);
                        }
                    }
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = EnemyController.GetInstance().grids[i].childCount - 1; ii >= 0; ii--)
                        {
                            Transform card = EnemyController.GetInstance().grids[i].GetChild(ii);
                            if (card.GetComponent<CardBehavior>().totalPower == maxPower && !card.GetComponent<CardProperty>().gold)
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
                ShowCards.GetInstance().totalGrid.SetParent(index, PlayerController.GetInstance().grids[(int)cardProperty.line + 2]);
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
        int dummyIndex = 0;

        for (int i = 0; i < PlayerController.GetInstance().grids[ShowCards.GetInstance().totalLine + 2].childCount; i++)
            if (ShowCards.GetInstance().grid.GetChild(i).name == name) dummyIndex = i;

        ShowCards.GetInstance().totalGrid.SetParent(index, PlayerController.GetInstance().grids[ShowCards.GetInstance().totalLine + 2]);
        PlayerController.GetInstance().grids[ShowCards.GetInstance().totalLine + 2].SetParent(dummyIndex, ShowCards.GetInstance().totalGrid);

        ShowCards.GetInstance().Hide();
        PlayerController.GetInstance().Number();
        PowerController.GetInstance().Number();
        EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]);
    }
}
