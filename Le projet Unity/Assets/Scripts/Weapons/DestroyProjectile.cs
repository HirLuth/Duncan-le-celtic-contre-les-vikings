using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    [SerializeField] private int timeBeforeDestruction;
    private float _timer;
    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer < timeBeforeDestruction) return;
        Destroy(gameObject);
    }
}
