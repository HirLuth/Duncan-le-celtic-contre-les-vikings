using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public enum WeaponsType
    {
        Javelot = 0,
        Epee = 1,
        Serpe = 2,
        Bouclier = 3,
        Livre = 4,
        Baton = 5,
        Carnyx = 6
    }
}
