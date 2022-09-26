using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armes : MonoBehaviour

{

    public ItemManager.WeaponsType weaponType;
    public ArmesBaseStat baseStat;
    public int Level;
    public bool isTransformed;
    public int damage;
    public int numberOfProjectile;
    public float coolDown;
    public float projectileSize;
    public float projectileSpeed;
    public float timeOfTheEffect;

    private void Start()
    {
        /*Level = baseStat.Level;
        isTransformed = baseStat.isTransformed;
        damage = baseStat.damage;
        numberOfProjectile = baseStat.numberOfProjectile;
        coolDown = baseStat.coolDown;
        projectileSize = baseStat.projectileSize;
        projectileSpeed = baseStat.projectileSpeed;
        timeOfTheEffect = baseStat.timeOfTheEffect;*/
    }
}
