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
    

    void Update()
    {
        if(gotCarnyx)
        {
            timerCooldown += Time.deltaTime;

            if (timerCooldown >= GetComponent<Armes>().coolDown)
            {
                timerCooldown = 0;
              GameObject nuage = Instantiate(nuageCornyx, CharacterController.instance.transform.position ,Quaternion.Euler(0,0,0));
              //GameObject nuage = Instantiate(nuageCornyx,new Vector2(0,0) ,Quaternion.Euler(0,0,0));
                nuage.GetComponent<CornyxProjDepla>().damage = GetComponent<Armes>().damage;
                nuage.GetComponent<CornyxProjDepla>().timeToDestroy = GetComponent<Armes>().timeOfTheEffect;
            }
        }
    }
}
