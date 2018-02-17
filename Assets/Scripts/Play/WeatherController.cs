using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {
    public static WeatherController instance;
    public Transform grid;
    public GameObject frostSprite;
    public GameObject fogSprite;
    public GameObject rainSprite;
    public bool frost, fog, rain;

    private void Awake()
    {
        instance = this;
    }

    public void ClearSky()
    {
        grid.DestroyChildren();
        frost = false;
        fog = false;
        rain = false;
        frostSprite.SetActive(false);
        fogSprite.SetActive(false);
        rainSprite.SetActive(false);
    }

    public void Frost(int index)
    {
        if (!WeatherController.instance.frost)
        {
            PlayerController.instance.grids[1].GetChild(index).SetParent(grid);
            frost = true;
            frostSprite.SetActive(true);
        }
    }

    public void Fog(int index)
    {
        if (!WeatherController.instance.fog)
        {
            PlayerController.instance.grids[1].GetChild(index).SetParent(grid);
            fog = true;
            fogSprite.SetActive(true);
        }
    }

    public void Rain(int index)
    {
        if (!WeatherController.instance.fog)
        {
            PlayerController.instance.grids[1].GetChild(index).SetParent(grid);
            rain = true;
            rainSprite.SetActive(true);
        }
    }
}
