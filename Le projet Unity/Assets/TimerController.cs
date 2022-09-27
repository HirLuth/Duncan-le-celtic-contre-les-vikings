using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public float timer;

    [SerializeField] 
    private TextMeshProUGUI firstMinute;
    [SerializeField] 
    private TextMeshProUGUI secondMinute;
    [SerializeField] 
    private TextMeshProUGUI separator;
    [SerializeField] 
    private TextMeshProUGUI firstSecond;
    [SerializeField] 
    private TextMeshProUGUI secondSecond;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerDisplay(timer);

        if (timer >= 1200)
        {
            Debug.Log("Combat de Boss");
        }
    }
    

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    private void Flash()
    {
        if (timer != 0)
        {
            timer = 0;
            UpdateTimerDisplay(timer);
        }
    }
}
