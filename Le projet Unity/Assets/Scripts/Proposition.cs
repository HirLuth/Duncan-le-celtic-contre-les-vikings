using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Weapons;

public class Proposition : MonoBehaviour
{
    public int weaponNumberAssociated;
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private TMP_Text weaponDescritpionText;
    [SerializeField] private Image weaponImage;

    public void SetUpApparition( int weaponInt, string weaponName, string weaponDescription, string weaponLevelUpDescription, Sprite weaponSprite)
    {
        weaponNameText.text = weaponName;
        weaponImage.sprite = weaponSprite;
        for (int i = 0; i < UIManager.instance.possessedWeapons.Count; i++)
        {
            Armes arme = UIManager.instance.possessedWeapons[i].GetComponent<Armes>();
            if (weaponInt == (int)arme.weaponType)
            {
                weaponDescritpionText.text = weaponLevelUpDescription;
                return;
            }
        }
        weaponDescritpionText.text = weaponDescription;
    }

    public void OnButtonPress()
    {
        UIManager.instance.EndLevelUpEvent(weaponNumberAssociated);
    }
}
