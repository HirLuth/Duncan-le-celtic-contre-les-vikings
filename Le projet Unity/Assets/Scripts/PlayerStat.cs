using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;
    [Header("Player SelfStats")] 
    public PlayerStat baseStats;
    public int level;
    public int MaxHealth;
    public int currentHealth;
    public int defense;
    public int requiredExp;
    public int currentExp;
    public float expModifier;
    public float moveSpeed;
    
    [Header("Player WeaponStat modifier")]
    public float damageModifier;
    public float numberOfProjectileModifier;
    public float coolDownModifier;
    public float projectileSizeModifier;
    public float projectileSpeedModifier;
    public float timeOfTheEffectModifier;

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

   /* private void Start()
    {
        level = baseStats.level;
        MaxHealth = baseStats.MaxHealth;
        currentHealth = MaxHealth;
        defense = baseStats.defense;
        requiredExp = baseStats.requiredExp;
        currentExp = 0;
        expModifier = baseStats.expModifier;
        moveSpeed = baseStats.moveSpeed;

    }*/
}
