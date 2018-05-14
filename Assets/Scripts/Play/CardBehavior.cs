using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavior : MonoBehaviour {
    [HideInInspector] public int totalPower;
    CardProperty cardProperty;

    private void Awake()
    {
        cardProperty = GetComponent<CardProperty>();
    }

    public void Play()
    {
        int index = 0;
        for (int i = 0; i < ShowCards.GetInstance().totalGrid.childCount; i++)
            if (ShowCards.GetInstance().grid.GetChild(i).name == name) index = i;
        ShowCards.GetInstance().card = ShowCards.GetInstance().totalGrid.GetChild(index);

        switch (cardProperty.effect)
        {
            case Global.Effect.spy:
                ShowCards.GetInstance().card.SetTarget(EnemyController.GetInstance().grids[(int)cardProperty.line + 2]);
                PlayerController.GetInstance().DrawCards(2);
                break;
            case Global.Effect.clear_sky:
                WeatherController.GetInstance().ClearSky();
                goto default;
            case Global.Effect.frost:
                if (!WeatherController.GetInstance().weather[0])
                {
                    WeatherController.GetInstance().Frost();
                    ShowCards.GetInstance().card.SetTarget(WeatherController.GetInstance().grid);
                    break;
                }
                else goto default;
            case Global.Effect.fog:
                if (!WeatherController.GetInstance().weather[1])
                {
                    WeatherController.GetInstance().Fog();
                    ShowCards.GetInstance().card.SetTarget(WeatherController.GetInstance().grid);
                    break;
                }
                else goto default;
            case Global.Effect.rain:
                if (!WeatherController.GetInstance().weather[2])
                {
                    WeatherController.GetInstance().Rain();
                    ShowCards.GetInstance().card.SetTarget(WeatherController.GetInstance().grid);
                    break;
                }
                else goto default;
            case Global.Effect.scorch:
                if (cardProperty.effect == Global.Effect.scorch)
                {
                    int maxPower = 0;
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = 0; ii < PlayerController.GetInstance().grids[i].childCount; ii++)
                        {
                            Transform scorchCard = PlayerController.GetInstance().grids[i].GetChild(ii);
                            if (!scorchCard.GetComponent<CardProperty>().gold)
                            {
                                int power = scorchCard.GetComponent<CardBehavior>().totalPower;
                                if (power > maxPower) maxPower = power;
                            }
                        }
                    }
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = 0; ii < EnemyController.GetInstance().grids[i].childCount; ii++)
                        {
                            Transform scorchCard = EnemyController.GetInstance().grids[i].GetChild(ii);
                            if (!scorchCard.GetComponent<CardProperty>().gold)
                            {
                                int power = scorchCard.GetComponent<CardBehavior>().totalPower;
                                if (power > maxPower) maxPower = power;
                            }
                        }
                    }

                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = PlayerController.GetInstance().grids[i].childCount - 1; ii >= 0; ii--)
                        {
                            Transform scorchCard = PlayerController.GetInstance().grids[i].GetChild(ii);
                            if (scorchCard.GetComponent<CardBehavior>().totalPower == maxPower && !scorchCard.GetComponent<CardProperty>().gold)
                                scorchCard.SetTarget(PlayerController.GetInstance().grids[5]);
                        }
                    }
                    for (int i = 2; i < 5; i++)
                    {
                        for (int ii = EnemyController.GetInstance().grids[i].childCount - 1; ii >= 0; ii--)
                        {
                            Transform scorchCard = EnemyController.GetInstance().grids[i].GetChild(ii);
                            if (scorchCard.GetComponent<CardBehavior>().totalPower == maxPower && !scorchCard.GetComponent<CardProperty>().gold)
                                scorchCard.SetTarget(EnemyController.GetInstance().grids[5]);
                        }
                    }
                }
                goto default;
            case Global.Effect.dummy:
                ShowCards.GetInstance().Show(ShowCards.ShowBehavior.dummy, PlayerController.GetInstance().grids[2], true);
                return;
            case Global.Effect.muster:
                MusterController.GetInstance().Muster();
                goto default;
            case Global.Effect.warhorn:
                if (cardProperty.line == Global.Line.empty)
                {
                    ShowCards.GetInstance().Show(ShowCards.ShowBehavior.warhorn, PlayerController.GetInstance().grids[2], true);
                    return;
                }
                else
                {
                    WarhornController.GetInstance().playerWarhorn[(int)cardProperty.line] = true;
                    goto default;
                }
            case Global.Effect.agile:
                ShowCards.GetInstance().Show(ShowCards.ShowBehavior.agile, PlayerController.GetInstance().grids[2], true);
                return;
            default:
                ShowCards.GetInstance().card.SetTarget(PlayerController.GetInstance().grids[(int)cardProperty.line + 2]);
                break;
        }

        if (cardProperty.effect == Global.Effect.nurse)
        {
            ShowCards.GetInstance().Show(ShowCards.ShowBehavior.nurse, PlayerController.GetInstance().grids[5], true);
            CoroutineManager.GetInstance().AddTask(TweenCard.GetInstance().Play(ShowCards.GetInstance().card));
            return;
        }

        ShowCards.GetInstance().Hide();
        PlayerController.GetInstance().PlayOver(ShowCards.GetInstance().card);
    }

    public void Dummy()
    {
        int dummyIndex = 0;

        for (int i = 0; i < PlayerController.GetInstance().grids[ShowCards.GetInstance().totalLine + 2].childCount; i++)
            if (ShowCards.GetInstance().grid.GetChild(i).name == name) dummyIndex = i;

        ShowCards.GetInstance().card.SetTarget(PlayerController.GetInstance().grids[ShowCards.GetInstance().totalLine + 2]);
        PlayerController.GetInstance().grids[ShowCards.GetInstance().totalLine + 2].GetChild(dummyIndex).SetTarget(ShowCards.GetInstance().totalGrid);

        ShowCards.GetInstance().Hide();
        PlayerController.GetInstance().PlayOver(ShowCards.GetInstance().card);
    }

    public void Replace()
    {
        ShowCards.GetInstance().replaceInt++;

        int replaceIndex = 0;
        for (int i = 0; i < ShowCards.GetInstance().grid.childCount; i++)
            if (ShowCards.GetInstance().grid.GetChild(i).name == name) replaceIndex = i;

        PlayerController.GetInstance().grids[1].GetChild(replaceIndex).SetTarget(PlayerController.GetInstance().grids[0]);
        PlayerController.GetInstance().grids[0].GetChild(Random.Range(0, PlayerController.GetInstance().grids[0].childCount)).SetTarget(PlayerController.GetInstance().grids[1]);

        if (ShowCards.GetInstance().replaceInt != 2)
            ShowCards.GetInstance().Show(ShowCards.ShowBehavior.replace, PlayerController.GetInstance().grids[1], true);
        else
        {
            ShowCards.GetInstance().Hide();
            GameController.GetInstance().StartGame();
        }
    }
}
