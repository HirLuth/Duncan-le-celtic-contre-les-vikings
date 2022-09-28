using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SickleRotationCenter : MonoBehaviour
{
    public float projectileSpeed;
    private float _currentPointForAngle;
    
    private void FixedUpdate()
    {
        _currentPointForAngle+=projectileSpeed;
        transform.localRotation = Quaternion.Euler(0,0,_currentPointForAngle);
    }
}
