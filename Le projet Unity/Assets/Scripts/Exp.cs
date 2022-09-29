using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Exp : MonoBehaviour
{
    public static Exp instance;
    
    public Vector3 playerPos;
    [SerializeField] private float forceProjection = 3f;
    [SerializeField] private float forceAttract = 3f;
    [SerializeField] private float timer = 1;
    [SerializeField] private float deceleration = 0.3f;
    [SerializeField] private int xpValue;
    private float timeStamp;
    public Rigidbody2D rb;
    private bool isAtracted;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       
        Vector2 explode = new Vector2(Random.Range(-forceProjection, forceProjection), Random.Range(-forceProjection, forceProjection));
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
            rb.velocity -= rb.velocity * 0.01f;
            timer -= Time.deltaTime;
        }

        if (isAtracted)
        {
            //rb.AddForce(dir * (force),ForceMode2D.Impulse);
            rb.velocity = new Vector2(dirNormalised.x, dirNormalised.y) * forceAttract * (Time.time / timeStamp);
        }
        
       
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector3 dir = playerPos - transform.position;
        Vector3 dirNormalised = dir.normalized;
        
        if (col.gameObject.CompareTag("AttractExp"))
        {
            if (timer <= 0)
            {
                timeStamp = Time.time;
                isAtracted = true;
            }
        }
        
        if (col.gameObject.CompareTag("GetExp"))
        {
            ExpManager.instance.CollectExp(gameObject, xpValue);  
        }
    }
}
