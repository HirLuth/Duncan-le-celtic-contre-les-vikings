using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons.WeaponEffect
{
    public class DoDamage : MonoBehaviour
    {
        [SerializeField] private bool destroyedOnDamage;

        [SerializeField] private Armes armes;

        private void OnCollisionEnter2D(Collision2D collision)
        {
          IAMonstre1.instance.TakeDamage(Armes.instance.damage);
            if (destroyedOnDamage)
            {
                Destroy(gameObject);
            }
        }
    }
}
