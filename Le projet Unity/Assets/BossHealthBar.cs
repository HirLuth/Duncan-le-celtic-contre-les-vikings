using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public static BossHealthBar instance;
    public GameObject expBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (IABoss.instance.bossFight)
        {
            expBar.SetActive(false);
            SetMaxHealth(IABoss.instance.healthBossFight);
        }
    }
    
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate((slider.normalizedValue));
    }
}
