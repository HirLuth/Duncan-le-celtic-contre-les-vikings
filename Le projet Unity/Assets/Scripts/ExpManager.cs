using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public GameObject expPoint;
    public static ExpManager instance;
    public GameObject textLevel;
    public int level;
    private float _currentBonus;
    [SerializeField] private int startExp;
    [SerializeField] private float powerOfX;
    
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _currentBonus = Mathf.Pow(startExp, (float)1 / level);
        PlayerStat.instance.requiredExp = Mathf.RoundToInt(Mathf.Pow(level,1.2f)+_currentBonus);
    }

    private void Update()
    {
        if (PlayerStat.instance.currentExp >= PlayerStat.instance.requiredExp) // LEVEL UP
        {
            level++;
            PlayerStat.instance.level++;
            PlayerStat.instance.currentExp = 0;
            _currentBonus += Mathf.Pow(startExp, (float)1 / level);
            PlayerStat.instance.requiredExp = Mathf.RoundToInt(Mathf.Pow(level,powerOfX)+_currentBonus);
            UIManager.instance.LevelUpEvent();
            textLevel.GetComponent<TextMeshProUGUI>().SetText("Lvl. " + level);
        }
    }

    public void CreateExp(Vector3 ennemyPos, int soulAmount, bool isInExpRange)
    {
        for (int i = 0; i < soulAmount; i++)
        {
            GameObject exp = Instantiate(expPoint, new Vector2(999,999), Quaternion.Euler(0,0,0));
            exp.transform.position = ennemyPos;
            exp.GetComponent<Exp>().isAtracted = isInExpRange;
        }
    }

    public void CollectExp(GameObject collectedSoul, int value)
    {
        Destroy(collectedSoul);
        PlayerStat.instance.currentExp += value;
    }
    
}
