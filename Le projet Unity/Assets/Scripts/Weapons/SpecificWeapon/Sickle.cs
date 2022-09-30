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
        [SerializeField] private int hitBeforeDestruction;
        [SerializeField] private List<float> damagePerLevel;
        [SerializeField] private List<float> coolDownPerLevel;
        [SerializeField] private List<float> sizePerLevel;
        [SerializeField] private List<float> speedPerLevel;
        [SerializeField] private List<float> timeEffectPerLevel;
        [SerializeField] private List<int> hitBeforeDestructionPerLevel;

        private float _timer;
        private int _currentThrow;
        private Vector3 _newPos;
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer < sickleStat.coolDown*coolDownPerLevel[sickleStat.level]) return;
            _timer = 0;
            var playerTransform = CharacterController.instance.transform;
            var sickle = Instantiate(sickleProjectile, playerTransform.position, quaternion.identity);
            sickle.transform.localScale *= sizePerLevel[sickleStat.level];
            var centerScriptReference = sickle.GetComponent<SickleRotationCenter>();
            centerScriptReference.projectileSpeed = sickleStat.projectileSpeed*speedPerLevel[sickleStat.level];
            var projectileScriptReference = centerScriptReference.GetComponentInChildren<SickleProjectile>();
            projectileScriptReference.damage = Mathf.RoundToInt(sickleStat.damage*damagePerLevel[sickleStat.level]*(1+ExpManager.instance.level/ExpManager.instance.scalingWithLevel));
            projectileScriptReference.timeOfTheEffect = sickleStat.timeOfTheEffect*timeEffectPerLevel[sickleStat.level];
            projectileScriptReference.sickleMaxRange = sickleMaxRange;
            projectileScriptReference.timeToGetToMaxRange = timeToGetToMaxRange;
            projectileScriptReference.hitBeforeDestruction = hitBeforeDestruction*hitBeforeDestructionPerLevel[sickleStat.level];
        }
    }
}
