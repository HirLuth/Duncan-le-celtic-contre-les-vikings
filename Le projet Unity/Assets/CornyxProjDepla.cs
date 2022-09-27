using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;
using Random = UnityEngine.Random;

public class CornyxProjDepla : MonoBehaviour
{
    [Header("Déplacements")] 
    public float force;
    private Rigidbody2D rb;
    public float deceleration;
    
    [Header("Nuage Poison")]
    private float tempsReloadHitTimer;
    private float tempsReloadHit;
    private bool stopAttack;
    private bool startAttack;
    public int nbDOT;
    [HideInInspector] public float damage;
    [HideInInspector] public float timeToDestroy;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        
        Vector2 explode = new Vector2(Random.Range(-force, force), Random.Range(-force, force));
        rb.AddForce(explode, ForceMode2D.Impulse);
        rb.drag = deceleration;
    }

    void Update()
    {
        if (rb.velocity.x > 0 && rb.velocity.y > 0)
        {
            rb.velocity -= rb.velocity * 0.4f;
        }
        
        Destroy(gameObject,timeToDestroy);
    }

   

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monstre")
        {
            Debug.Log("samère");
            stopAttack = false;
            for (int i = 0; i < nbDOT; i++)
            {
                if (tempsReloadHitTimer <= nbDOT && stopAttack == false)
                {
                    tempsReloadHitTimer += Time.deltaTime;
                }

                if (tempsReloadHitTimer > tempsReloadHit && col.gameObject.tag == "Monstre")
                {
                    Debug.Log("touché");
                    col.GetComponent<IAMonstre1>().TakeDamage(Armes.instance.damage);
                    col.GetComponent<IAMonstre1>().DamageText(Armes.instance.damage);
                    tempsReloadHitTimer = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monstre")
        {
            stopAttack = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monstre")
        {
            StartCoroutine(stopAttackTimer());
            tempsReloadHitTimer = 0;
        }
    }

    IEnumerator stopAttackTimer()
    {
        stopAttack = true;
        yield return new WaitForSeconds(0.1f);
        stopAttack = false;
    }
}
