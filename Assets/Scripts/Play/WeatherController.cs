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
        grid.DestroyChildren();
        grid.GetComponent<UIGrid>().Reposition();
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

    public void Frost(int index)
    {
        if (!frost)
        {
            PlayerController.instance.grids[1].GetChild(index).SetParent(grid);
            grid.GetComponent<UIGrid>().Reposition();
            frost = true;
            playerFrostSprite.SetActive(true);
            enemyFrostSprite.SetActive(true);
        }
    }

    public void Fog(int index)
    {
        if (!fog)
        {
            PlayerController.instance.grids[1].GetChild(index).SetParent(grid);
            grid.GetComponent<UIGrid>().Reposition();
            fog = true;
            playerFogSprite.SetActive(true);
            enemyFogSprite.SetActive(true);
        }
    }

    public void Rain(int index)
    {
        if (!rain)
        {
            PlayerController.instance.grids[1].GetChild(index).SetParent(grid);
            grid.GetComponent<UIGrid>().Reposition();
            rain = true;
            playerRainSprite.SetActive(true);
            enemyRainSprite.SetActive(true);
        }
    }
}
