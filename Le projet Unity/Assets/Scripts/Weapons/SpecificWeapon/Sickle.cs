using System;
using Unity.Mathematics;
using UnityEngine;

namespace Weapons.SpecificWeapon
{
    public class Sickle : MonoBehaviour
    {
        [SerializeField] private Armes sickleStat;
        [SerializeField] private GameObject sickleProjectile;
        [SerializeField] private float sickleMaxRange;
        [SerializeField] private float timeToGetToMaxRange;
        private float _timer;
        private float _currentThrow;
        private Vector3 _newPos;
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer < _currentThrow * sickleStat.coolDown) return;
            var playerTransform = CharacterController.instance.transform;
            var sickle = Instantiate(sickleProjectile, playerTransform.position, quaternion.identity);
            var centerScriptReference = sickle.GetComponent<SickleRotationCenter>();
            centerScriptReference.projectileSpeed = sickleStat.projectileSpeed;
            var projectileScriptReference = centerScriptReference.GetComponentInChildren<SickleProjectile>();
            projectileScriptReference.damage = sickleStat.damage;
            projectileScriptReference.timeOfTheEffect = sickleStat.timeOfTheEffect;
            projectileScriptReference.sickleMaxRange = sickleMaxRange;
            projectileScriptReference.timeToGetToMaxRange = timeToGetToMaxRange;
            _currentThrow++;
        }
    }
}
