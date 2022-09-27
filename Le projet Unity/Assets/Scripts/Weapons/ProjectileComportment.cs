using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class ProjectileComportment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Monstre"))
        {
            Destroy(this.gameObject);
        }
    }
}
