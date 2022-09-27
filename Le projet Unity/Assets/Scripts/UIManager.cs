using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> listOfPositions;
    public List<GameObject> listOfWeapons;
    public List<GameObject> possessedWeapons;

    private void Start()
    {
        AddWeapon(ItemManager.WeaponsType.Javelot);
    }

    public void AddWeapon(ItemManager.WeaponsType weaponsType)
    {
        int placeInThelist = possessedWeapons.Count + 1;
        GameObject newWeapon = Instantiate(listOfWeapons[(int)weaponsType]);
        possessedWeapons.Add(newWeapon);
        newWeapon.transform.position = listOfPositions[placeInThelist].transform.position;
    }
}
