using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Proposition : MonoBehaviour
{
    public int weaponNumberAssociated;
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private TMP_Text weaponDescritpionText;
    [SerializeField] private Image weaponImage;

    public void SetUpApparition(string weaponName, string weaponDescription, Sprite weaponSprite)
    {
        weaponNameText.text = weaponName;
        weaponDescritpionText.text = weaponDescription;
        weaponImage.sprite = weaponSprite;

    }

    public void OnButtonPress()
    {
        Debug.Log(3);
        UIManager.instance.EndLevelUpEvent(weaponNumberAssociated);
    }
}
