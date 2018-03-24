using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : Singleton<WeatherController> {
    public Transform grid;
    [SerializeField] GameObject playerFrostSprite;
    [SerializeField] GameObject playerFogSprite;
    [SerializeField] GameObject playerRainSprite;
    [SerializeField] GameObject enemyFrostSprite;
    [SerializeField] GameObject enemyFogSprite;
    [SerializeField] GameObject enemyRainSprite;
    [HideInInspector] public bool[] weather = { false, false, false };

    public void ClearSky()
    {
        for (int i = 0; i < grid.childCount; i++)
            grid.SetParent(i, PlayerController.GetInstance().grids[5]);
        weather[0] = false;
        weather[1] = false;
        weather[2] = false;
        playerFrostSprite.SetActive(false);
        playerFogSprite.SetActive(false);
        playerRainSprite.SetActive(false);
        enemyFrostSprite.SetActive(false);
        enemyFogSprite.SetActive(false);
        enemyRainSprite.SetActive(false);
    }

    public void Frost()
    {
        weather[0] = true;
        playerFrostSprite.SetActive(true);
        enemyFrostSprite.SetActive(true);
    }

    public void Fog()
    {
        weather[1] = true;
        playerFogSprite.SetActive(true);
        enemyFogSprite.SetActive(true);
    }

    public void Rain()
    {
        weather[2] = true;
        playerRainSprite.SetActive(true);
        enemyRainSprite.SetActive(true);
    }
}
