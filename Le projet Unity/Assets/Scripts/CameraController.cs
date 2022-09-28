using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance; 
    public GameObject player;
    public new Camera camera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y,-10); 
    }
}
