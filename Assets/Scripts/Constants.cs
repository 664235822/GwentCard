using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
    public enum Group { northern, nilfgaardian, monster, scoiatael }
    public enum List { leader, special, monster, neutral }
    public enum Line { melee, ranged, siege, empty, agile }
    public enum Effect { empty, clear_sky, dummy, fog, frost, improve_neighbours, muster, nurse, rain, same_type_morale, scorch, spy, warhorn }
    public static readonly string path = string.Format("{0}/SaveCards.xml", Application.persistentDataPath);
}