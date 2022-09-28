using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SickleRotationCenter : MonoBehaviour
{
    [SerializeField] private Armes sickleStat;
    private float _currentPointForAngle;
    
    private void FixedUpdate()
    {
        _currentPointForAngle+=sickleStat.projectileSpeed+10;
        transform.localRotation = Quaternion.Euler(0,0,_currentPointForAngle);
    }
}
