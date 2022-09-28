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
        private float timer;
        
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= armes.coolDown)
            {
                timer = 0;
                SpawnBook();
            }
        }

        private void SpawnBook()
        {
            List<float> listDegrée;
            listDegrée = CharacterController.instance.listPositionDegree.ToList();
            for (int i = 0; i < armes.numberOfProjectile; i++)
            {
                int sortedDegreeIndexInList = Random.Range(1, 8);
                GameObject currentBook = Instantiate(projectile);
                currentBook.transform.rotation = quaternion.Euler(0,0,listDegrée[sortedDegreeIndexInList]);
                listDegrée.Remove(sortedDegreeIndexInList);
                SummonedBook currentBookScriptReference = currentBook.GetComponent<SummonedBook>();
                currentBookScriptReference.damage = armes.damage;
                currentBookScriptReference.effectDuration = armes.timeOfTheEffect;
                currentBookScriptReference.paperSpeed = armes.projectileSpeed;
                currentBookScriptReference.angleOfThebookInDegree = listDegrée[sortedDegreeIndexInList];
                listDegrée.Remove(sortedDegreeIndexInList);
            }
            
        }
    }
}
