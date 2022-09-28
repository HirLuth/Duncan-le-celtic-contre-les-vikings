using System;
using UnityEngine;

namespace Weapons
{
    public class SpearProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        public int damage;
        public float speed;
        public Vector2 direction;

        private void Start()
        {
            rb.velocity = direction*speed;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Monstre"))
            {
                col.GetComponent<IAMonstre1>().TakeDamage(damage);
                col.GetComponent<IAMonstre1>().DamageText(damage);
                Destroy(gameObject);
            }
            
        }
    }
}
