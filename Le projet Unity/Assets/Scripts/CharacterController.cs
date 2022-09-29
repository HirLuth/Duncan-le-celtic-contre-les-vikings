using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{

 public static CharacterController instance;
 public float speed;
 public int lookingAt;
 private Rigidbody2D rb;
 private float movementX;
 private float movementY;
 public int health;
 public int maxHealth = 100;
 private bool isTouched;
 public static bool isTakingDamage;
 public List<float> listPositionDegree;
 public Vector2 VectorDepla;

    private void Awake()
    {
         if (instance == null)
         {
             instance = this;
         }

         rb = gameObject.GetComponent<Rigidbody2D>();   
    }

    private void Start()
    {
        HealthBar.instance.SetMaxHealth(100);
    }

    public void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        VectorDepla = new Vector2(movementX, movementY);
        
        rb.velocity = (new Vector2(movementX * speed, movementY * speed));
        
        
        if (movementX > 0) // Le personnage s'oriente vers la direction où il marche. 
        {
            lookingAt = 2;
            transform.localScale = new Vector3(1, 2, 0);
        }
        else if (movementX < 0)
        {
            lookingAt = 0;
            transform.localScale = new Vector3(-1, 2, 0);
        }
        
        if (movementY > 0) // utile pour lancer des armes dans la bonne direction
        {
            if (movementX > 0)
            {
                lookingAt = 2;
            }
            else if (movementX < 0)
            {
                lookingAt = 8;
            }
            else
            {
                lookingAt = 1;
            }
            
        }
        else if (movementY < 0)
        {
            if (movementX > 0)
            {
                lookingAt = 4;
            }
            else if (movementX < 0)
            {
                lookingAt = 6;
            }
            else
            {
                lookingAt = 5;
            }
        }
        else
        {
            if (movementX > 0)
            {
                lookingAt = 3;
            }
            else if (movementX<0)
            {
                lookingAt = 7;
            }
        }

        if (isTakingDamage)
        {
            StartCoroutine(ChangeColor());
        }

        if (health <= 0) // mort du personnage
        {
            Destroy(gameObject);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        HealthBar.instance.SetHealth(health);
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
