using System;
using System.Collections.Generic;
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
        [SerializeField] private List<float> damagePerLevel;
        [SerializeField] private List<float> coolDownPerLevel;
        [SerializeField] private List<float> sizePerLevel;
        [SerializeField] private List<float> speedPerLevel;
        [SerializeField] private List<float> timeEffectPerLevel;

        private float _timer;
        private float _currentThrow;
        private Vector3 _newPos;
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer < _currentThrow * sickleStat.coolDown*coolDownPerLevel[sickleStat.level]) return;
            var playerTransform = CharacterController.instance.transform;
            var sickle = Instantiate(sickleProjectile, playerTransform.position, quaternion.identity);
            sickle.transform.localScale *= sizePerLevel[sickleStat.level];
            var centerScriptReference = sickle.GetComponent<SickleRotationCenter>();
            centerScriptReference.projectileSpeed = sickleStat.projectileSpeed*speedPerLevel[sickleStat.level];
            var projectileScriptReference = centerScriptReference.GetComponentInChildren<SickleProjectile>();
            projectileScriptReference.damage = Mathf.RoundToInt(sickleStat.damage*damagePerLevel[sickleStat.level]);
            projectileScriptReference.timeOfTheEffect = sickleStat.timeOfTheEffect*timeEffectPerLevel[sickleStat.level];
            projectileScriptReference.sickleMaxRange = sickleMaxRange;
            projectileScriptReference.timeToGetToMaxRange = timeToGetToMaxRange;
            _currentThrow++;
        }
    }
}
