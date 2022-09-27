using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Spear : MonoBehaviour
{
    [SerializeField] private Armes armes;
    [SerializeField] private GameObject projectile;
    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer == armes.coolDown)
        {
            LaunchSpearProjectiles();
            timer = 0;
        }
    }

    private void LaunchSpearProjectiles()
    {
        GameObject javelot = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, 90));
    }
}
