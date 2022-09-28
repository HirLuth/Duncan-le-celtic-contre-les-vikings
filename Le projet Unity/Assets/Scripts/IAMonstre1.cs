using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class IAMonstre1 : MonoBehaviour
{
    [Header("Attaque")]
    public GameObject player;
    public float speed;
    public float colldownDmg;
    public int Damages;
    public bool isMoving;
    private float timerDmg;
    private bool isTouching;
    private Rigidbody2D rb;
    public GameObject textDamage;
    public bool specialMonster;
    public GameObject coffre;

    //public static IAMonstre1 instance; 
        
    [Header("Defence")] 
    public int health;
   
    
    public void Awake()
    {
        
        
        // if(instance == null)
        // {
        //     instance = this;
        // }
    }

    private void Start()
    {
        player = CharacterController.instance.gameObject;
        ListeMonstres.instance.ennemyList.Add(gameObject);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 target = new Vector2(player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y);

        if (isMoving)
        {
         transform.Translate(target.normalized*speed*Time.deltaTime,Space.Self);
        }

        if (isTouching)
        {
            timerDmg += Time.deltaTime;
            CharacterController.isTakingDamage = true;
            
            if (timerDmg > colldownDmg)
            {
                CharacterController.instance.TakeDamage(Damages);
                GameObject text = Instantiate(textDamage, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
                //text.GetComponent<TextMeshPro>().color = Color.red;
                textDamage.GetComponentInChildren<TextMeshPro>().SetText(Damages.ToString());
                timerDmg = 0;
            }
        }
        
        
        if (isTouching == false)
        {
            CharacterController.isTakingDamage = false;
        }

        if (health <= 0)
        {
            if (specialMonster)
            {
                DropCoffre();
                ExpManager.instance.CreateExp(transform.position,Random.Range(10,15));
            }
            ExpManager.instance.CreateExp(transform.position,Random.Range(1,3));
            Destroy(gameObject);
            ListeMonstres.instance.ennemyList.Remove(gameObject);
        }
    }
    
    public void DamageText(int damageAmount)
    {
        Instantiate(textDamage, new Vector3(transform.position.x,transform.position.y + 1,-5), Quaternion.identity);
        textDamage.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());
    }
    
    

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health == 0)
        {
            ExpManager.instance.CreateExp(transform.position,Random.Range(1,3));
            ListeMonstres.instance.ennemyList.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isTouching = false;
            timerDmg = 0;
        }
    }
    
    void DropCoffre()
    {
        GameObject coffreObj = Instantiate(coffre,transform.position,Quaternion.identity);
    }
} 

