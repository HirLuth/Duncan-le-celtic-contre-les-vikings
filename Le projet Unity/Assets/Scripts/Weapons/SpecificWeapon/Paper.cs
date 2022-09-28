using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    public int damage;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        transform.position = transform.position + new Vector3(direction.x*3,direction.y*3,0);
        rb.velocity = direction * speed;
    }
}
