using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class ChestBehaviour : MonoBehaviour
{
    public static ChestBehaviour instance;
    public List<GameObject> listOfPositions;
    public List<GameObject> listOfWeapons;
    public List<GameObject> possessedWeapons;
    public List<Proposition> propositions;
    public List<int> listPossibleWeapontoGet;
    [SerializeField] private GameObject ChestMenu;
    [SerializeField] private GameObject menuIcon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            EventChest();
            Destroy(gameObject);
        }
    }
    
    public void EventChest()
    {
        ChestMenu.SetActive(true);
        Time.timeScale = 0;
        List<int> listForTirage;
        listForTirage = listPossibleWeapontoGet.ToList();
        for (int i = 0; i < propositions.Count; i++)
        {
            int weaponSorted = listForTirage[UnityEngine.Random.Range(0, listForTirage.Count)];
            listForTirage.Remove(weaponSorted);
            //propositions[i].SetUpApparition(globalStats.listBaseStats[weaponSorted].nameInMenus,globalStats.listBaseStats[weaponSorted].description,globalStats.listBaseStats[weaponSorted].sprite);
            propositions[i].weaponNumberAssociated = weaponSorted;
        }
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
