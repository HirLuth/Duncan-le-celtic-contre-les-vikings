using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons.SpecificWeapon
{
    public class SickleProjectile : MonoBehaviour
    {
        [SerializeField] private Armes sickleStat;
        [SerializeField] private GameObject sickleCenter;
        [SerializeField] private int numberOfLoop;
        [SerializeField] private float sickleMaxRange;
        private float _timer;

        private void Start()
        {
            Debug.Log(sickleStat.timeOfTheEffect);
        }

        private void FixedUpdate()
        {
            _timer += Time.deltaTime;
            Debug.Log(sickleStat.timeOfTheEffect);
            transform.localPosition = new Vector3(0,sickleMaxRange*Mathf.Cos(Mathf.PI*_timer*numberOfLoop/sickleStat.timeOfTheEffect/2));
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log("Aouch");
            if (other.CompareTag("Player"))
            {
                if (_timer < sickleStat.timeOfTheEffect) return;
                Destroy(sickleCenter);
            }

            if (other.CompareTag("Monstre"))
            {
                other.GetComponent<IAMonstre1>().TakeDamage(sickleStat.damage);
                other.GetComponent<IAMonstre1>().DamageText(sickleStat.damage);
            }
        }
    }
}
