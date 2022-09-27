using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public GameObject expPoint;
    public int maxExpAddPerLevel;
    public static ExpManager instance;
    
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (PlayerStat.instance.currentExp >= PlayerStat.instance.requiredExp) // LEVEL UP
        {
            PlayerStat.instance.level++;
            PlayerStat.instance.currentExp = 0;
            PlayerStat.instance.requiredExp += maxExpAddPerLevel;
        }
    }

    public void CreateExp(Vector3 ennemyPos, int soulAmount)
    {
        for (int i = 0; i <= soulAmount; i++)
        {
            Instantiate(expPoint, ennemyPos, Quaternion.Euler(0,0,0));
        }
    }

    public void CollectExp(GameObject collectedSoul, int value)
    {
        Destroy(collectedSoul);
        PlayerStat.instance.currentExp += value;
    }
    
}
