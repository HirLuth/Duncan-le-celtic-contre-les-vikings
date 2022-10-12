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
    public GameObject textDamagePlayerBlue;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        Destroy(gameObject,timeToDestroy);
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            CharacterController.instance.TakeDamage(damage);
            
            if (CharacterController.instance.shieldActivated)
            {
                textDamagePlayerBlue.GetComponentInChildren<TextMeshPro>().SetText(damage.ToString());
                GameObject textBlue = Instantiate(textDamagePlayerBlue, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
            }
            else
            {
                textDamagePlayer.GetComponentInChildren<TextMeshPro>().SetText(damage.ToString());
                GameObject text = Instantiate(textDamagePlayer, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
            }
        }
    }
}
