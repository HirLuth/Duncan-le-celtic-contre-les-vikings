using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FirballBehaviour : MonoBehaviour
{
    public float speed;
    public int damage;
    private Rigidbody2D rb;
    public GameObject player;
    public GameObject textDamagePlayer;
    public Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        direction.Normalize();
        Destroy(gameObject,5f);
    }

   
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + direction.x * speed, transform.position.y + direction.y * speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player)
        {
            CharacterController.instance.TakeDamage(damage);
            GameObject text = Instantiate(textDamagePlayer, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
            textDamagePlayer.GetComponentInChildren<TextMeshPro>().SetText(damage.ToString());
        }
    }
}
