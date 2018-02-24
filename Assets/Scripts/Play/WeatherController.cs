using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {
    public static WeatherController instance;
    public Transform grid;
    public GameObject playerFrostSprite;
    public GameObject playerFogSprite;
    public GameObject playerRainSprite;
    public GameObject enemyFrostSprite;
    public GameObject enemyFogSprite;
    public GameObject enemyRainSprite;
    public bool frost = false;
    public bool fog = false;
    public bool rain = false;

    private void Awake()
    {
        instance = this;
    }

    public void ClearSky()
    {
        for (int i = 0; i < grid.childCount; i++)
            grid.SetParent(i, PlayerController.instance.grids[5]);
        frost = false;
        fog = false;
        rain = false;
        playerFrostSprite.SetActive(false);
        playerFogSprite.SetActive(false);
        playerRainSprite.SetActive(false);
        enemyFrostSprite.SetActive(false);
        enemyFogSprite.SetActive(false);
        enemyRainSprite.SetActive(false);
    }

    public void Frost()
    {
        frost = true;
        playerFrostSprite.SetActive(true);
        enemyFrostSprite.SetActive(true);
    }

    public void Fog()
    {
        fog = true;
        playerFogSprite.SetActive(true);
        enemyFogSprite.SetActive(true);
    }

    public void Rain()
    {
        rain = true;
        playerRainSprite.SetActive(true);
        enemyRainSprite.SetActive(true);
    }
}
