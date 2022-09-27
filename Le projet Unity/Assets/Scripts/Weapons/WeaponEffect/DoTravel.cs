using System;
using UnityEngine;

namespace Weapons.WeaponEffect
{
    public class DoTravel : MonoBehaviour
    {
        public static DoTravel instance;
        
        private Vector3 _startPos;
        private Vector3 _direction;
        public float _timer;
        private float _cameraHalfHeight;
        private float _cameraHalfWidth;
        private float _lengthFromCameraCornerToCameraCenter;
        private Vector2 _vectorFromProjectileToCameraCenter;
        private enum Path
        {
            Straight,
            Sickle,
            EvolvedHorn
        }
        [SerializeField] private Path path;
        public Camera ifUntimedPlayerCamera;
        [SerializeField] private Armes armes;
        [SerializeField] private bool timed;

        public void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            _startPos = transform.position;
            if (timed) return;
            _cameraHalfHeight = ifUntimedPlayerCamera.orthographicSize;
            _cameraHalfWidth = ifUntimedPlayerCamera.aspect * _cameraHalfHeight;
            _lengthFromCameraCornerToCameraCenter = new Vector2(_cameraHalfWidth, _cameraHalfHeight).sqrMagnitude;
        }

        public void SetDirection(float angleInRadian)
        {
            _direction.Set(Mathf.Cos(angleInRadian)*armes.projectileSpeed,Mathf.Sin(angleInRadian)*armes.projectileSpeed,0);
        }

        private void Update()
        {
            switch (path)
            {
                case Path.Straight:
                    transform.Translate(_direction);
                    break;
                case Path.Sickle:
                    transform.Translate(_direction);
                    break;
                case Path.EvolvedHorn:
                    transform.Translate(_direction);
                    break;
            }

            if (timed)
            {
                DestroyWithTime();
            }
            else
            {
                DestroyOutOfCamera();
            }
        }

        private void DestroyWithTime()
        {
            _timer += Time.deltaTime;
            if (_timer>armes.timeOfTheEffect)
            {
                Destroy(gameObject);
            }
        }

        private void DestroyOutOfCamera()
        {
            var thisPosition = transform.position;
            var cameraPosition = ifUntimedPlayerCamera.transform.position;
            _vectorFromProjectileToCameraCenter.Set(thisPosition.x-cameraPosition.x,thisPosition.y-cameraPosition.y);
            if (_vectorFromProjectileToCameraCenter.SqrMagnitude() < _lengthFromCameraCornerToCameraCenter) return;
            Destroy(gameObject);
        }
    }
}
