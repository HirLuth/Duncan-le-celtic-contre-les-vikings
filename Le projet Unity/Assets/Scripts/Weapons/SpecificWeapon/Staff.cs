using System.Collections.Generic;
using UnityEngine;

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
        private float timer;
        
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= armes.coolDown*coolDownPerLevel[armes.level])
            {
                SpawnRoots();
                timer = 0;
            }
        }

        private void SpawnRoots()
        {
            for (int i = 0; i < armes.numberOfProjectile*projectilesPerLevel[armes.level]; i++)
            {
                GameObject currentRoot = Instantiate(projectile);
                currentRoot.transform.position = GetPosition();
                Root currentRootScript = currentRoot.GetComponent<Root>();
                currentRootScript.size = armes.projectileSize*sizePerLevel[armes.level];
                currentRootScript.damage = Mathf.RoundToInt(armes.damage*damagePerLevel[armes.level]);
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
