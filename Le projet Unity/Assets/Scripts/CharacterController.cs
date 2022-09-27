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
 public int health;
 private bool isTouched;
 public static bool isTakingDamage;
 
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
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        
        rb.velocity = (new Vector2(movementX * speed, movementY * speed));
        
        
        if (movementX > 0) // Le personnage s'oriente vers la direction où il marche. 
        {
            lookingAt = 2;
            transform.localScale = new Vector3(1, 2, 0);
        }
        else if (movementX < 0)
        {
            lookingAt = 1;
            transform.localScale = new Vector3(-1, 2, 0);
        }

        if (isTakingDamage)
        {
            StartCoroutine(ChangeColor());
        }

        if (health == 0) // mort du personnage
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
    }

    IEnumerator ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

   /* private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Exp") && Exp.instance.haspoofed)
        {
            Debug.Log("samère");
            Exp.instance.CollectExp(col.gameObject, 1);
        }
    }*/
    
}
