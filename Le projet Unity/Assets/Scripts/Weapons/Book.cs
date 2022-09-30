using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Book : MonoBehaviour
    {
        [SerializeField] private Armes armes;
        [SerializeField] private GameObject projectile;
        [SerializeField] private float sprayAngle;
        [SerializeField] private int numberOfQuarter;
        [SerializeField] private float timeBetweenPaper;
        [SerializeField] private List<int> projectilesPerLevel;
        [SerializeField] private List<float> damagePerLevel;
        [SerializeField] private List<float> cooldownPerLevel;
        [SerializeField] private List<float> speedPerLevel;
        [SerializeField] private List<float> timeEffectPerLevel;
        [SerializeField] private List<float> timeBetweenPaperPerLevel;
        private float timer;

        private void Start()
        {
            SpawnBook();
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= (armes.coolDown*cooldownPerLevel[armes.level]) + armes.timeOfTheEffect)
            {
                timer = 0;
                SpawnBook();
            }
        }

        private void SpawnBook()
        {
            List<float> listDegrée;
            listDegrée = CharacterController.instance.listPositionDegree.ToList();
            for (int i = 0; i < armes.numberOfProjectile*projectilesPerLevel[armes.level]; i++)
            {
                int sortedDegreeIndexInList = Random.Range(1, listDegrée.Count);
                GameObject currentBook = Instantiate(projectile);
                currentBook.transform.rotation = Quaternion.Euler(0,0,listDegrée[sortedDegreeIndexInList]);
                SummonedBook currentBookScriptReference = currentBook.GetComponent<SummonedBook>();
                currentBookScriptReference.damage = Mathf.RoundToInt(armes.damage*damagePerLevel[armes.level]*(1+ExpManager.instance.level/ExpManager.instance.scalingWithLevel));
                currentBookScriptReference.effectDuration = armes.timeOfTheEffect*timeEffectPerLevel[armes.level];
                currentBookScriptReference.paperSpeed = armes.projectileSpeed*speedPerLevel[armes.level];
                currentBookScriptReference.timeBetweenPaper = timeBetweenPaper * timeBetweenPaperPerLevel[armes.level];
                currentBookScriptReference.angleOfThebookInDegree = listDegrée[sortedDegreeIndexInList];
                currentBookScriptReference.sprayAngle = sprayAngle;
                currentBookScriptReference.numberOfQuarter = numberOfQuarter;
                listDegrée.RemoveAt(sortedDegreeIndexInList);
            }
            
        }
    }
}
