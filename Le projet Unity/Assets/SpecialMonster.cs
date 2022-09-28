using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpecialMonster : MonoBehaviour
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
    public static SpecialMonster instance;
    
        
    [Header("Defence")] 
    public int health;
   
    
    public void Awake()
    {
       
        
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
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
                Instantiate(textDamage, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
                textDamage.GetComponentInChildren<TextMeshPro>().SetText(Damages.ToString());
                timerDmg = 0;
            }
        }
        
        
        if (isTouching == false)
        {
            CharacterController.isTakingDamage = false;
        }

        if (health == 0)
        {
            ExpManager.instance.CreateExp(transform.position,Random.Range(1,3));
            Destroy(gameObject);
            ListeMonstres.instance.ennemyList.Remove(gameObject);
        }
    }
    
    public void DamageTextPlayer(int damageAmount)
    {
        Instantiate(textDamage, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
        textDamage.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());
    }
    
    public void DamageText(int damageAmount)
    {
        Instantiate(textDamage, new Vector3(transform.position.x,transform.position.y + 1,-5), Quaternion.identity);
        textDamage.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());
    }
    
    

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
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
}
