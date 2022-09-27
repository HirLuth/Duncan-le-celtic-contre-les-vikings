using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolManager : MonoBehaviour
{
    private float lenght;
    private Vector2 startpos;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startpos = new Vector2(transform.position.x, transform.position.y);
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxEffect));
        float tempY = (cam.transform.position.y * (1 - parallaxEffect));
        float distX = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startpos.x + distX, transform.position.y, transform.position.z);

        if (tempX > startpos.x + lenght)
        {
            startpos.x += lenght;
        }
        else if (tempX < startpos.x - lenght)
        {
            startpos.x -= lenght;
        }
        
        if (tempY > startpos.y + lenght)
        {
            startpos.y += lenght;
        }
        else if (tempY < startpos.y - lenght)
        {
            startpos.y -= lenght;
        }
    }
    
   
}
