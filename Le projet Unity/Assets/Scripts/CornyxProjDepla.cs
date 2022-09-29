using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject armeAssociee;
    
    [Header("Nuage Poison")]
    public float tempsReloadHitTimer;
    public float tempsReloadHit;
    private bool stopAttack;
    private bool startAttack;
    public int nbDOT;
     public int damage;
    public float timeToDestroy;
    
    void Start()
    {
        

        rb = gameObject.GetComponent<Rigidbody2D>();
        
        Vector2 explode = new Vector2(Random.Range(-force, force), Random.Range(-force, force));
        rb.AddForce(explode, ForceMode2D.Impulse);
        rb.drag = deceleration;
    }

    void Update()
    {
        rb.velocity -= rb.velocity * deceleration;
            Destroy(gameObject,timeToDestroy);
    }

   

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monstre")
        {
            stopAttack = false;
            for (int i = 0; i < nbDOT; i++)
            {
                if (tempsReloadHitTimer <= nbDOT && stopAttack == false)
                {
                    tempsReloadHitTimer += Time.deltaTime;
                }

                if (tempsReloadHitTimer > tempsReloadHit && col.gameObject.tag == "Monstre")
                {
                    col.GetComponent<IAMonstre1>().TakeDamage(damage);
                    col.GetComponent<IAMonstre1>().DamageText(damage);
                    tempsReloadHitTimer = 0;
                }
                
                if (tempsReloadHitTimer > tempsReloadHit && col.gameObject.CompareTag("Boss"))
                {
                    col.gameObject.GetComponent<IABoss>().TakeDamage(damage);
                    col.gameObject.GetComponent<IABoss>().DamageText(damage);
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
