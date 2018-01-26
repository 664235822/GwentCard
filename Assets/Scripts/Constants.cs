using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
    public enum Group { northern, nilfgaardian, monster, scoiatael }
    public enum List { leader, special, monster, neutral }
    public static readonly string path = string.Format("{0}/SaveCards.xml", Application.persistentDataPath);
}
