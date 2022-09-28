using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    public float timeToDestroy;
    
    void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,timeToDestroy);
    }
}
