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

            if (timerCooldown >= Armes.instance.coolDown)
            {
                timerCooldown = 0;
                GameObject nuage = Instantiate(nuageCornyx, CharacterController.instance.transform.position + new Vector3(0,5,0) ,Quaternion.identity);
                nuage.GetComponent<CornyxProjDepla>().damage = Armes.instance.damage;
                nuage.GetComponent<CornyxProjDepla>().timeToDestroy = Armes.instance.timeOfTheEffect;
            }
        }
    }
}
