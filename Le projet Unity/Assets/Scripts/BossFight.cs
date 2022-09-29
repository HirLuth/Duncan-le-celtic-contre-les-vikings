using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public static BossFight instance;
    [SerializeField] private GameObject bossFightArena;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Instantiate(bossFightArena,CharacterController.instance.transform.position,Quaternion.identity);
    }
}
