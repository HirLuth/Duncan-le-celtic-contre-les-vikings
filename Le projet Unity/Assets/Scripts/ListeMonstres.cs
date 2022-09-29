using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeMonstres : MonoBehaviour
{
    public List<GameObject> ennemyList;
    public static ListeMonstres instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
}
