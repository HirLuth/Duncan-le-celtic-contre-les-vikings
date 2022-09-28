using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Spear : MonoBehaviour
{
    [SerializeField] private Armes armes;
    [SerializeField] private GameObject projectile;
    private float _timer;
    void Update()
    {
        _timer += Time.deltaTime;
        Debug.Log(_timer);
        if (_timer >= armes.coolDown)
        {
            Debug.Log(42);
            LaunchSpearProjectiles();
            _timer = 0;
        }
    }

    private void LaunchSpearProjectiles()
    {
        GameObject javelot = Instantiate(projectile,CharacterController.instance.transform.position, Quaternion.Euler(0, 0,  90 - CharacterController.instance.listPositionDegree[CharacterController.instance.lookingAt]));
        SpearProjectile projectileScriptReference = javelot.GetComponent<SpearProjectile>();
        projectileScriptReference.speed = armes.projectileSpeed;
        projectileScriptReference.damage = armes.damage;
        projectileScriptReference.direction = Quaternion.AngleAxis(- CharacterController.instance.listPositionDegree[CharacterController.instance.lookingAt], Vector3.forward ) * Vector3.up;
    }
}
