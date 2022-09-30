using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Halo : MonoBehaviour
{
   [Header("Nuage Poison")]
   public float tempsReloadHitTimer;
   public float tempsReloadHit;
   private bool stopAttack;
   private bool startAttack;
   public int nbDOT;
   public int damage;
   public float timeToDestroy;
   public GameObject textDamagePlayer;
   public GameObject player;

   private void Start()
   {
      player = GameObject.FindWithTag("Player");
   }

   private void OnTriggerStay2D(Collider2D col)
   {
      if (col.gameObject.tag == "Player")
      {
         stopAttack = false;
         for (int i = 0; i < nbDOT; i++)
         {
            if (tempsReloadHitTimer <= nbDOT && stopAttack == false)
            {
               tempsReloadHitTimer += Time.deltaTime;
            }

            if (tempsReloadHitTimer > tempsReloadHit && col.gameObject.tag == "Monstre")
            {
               col.GetComponent<CharacterController>().TakeDamage(damage);
               GameObject text = Instantiate(textDamagePlayer, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
               text.GetComponentInChildren<TextMeshPro>().SetText(damage.ToString());
               tempsReloadHitTimer = 0;
            }
            
         }
      }
   }
    

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.tag == "Player")
      {
         stopAttack = false;
      }
        
      if (col.gameObject.tag == "Player")
      {
         stopAttack = false;
      }
   }
}
