using System;
using UnityEngine;

namespace Weapons
{
    public class SpearProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        public int damage;
        public int speed;

        private void Start()
        {
            
        }
    }
}
