using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Exp : MonoBehaviour
{
    public static Exp instance;
    
    public Vector3 playerPos;
    [SerializeField] private float force = 3f;
    [SerializeField] private float timer = 1;
    [SerializeField] private float deceleration = 0.3f;
    [SerializeField] private int xpValue;
    public bool haspoofed = false;
    public Rigidbody2D rb;
    private bool isAtracted;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       
        Vector2 explode = new Vector2(Random.Range(-force, force), Random.Range(-force, force));
        rb.AddForce(explode, ForceMode2D.Impulse);
        rb.drag = deceleration;
        
    }

    void Update()
    {
        playerPos = CharacterController.instance.transform.position;
        Vector3 dir = playerPos - transform.position;
        Vector3 dirNormalised = dir.normalized;

        if (timer >= 0)
        {
            rb.velocity -= rb.velocity * 0.4f;
            timer -= Time.deltaTime;
        }

        if (isAtracted)
        {
            PoofAway(dirNormalised);
            haspoofed = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector3 dir = playerPos - transform.position;
        Vector3 dirNormalised = dir.normalized;
        
        if (col.gameObject.CompareTag("Player"))
        {
            if (timer <= 0)
            {
                if (!haspoofed)
                {
                    isAtracted = true;
                }
            }
        }
        
        if (col.gameObject.CompareTag("GetExp"))
        {
            ExpManager.instance.CollectExp(gameObject, xpValue);  
        }
    }
    

    public void PoofAway(Vector2 dir)
    {
        if (!haspoofed)
        {
            //rb.AddForce(-dir*force,ForceMode2D.Impulse);
            rb.AddForce(dir * (force),ForceMode2D.Impulse);
        }
    }
}
