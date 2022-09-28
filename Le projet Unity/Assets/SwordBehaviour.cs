using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SwordBehaviour : MonoBehaviour
{
    private bool stopAttack;
    public int damage;
    public float timeToDestroy;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.tag);
        if (col.gameObject.CompareTag("Monstre"))
        {
            col.gameObject.GetComponent<IAMonstre1>().TakeDamage(damage);
            col.gameObject.GetComponent<IAMonstre1>().DamageText(damage);
        }
    }
}

   