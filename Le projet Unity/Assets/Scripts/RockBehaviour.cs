using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    public int damage;
    public float timeToDestroy;
    public GameObject textDamagePlayer;
    public GameObject player;

    private void Start()
    {
        Destroy(gameObject,timeToDestroy);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            CharacterController.instance.TakeDamage(damage);
            
            GameObject text = Instantiate(textDamagePlayer,
                new Vector3(player.transform.position.x, player.transform.position.y + 1, -5), Quaternion.identity);
            textDamagePlayer.GetComponentInChildren<TextMeshPro>().SetText(damage.ToString());
        }
    }
}
