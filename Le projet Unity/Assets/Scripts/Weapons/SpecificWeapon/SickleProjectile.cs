using UnityEngine;

namespace Weapons.SpecificWeapon
{
    public class SickleProjectile : MonoBehaviour
    {
        [SerializeField] private GameObject sickleCenter;
        [SerializeField] private int numberOfLoop;
        public float sickleMaxRange;
        public float timeOfTheEffect;
        public float timeToGetToMaxRange;
        public int damage;
        private float _timer;
        private float _timerBack;
        private bool _tweenActive;
        
        private void FixedUpdate()
        {
            _timer += Time.deltaTime;
            if (_timer < timeToGetToMaxRange)
            {
                transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.right*sickleMaxRange, _timer / timeToGetToMaxRange);
            }
            if (_timer < timeOfTheEffect) return;
            Destroy(sickleCenter);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Monstre"))
            {
                other.GetComponent<IAMonstre1>().TakeDamage(damage);
                other.GetComponent<IAMonstre1>().DamageText(damage);
            }
            
            
            if (other.gameObject.CompareTag("Boss"))
            {
                other.gameObject.GetComponent<IABoss>().TakeDamage(damage);
                other.gameObject.GetComponent<IABoss>().DamageText(damage);
            }
        }
    }
}
