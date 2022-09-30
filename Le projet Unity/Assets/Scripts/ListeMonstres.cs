using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ListeMonstres : MonoBehaviour
{
    public List<GameObject> ennemyList;
    public static ListeMonstres instance;
    public int score;
    public GameObject splashScreen;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddScore(int scoreAmound)
    {
        score += scoreAmound;
    }

    public void Start()
    {
        Time.timeScale = 0;
        splashScreen.SetActive(true);
    }

    public void Commencer()
    {
        splashScreen.SetActive(false);
    }
}
