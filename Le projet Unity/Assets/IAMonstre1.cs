using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMonstre1 : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float colldownDmg;
    public int Damages;
    public bool isMoving;
    
    public float timerDmg;
    
    private bool isTouching;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 target = new Vector2(player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y);

        if (isMoving)
        {
         transform.Translate(target.normalized*speed*Time.deltaTime,Space.Self);
        }

        if (isTouching)
        {
            timerDmg += Time.deltaTime;

            if (timerDmg > colldownDmg)
            {
                CharacterController.instance.TakeDamage(Damages);
                timerDmg = 0;
            }
        }
    }
    
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isTouching = false;
            timerDmg = 0;
        }
    }
} 

