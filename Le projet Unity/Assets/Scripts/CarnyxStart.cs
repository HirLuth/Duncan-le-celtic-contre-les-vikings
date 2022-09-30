using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class CarnyxStart : MonoBehaviour
{
    private bool gotCarnyx = true;
    public float timerCooldown;
    public GameObject nuageCornyx;
    [SerializeField] private Armes armes;
    [SerializeField] private List<int> projectilesPerLevel;
    [SerializeField] private List<float> damagePerLevel;
    [SerializeField] private List<float> coolDownPerLevel;
    [SerializeField] private List<float> sizePerLevel;
    [SerializeField] private List<float> timeEffectPerLevel;


    void Update()
    {
        if(gotCarnyx)
        {
            timerCooldown += Time.deltaTime;

            if (timerCooldown >= armes.coolDown*coolDownPerLevel[armes.level])
            {
                timerCooldown = 0;
                LaunchProjectile();
            }
        }
    }

    void LaunchProjectile()
    {
        for (int i = 0; i < armes.numberOfProjectile*projectilesPerLevel[armes.level]; i++)
        {
            GameObject nuage = Instantiate(nuageCornyx, CharacterController.instance.transform.position ,Quaternion.Euler(0,0,0));
            //GameObject nuage = Instantiate(nuageCornyx,new Vector2(0,0) ,Quaternion.Euler(0,0,0));
            nuage.transform.localScale *= sizePerLevel[armes.level];
            nuage.GetComponent<CornyxProjDepla>().damage = Mathf.RoundToInt(armes.damage*damagePerLevel[armes.level]*(1+ExpManager.instance.level/(ExpManager.instance.scalingWithLevel*3)));
            nuage.GetComponent<CornyxProjDepla>().timeToDestroy = armes.timeOfTheEffect*timeEffectPerLevel[armes.level];
        }
    }
}
