using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons.WeaponEffect
{
    public class DoDamage : MonoBehaviour
    {
        [SerializeField] private bool destroyedOnDamage;

        [SerializeField] private Armes armes;

        private void OnTriggerStay2D(Collider2D other)
        {
            //other.GetComponent<Damage>().GetDamage(armes.damage);
            if (destroyedOnDamage)
            {
                Destroy(gameObject);
            }
        }
    }
}
