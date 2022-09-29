using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public GameObject expPoint;
    public int maxExpAddPerLevel;
    public static ExpManager instance;
    public GameObject textLevel;
    public int level;
    
    
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
            level++;
            PlayerStat.instance.level++;
            PlayerStat.instance.currentExp = 0;
            PlayerStat.instance.requiredExp += maxExpAddPerLevel;
            UIManager.instance.LevelUpEvent();
            textLevel.GetComponent<TextMeshProUGUI>().SetText("Lvl. " + level);
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
