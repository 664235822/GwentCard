using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public UIAtlas[] atlas;
    bool offensive;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame(string PlayerGroup)
    {
        PlayerController.instance.Initialize(PlayerGroup);
        EnemyController.instance.Initialize();

        int random = Random.Range(0, 2);
        if (random == 0) offensive = true;
        else offensive = false;

        if (!offensive) EnemyController.instance.Play();
    }

}
