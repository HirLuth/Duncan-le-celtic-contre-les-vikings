using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public int damage;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        transform.position += direction*3;
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Monstre"))
        {
            col.GetComponent<IAMonstre1>().TakeDamage(damage);
            col.GetComponent<IAMonstre1>().DamageText(damage);
            Destroy(gameObject);
        }
    }
}
