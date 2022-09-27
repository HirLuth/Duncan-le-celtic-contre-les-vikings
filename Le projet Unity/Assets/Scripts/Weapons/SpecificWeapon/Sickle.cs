using System;
using UnityEngine;

namespace Weapons.SpecificWeapon
{
    public class Sickle : MonoBehaviour
    {
        [SerializeField] private Armes sickle;
        [SerializeField] private GameObject sickleProjectile;
        private float _timer;
        private float _currentThrow;

        private void Start()
        {
            //Instantiate(sickleProjectile, CharacterController.instance.transform);
        }

        private void FixedUpdate()
        {
            _timer += Time.deltaTime;
            if (_timer < _currentThrow * sickle.coolDown) return;
            Instantiate(sickleProjectile, CharacterController.instance.transform);
            _currentThrow++;
        }
    }
}
