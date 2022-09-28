using UnityEngine;

namespace Weapons.SpecificWeapon
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private Armes armes;
        [SerializeField] private GameObject projectile;
        private float timer;
        
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= armes.coolDown)
            {
                SpawnRoots();
                timer = 0;
            }
        }

        private void SpawnRoots()
        {
            for (int i = 0; i < armes.numberOfProjectile; i++)
            {
                GameObject currentRoot = Instantiate(projectile);
                currentRoot.transform.position = GetPosition();
                currentRoot.GetComponent<Root>().size = armes.projectileSize;
                currentRoot.GetComponent<Root>().damage = armes.damage;
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
