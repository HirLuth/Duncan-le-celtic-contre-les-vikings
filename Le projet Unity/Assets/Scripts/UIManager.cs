using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private ArmesBaseStat globalStats;
    public List<GameObject> listOfPositions;
    public List<GameObject> listOfWeapons;
    public List<GameObject> possessedWeapons;
    public List<Proposition> propositions;
    public List<int> listPossibleWeapontoGet;
    [SerializeField] private GameObject levelUpMenu;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private void Start()
    {
       LevelUpEvent();
    }

    public void LevelUpEvent()
    {
        levelUpMenu.SetActive(true);
        Time.timeScale = 0;
        List<int> listForTirage;
        listForTirage = listPossibleWeapontoGet.ToList();
        for (int i = 0; i < propositions.Count; i++)
        {
            int weaponSorted = listForTirage[Random.Range(0, listForTirage.Count)];
            listForTirage.Remove(weaponSorted);
            propositions[i].SetUpApparition(globalStats.listBaseStats[weaponSorted].nameInMenus,globalStats.listBaseStats[weaponSorted].description,globalStats.listBaseStats[weaponSorted].sprite);
            propositions[i].weaponNumberAssociated = weaponSorted;
        }
    }

    public void EndLevelUpEvent(int weaponDataNumber)
    {
        for (int i = 0; i < possessedWeapons.Count; i++)
        {
            Armes arme = possessedWeapons[i].GetComponent<Armes>();
            if (weaponDataNumber == (int)arme.weaponType)
            {
                arme.level += 1;
                arme.UpdateLevelIndicator();
                Time.timeScale = 1;
                levelUpMenu.SetActive(false);
                return;
            }
        }
        AddWeapon(weaponDataNumber);
        Time.timeScale = 1;
        levelUpMenu.SetActive(false);
    }


    public void AddWeapon(int weaponsType)
    {
        int placeInThelist = possessedWeapons.Count;
        GameObject newWeapon = Instantiate(listOfWeapons[weaponsType]);
        possessedWeapons.Add(newWeapon);
        newWeapon.transform.position = listOfPositions[placeInThelist].transform.position;
        newWeapon.transform.parent = listOfPositions[placeInThelist].transform;
    }
}
