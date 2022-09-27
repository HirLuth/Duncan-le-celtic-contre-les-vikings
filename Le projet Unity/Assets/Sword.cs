using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Sword : MonoBehaviour
{

    [Header("Stats")]
    private bool canAttack;
    public float tempsReloadHitTimer;
    public float tempsReloadHit;
    public float damage;
    public Transform closestEnnemi;

    [Header("Combo")] 
    public float comboDelay;
    private int comboStep ;


    private void Start()
    {
        damage = GetComponent<Armes>().damage;
        tempsReloadHit = GetComponent<Armes>().timeOfTheEffect;
    }

    Transform GetClosestEnemy (Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
                bestTarget = closestEnnemi;
            }
        }
        return bestTarget;
    }
    
    
    
    private void Update()
    {
        if (canAttack == true)
        {
            tempsReloadHit += Time.deltaTime;

            if (tempsReloadHitTimer >= tempsReloadHit)
            {
                tempsReloadHit = 0;
                comboStep++;
            }

            if (comboStep == 1)
            {
                //Attaque 1
            }
            
            if (comboStep == 2)
            {
                //Attaque 2
            }
            
            if (comboStep == 3)
            {
                //Attaque 3
            }
        }
    }
}
