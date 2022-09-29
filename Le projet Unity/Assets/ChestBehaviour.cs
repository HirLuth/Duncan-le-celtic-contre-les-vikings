using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Weapons;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class ChestBehaviour : MonoBehaviour
{
    [SerializeField] private ArmesBaseStat globalStats;
    public static ChestBehaviour instance;
    public List<int> listPossibleWeapontoGet;
    [SerializeField] private GameObject chestMenu;
    [SerializeField] private GameObject menuIcon;
    public List<Sprite> spriteList;
    public int spriteActuel;
    public float timeWaited;
    public int healthHealed;

    public bool isRolling;

    private void Awake()
    {
        spriteActuel = 0;
        
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        chestMenu = UIManager.instance.chestMenu;
        menuIcon = UIManager.instance.iconeMenu;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            EventChest();
        }
    }

    private void Update()
    {
        if (isRolling == true)
        {
            StartCoroutine(rollSprite());
        }
    }

    public void EventChest()
    {
        isRolling = true;
        chestMenu.SetActive(true);
        Time.timeScale = 0;

        StartCoroutine(ChoseItem());
    }

    IEnumerator ChoseItem()
    {
        List<int> listForTirage;
        int weaponSorted = UnityEngine.Random.Range(1, 13);
        yield return new WaitForSecondsRealtime(timeWaited);
        menuIcon.GetComponent<Image>().sprite = spriteList[weaponSorted-1];
        isRolling = false;
        EndChestEvent(weaponSorted-1);
    }
    
    public void EndChestEvent(int weaponDataNumber)
    {
        if (weaponDataNumber >= 7)
        {
            CharacterController.instance.health += healthHealed;
            HealthBar.instance.SetHealth(CharacterController.instance.health);
        }
        else
        {
            for (int i = 0; i < UIManager.instance.possessedWeapons.Count; i++)
            {
                Armes arme = UIManager.instance.possessedWeapons[i].GetComponent<Armes>();
                if (weaponDataNumber == (int)arme.weaponType)
                {
                    arme.level += 1;
                    arme.UpdateLevelIndicator();
                    StartCoroutine(WaitBeforeClose());
                    return;
                }
            }
            UIManager.instance.AddWeapon(weaponDataNumber);
        }
        StartCoroutine(WaitBeforeClose());
    }
    
    IEnumerator rollSprite()
    {
        if (isRolling)
        {
            menuIcon.GetComponent<Image>().sprite = spriteList[spriteActuel];
            spriteActuel++;
            if (spriteActuel == 8)
            {
                spriteActuel = 0;
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }
    
    IEnumerator WaitBeforeClose()
    {
        yield return new WaitForSecondsRealtime(timeWaited);
        Time.timeScale = 1;
        chestMenu.SetActive(false);
        Destroy(gameObject);
    }
    
}
