using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Sword : MonoBehaviour
{

    [Header("Stats")] 
    [SerializeField] private Armes armes;
    private bool canAttack;
    public float tempsReloadHitTimer;
    public float tempsReloadHit;
    public int damage;
    public Transform closestEnnemy;
    public GameObject sword;
    public float timeToDestroy;
    public float projectileSize;
    [SerializeField] private List<float> damagePerLevel;
    [SerializeField] private List<float> coolDownPerLevel;
    [SerializeField] private List<float> sizePerLevel;
    [SerializeField] private List<float> lastingTimePerLevel;



    private void Start()
    {
        projectileSize = armes.projectileSize;
        canAttack = true;
        damage = armes.damage;
        tempsReloadHit = armes.coolDown;
        timeToDestroy = armes.timeOfTheEffect;
        

    }
    
    
    
    private void Update()
    {
        Vector2 currentPos = CharacterController.instance.transform.position;
        float minDist = Mathf.Infinity;
        closestEnnemy = null;

        foreach (GameObject monstre in ListeMonstres.instance.ennemyList)
        {
            if (monstre != null)
            {
                float distance = (Vector2.Distance(monstre.transform.position,currentPos));
                if (distance < minDist)
                {
                    minDist = distance;
                    closestEnnemy = monstre.transform;
                }
            }
        }

        if (closestEnnemy != null)
        {
            Vector2 spawnSword = new Vector2(closestEnnemy.transform.position.x - CharacterController.instance.transform.position.x,
                closestEnnemy.transform.position.y -CharacterController.instance.transform.position.y).normalized;
        }
        
        if (canAttack == true)
        {
            tempsReloadHitTimer += Time.deltaTime;

            if (tempsReloadHitTimer >= tempsReloadHit*coolDownPerLevel[armes.level] && closestEnnemy != null)
            {
                tempsReloadHitTimer = 0;
                GameObject swordObj = Instantiate(sword, new Vector3(999,99,0),Quaternion.identity);
                swordObj.transform.position = CharacterController.instance.transform.position;
                swordObj.transform.localScale *= projectileSize*sizePerLevel[armes.level];
                swordObj.GetComponent<SwordBehaviour>().damage = Mathf.RoundToInt(damage*damagePerLevel[armes.level]);
                swordObj.GetComponent<SwordBehaviour>().timeToDestroy = timeToDestroy*lastingTimePerLevel[armes.level];
                


                Vector2 dir = new Vector2(closestEnnemy.transform.position.x - swordObj.transform.position.x,
                    closestEnnemy.transform.position.y - swordObj.transform.position.y);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                swordObj.transform.localRotation = Quaternion.AngleAxis(angle,Vector3.forward);
                Destroy(swordObj,timeToDestroy);
            }
        }
    }
}