using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons.SpecificWeapon
{
    public class SickleProjectile : MonoBehaviour
    {
        private float _currentPointForAngle;
    
        private void FixedUpdate()
        {
            _currentPointForAngle+=0.04f;
            transform.Translate(new Vector3(Mathf.Cos(_currentPointForAngle),Mathf.Sin(_currentPointForAngle),0),Space.Self);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log("Aouch");
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }

            if (other.CompareTag("Monstre"))
            {
                Destroy(gameObject);
            }
        }
    }
}
