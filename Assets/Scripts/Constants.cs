using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
    public enum Group { northern, nilfgaardian, monster, scoiatael }
    public enum List { leader, special, monster, neutral }
    public enum Line { melee, ranged, siege, empty, agile }
    public enum Effect { empty, clear_sky, dummy, fog, frost, improve_neighbours, muster, nurse, rain, same_type_morale, scorch, spy, warhorn }
    public static readonly string path = string.Format("{0}/PlayerCards.xml", Application.persistentDataPath);
    public static readonly string enemyPath = string.Format("{0}/EnemyCards.xml", Application.persistentDataPath);

    public static void SetParent(Transform Grid1, Transform Grid2, int index)
    {
        Grid1.GetChild(index).SetParent(Grid2);
        if (Grid1.GetComponent<UIGrid>() != null) Grid1.GetComponent<UIGrid>().Reposition();
        if (Grid2.GetComponent<UIGrid>() != null) Grid2.GetComponent<UIGrid>().Reposition();
    }
}