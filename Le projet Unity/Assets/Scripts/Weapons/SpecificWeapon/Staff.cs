using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapon
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private Armes armes;
        [SerializeField] private GameObject projectile;
        [SerializeField] private List<int> projectilesPerLevel;
        [SerializeField] private List<float> damagePerLevel;
        [SerializeField] private List<float> coolDownPerLevel;
        [SerializeField] private List<float> sizePerLevel;
        [SerializeField] private List<float> timeEffectPerLevel;
        [SerializeField] public float coolDown;
        private float timer;

        private void Start()
        {
            coolDown = armes.coolDown;
        }

        void Update()
        {
            /*if (IABoss.instance.bossFight)
            {
                coolDown = coolDownBoss;
            }*/
            
            timer += Time.deltaTime;
            if (timer >= coolDown*coolDownPerLevel[armes.level])
            {
                SpawnRoots();
                timer = 0;
            }
        }

        private void SpawnRoots()
        {
            int nombreprojectiles;
            if (IABoss.instance.bossFight)
            {
                nombreprojectiles = 1;
            }
            else
            {
                nombreprojectiles = armes.numberOfProjectile * projectilesPerLevel[armes.level];
            }
            for (int i = 0; i < nombreprojectiles; i++)
            {
                Vector2 position = GetPosition();
                GameObject currentRoot = Instantiate(projectile);
                currentRoot.transform.position = position;
                Root currentRootScript = currentRoot.GetComponent<Root>();
                currentRootScript.size = armes.projectileSize*sizePerLevel[armes.level];
                currentRootScript.damage = Mathf.RoundToInt(armes.damage*damagePerLevel[armes.level]*(1+ExpManager.instance.level/ExpManager.instance.scalingWithLevel));
                currentRootScript.timeToDisapear = armes.timeOfTheEffect * timeEffectPerLevel[armes.level];
            }
        }

        private Vector3 GetPosition()
        {
            int selectedEnemyNumber = Random.Range(0, ListeMonstres.instance.ennemyList.Count);

            for (int i = 0; i < 5; i++)
            {
                if (Mathf.Abs(ListeMonstres.instance.ennemyList[selectedEnemyNumber].transform.position.y - CharacterController.instance.transform.position.y) < CameraController.instance.camera.orthographicSize 
                    && Mathf.Abs(ListeMonstres.instance.ennemyList[selectedEnemyNumber].transform.position.x - CharacterController.instance.transform.position.x) < CameraController.instance.camera.orthographicSize * CameraController.instance.camera.aspect)
                {
                    return ListeMonstres.instance.ennemyList[selectedEnemyNumber].transform.position;
                }

                if (selectedEnemyNumber == ListeMonstres.instance.ennemyList.Count -1)
                {
                    selectedEnemyNumber = 0;
                }
                else
                {
                    selectedEnemyNumber += 1;
                }
            }

            return ListeMonstres.instance.ennemyList[selectedEnemyNumber].transform.position;
        }
    }
}
