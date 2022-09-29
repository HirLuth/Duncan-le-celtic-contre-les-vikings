using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    public int damage;
    public float timeToDestroy;

    private void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            CharacterController.instance.TakeDamage(damage);
        }
    }
}
