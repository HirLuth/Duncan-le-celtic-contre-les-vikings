using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{

 public static CharacterController instance;
 public float speed;
 private float lookingAt;
 private Rigidbody2D rb;
 private float movementX;
 private float movementY;
 
    private void Awake()
    {
         if (instance == null)
         {
             instance = this;
         }

         rb = gameObject.GetComponent<Rigidbody2D>();   
    }


    public void Update()
    {
        Debug.Log(movementX);
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        
        rb.velocity = (new Vector2(movementX * speed, movementY * speed));
        
        
        if (movementX > 0) // Le personnage s'oriente vers la direction où il marche. 
        {
            lookingAt = 2;
            //transform.localRotation = new Quaternion(0, 0,0,1);
            transform.localScale = new Vector3(1, 2, 0);
        }
        else if (movementX < 0)
        {
            lookingAt = 1;
            //transform.localRotation = new Quaternion(0, 0,0,1);
            transform.localScale = new Vector3(-1, 2, 0);
        }
    }
}
