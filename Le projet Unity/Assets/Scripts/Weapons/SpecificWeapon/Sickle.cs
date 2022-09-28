using System;
using Unity.Mathematics;
using UnityEngine;

namespace Weapons.SpecificWeapon
{
    public class Sickle : MonoBehaviour
    {
        [SerializeField] private Armes sickle;
        [SerializeField] private GameObject sickleProjectile;
        private float _timer;
        private float _currentThrow;
        private Vector3 _newPos;

        private void Start()
        {
            var playerTransform = CharacterController.instance.transform;
            Instantiate(sickleProjectile, playerTransform.position, quaternion.identity,playerTransform);
        }

        // private void Update()
        // {
        //     _timer += Time.deltaTime;
        //     if (_timer < _currentThrow * sickle.coolDown) return;
        //     var playerTransform = CharacterController.instance.transform;
        //     Instantiate(sickleProjectile, playerTransform.position, quaternion.identity,playerTransform);
        //     _currentThrow++;
        // }
    }
}
