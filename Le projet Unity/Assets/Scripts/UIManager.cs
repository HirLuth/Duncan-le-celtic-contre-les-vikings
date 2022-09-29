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
    private bool proposeGigot;
    public GameObject chestMenu;
    public GameObject iconeMenu;
    public int maxLevel;


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
       StartEvent();
    }

    public void LevelUpEvent()
    {
        if (proposeGigot)
        {
           return; 
        }
        levelUpMenu.SetActive(true);
        Time.timeScale = 0;
        List<int> listForTirage;
        listForTirage = listPossibleWeapontoGet.ToList();
        for (int i = 0; i < propositions.Count; i++)
        {
            int weaponSorted = listForTirage[Random.Range(0, listForTirage.Count)];
            listForTirage.Remove(weaponSorted);
            propositions[i].SetUpApparition((int)globalStats.listBaseStats[weaponSorted].weaponType,
                globalStats.listBaseStats[weaponSorted].nameInMenus,
                globalStats.listBaseStats[weaponSorted].description,
                globalStats.listBaseStats[weaponSorted].descritpionLevelUp,
                globalStats.listBaseStats[weaponSorted].sprite);
            propositions[i].weaponNumberAssociated = weaponSorted;
        }
    }

    public void StartEvent()
    {
        levelUpMenu.SetActive(true);
        Time.timeScale = 0;
        List<int> listForTirage;
        listForTirage = listPossibleWeapontoGet.ToList();
        listForTirage.Remove(3);
        for (int i = 0; i < propositions.Count; i++)
        {
            int weaponSorted = listForTirage[Random.Range(0, listForTirage.Count)];
            listForTirage.Remove(weaponSorted);
            propositions[i].SetUpApparition((int)globalStats.listBaseStats[weaponSorted].weaponType,
                globalStats.listBaseStats[weaponSorted].nameInMenus,
                globalStats.listBaseStats[weaponSorted].description,
                globalStats.listBaseStats[weaponSorted].descritpionLevelUp,
                globalStats.listBaseStats[weaponSorted].sprite);
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
                LevelUpWeapon(arme);
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

    public void LevelUpWeapon(Armes weaponToLevelUp)
    {
        weaponToLevelUp.level += 1;
        if (weaponToLevelUp.level == maxLevel)
        {
            for (int i = 0; i < listPossibleWeapontoGet.Count; i++)
            {
                if (listPossibleWeapontoGet[i] == (int)weaponToLevelUp.weaponType)
                {
                    listPossibleWeapontoGet.RemoveAt(i);
                    if (listPossibleWeapontoGet.Count == 0)
                    {
                        proposeGigot = true;
                    }
                    else if (listPossibleWeapontoGet.Count<2)
                    {
                        Destroy(propositions[1].gameObject);
                        propositions.RemoveAt(1);
                        Debug.Log(1);
                    }
                    else if (listPossibleWeapontoGet.Count<3)
                    {
                        Debug.Log(2);
                        Destroy(propositions[2].gameObject);
                        propositions.RemoveAt(2);
                    }
                }
            }
        }
    }
}
