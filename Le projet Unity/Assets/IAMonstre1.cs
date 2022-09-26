using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMonstre1 : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float speed;
    public bool isMoving;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 target = new Vector2(player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y);

        if (isMoving)
        {
         transform.Translate(target*speed,Space.Self);
        }
    }
}
