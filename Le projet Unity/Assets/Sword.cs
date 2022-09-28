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
    public Transform closestEnnemy;
    public GameObject sword;
    public float timeToDestroy;



    private void Start()
    {
        tempsReloadHit = GetComponent<Armes>().coolDown;
        canAttack = true;
        damage = GetComponent<Armes>().damage;
        tempsReloadHit = GetComponent<Armes>().coolDown;
        timeToDestroy = GetComponent<Armes>().timeOfTheEffect;
        
    }
    
    
    
    private void Update()
    {
        Vector2 currentPos = CharacterController.instance.transform.position;
        float minDist = Mathf.Infinity;
        closestEnnemy = null;

        foreach (GameObject monstre in ListeMonstres.instance.ennemyList)
        {
            float distance = (Vector2.Distance(monstre.transform.position,currentPos));
            Debug.Log(currentPos);
            if (distance < minDist)
            {
                minDist = distance;
                closestEnnemy = monstre.transform;
            }
        }
        
        
        Vector2 spawnSword = new Vector2(closestEnnemy.transform.position.x - CharacterController.instance.transform.position.x,
            closestEnnemy.transform.position.y -CharacterController.instance.transform.position.y).normalized;
        
        
        if (canAttack == true)
        {
            tempsReloadHitTimer += Time.deltaTime;

            if (tempsReloadHitTimer >= tempsReloadHit)
            {
                tempsReloadHitTimer = 0;
                GameObject swordObj = Instantiate(sword);
                swordObj.transform.position = CharacterController.instance.transform.position;
                swordObj.GetComponent<SwordBehaviour>().damage = GetComponent<Armes>().damage;
                swordObj.GetComponent<SwordBehaviour>().timeToDestroy = GetComponent<Armes>().timeOfTheEffect;


                Vector2 dir = new Vector2(closestEnnemy.transform.position.x - swordObj.transform.position.x,
                    closestEnnemy.transform.position.y - swordObj.transform.position.y);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                swordObj.transform.localRotation = Quaternion.AngleAxis(angle,Vector3.forward);
                Destroy(swordObj,timeToDestroy);
            }
        }
    }
}