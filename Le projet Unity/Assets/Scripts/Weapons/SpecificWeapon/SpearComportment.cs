using System;
using UnityEngine;

namespace Weapons.SpecificWeapon
{
    public class SpearComportment : ProjectileComportment
    {
        [SerializeField] private Transform playerOrientation;
        [SerializeField] private Armes javelot;
        private Vector2 _direction;
        private float _speed;
        private void Start()
        {
            var position = playerOrientation.position;
            _direction.Set(position.x,position.y);
            _direction.Normalize();
            _speed = javelot.projectileSpeed;
        }

        private void Update()
        {
            transform.Translate(_direction*_speed);
        }
    }
}
