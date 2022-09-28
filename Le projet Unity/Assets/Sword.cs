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
    public GameObject player;
    private float range;

    [Header("Combo")] 
    public float comboDelay;
    private int comboStep ;


    private void Start()
    {
        canAttack = true;
        damage = GetComponent<Armes>().damage;
        tempsReloadHit = GetComponent<Armes>().timeOfTheEffect;
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
        
        
        Vector2 spawnSword = new Vector2(CharacterController.instance.transform.position.x - closestEnnemy.transform.position.x,
            CharacterController.instance.transform.position.y - closestEnnemy.transform.position.y);
        
        
        if (canAttack == true)
        {
            tempsReloadHitTimer += Time.deltaTime;

            if (tempsReloadHitTimer >= tempsReloadHit)
            {
                tempsReloadHitTimer = 0;
                comboStep++;
                
                if (comboStep == 1)
                {
                    GameObject swordObj = Instantiate(sword,spawnSword, Quaternion.Euler(0f,0f,0f));
                    //swordObj.transform.RotateAround(player.transform.position, swordObj.transform.position, 2);
                }
            
                if (comboStep == 2)
                {
                    //Attaque 2
                }

                if (comboStep == 3)
                {
                    comboStep = 0;
                }
            }
        }
    }


    void Combo1()
    {
        
    }
}
