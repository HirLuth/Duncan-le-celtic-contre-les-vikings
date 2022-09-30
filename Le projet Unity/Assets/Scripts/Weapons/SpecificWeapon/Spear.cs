using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Spear : MonoBehaviour
{
    [SerializeField] private Armes armes;
    [SerializeField] private GameObject projectile;
    private float _timer;
    public List<int> nbOfProjectilesPerLevel;
    public List<float> damagePerLevel;
    public List<float> coolDownModifierPerLevel;
    public List<float> speedPerLevel;
    
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= armes.coolDown*coolDownModifierPerLevel[armes.level])
        {
            LaunchSpearProjectiles();
            _timer = 0;
        }
    }

    private void LaunchSpearProjectiles()
    {
        for (int i = 0; i < armes.numberOfProjectile*nbOfProjectilesPerLevel[armes.level]; i++)
        {
            GameObject javelot = Instantiate(projectile,CharacterController.instance.transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0)
                , Quaternion.Euler(0, 0,  90 - CharacterController.instance.listPositionDegree[CharacterController.instance.lookingAt]));
            
            SpearProjectile projectileScriptReference = javelot.GetComponent<SpearProjectile>();
            projectileScriptReference.speed = armes.projectileSpeed*speedPerLevel[armes.level]+Mathf.RoundToInt(ExpManager.instance.level/10);
            projectileScriptReference.damage = Mathf.RoundToInt(armes.damage*damagePerLevel[armes.level]);
            projectileScriptReference.direction = Quaternion.AngleAxis(- CharacterController.instance.listPositionDegree[CharacterController.instance.lookingAt], Vector3.forward ) * Vector3.up;
        }
        
    }
}
