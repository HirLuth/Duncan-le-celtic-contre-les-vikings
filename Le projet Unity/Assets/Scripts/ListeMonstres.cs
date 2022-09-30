using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeMonstres : MonoBehaviour
{
    public List<GameObject> ennemyList;
    public static ListeMonstres instance;
    public int score;
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
}
