using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {
    public static WeatherController instance;
    public Transform grid;
    [SerializeField] GameObject playerFrostSprite;
    [SerializeField] GameObject playerFogSprite;
    [SerializeField] GameObject playerRainSprite;
    [SerializeField] GameObject enemyFrostSprite;
    [SerializeField] GameObject enemyFogSprite;
    [SerializeField] GameObject enemyRainSprite;
    [HideInInspector] public bool frost = false;
    [HideInInspector] public bool fog = false;
    [HideInInspector] public bool rain = false;

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
