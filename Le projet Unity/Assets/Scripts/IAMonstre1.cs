using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class IAMonstre1 : MonoBehaviour
{
    [SerializeField] private float outOfBoundOffSet;
    [SerializeField] private int monsterXp;
    [SerializeField] private int specialMonsterXp;
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
    public GameObject textDamagePlayer;
    public GameObject textDamagePlayerBlue;

    private float _cameraHalfHeight;
    private float _cameraHalfWidth;
    private bool _tpOnCooldown;
    private bool isInExpRange;

    //public static IAMonstre1 instance; 
        
    [Header("Defence")] 
    public int health;

    private void Start()
    {
        player = CharacterController.instance.gameObject;
        ListeMonstres.instance.ennemyList.Add(gameObject);
        rb = gameObject.GetComponent<Rigidbody2D>();
        var refCamera = CameraController.instance.camera;
        _cameraHalfHeight = refCamera.orthographicSize;
        _cameraHalfWidth = refCamera.aspect * refCamera.orthographicSize;
    }

    private void Update()
    {
        CheckIfInBound();

        if (specialMonster == false)
        {
            if (CharacterController.instance.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            }
            else
            {
                transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
            }
        }
        else
        {
            if (CharacterController.instance.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(0.8f,0.8f,0.8f);
            }
            else
            {
                transform.localScale = new Vector3(-0.8f,0.8f,0.8f);
            }
        }
       
        
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
                if (CharacterController.instance.shieldActivated)
                {
                    textDamagePlayerBlue.GetComponentInChildren<TextMeshPro>().SetText(Damages.ToString());
                    GameObject textBlue = Instantiate(textDamagePlayerBlue, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
                }
                else
                {
                    textDamagePlayer.GetComponentInChildren<TextMeshPro>().SetText(Damages.ToString());
                    GameObject text = Instantiate(textDamagePlayer, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
                }
             
                timerDmg = 0;
            }
        }
        
        
        if (isTouching == false)
        {
            CharacterController.isTakingDamage = false;
        }
    }
    
    public void DamageText(int damageAmount)
    {
        textDamage.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());
        Instantiate(textDamage, new Vector3(transform.position.x,transform.position.y + 1,-5), Quaternion.identity);
    }
    
    

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            if (specialMonster)
            {
                ListeMonstres.instance.AddScore(50);
                ExpManager.instance.CreateExp(transform.position,specialMonsterXp,isInExpRange);
                DropCoffre();
            }
            else
            {
                ListeMonstres.instance.AddScore(10);
                ExpManager.instance.CreateExp(transform.position,monsterXp,isInExpRange);
            }
            ListeMonstres.instance.ennemyList.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
        if (col.gameObject.CompareTag("AttractExp"))
        {
            isInExpRange = true;
        }
        
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isTouching = false;
            timerDmg = 0;
        }
        if (col.gameObject.CompareTag("AttractExp"))
        {
            isInExpRange = false;
        }
    }

    void DropCoffre()
    {
        GameObject coffreObj = Instantiate(coffre,transform.position,Quaternion.identity);
    }

    private void CheckIfInBound()
    {
        if (_tpOnCooldown) return;
        var currentPosition = transform.position;
        var playerPosition = CharacterController.instance.transform.position;
        var distancePlayer = currentPosition - playerPosition;
        if (distancePlayer.x>_cameraHalfWidth+outOfBoundOffSet||distancePlayer.x<-_cameraHalfWidth-outOfBoundOffSet)
        {
            transform.position = new Vector3(currentPosition.x-(2*distancePlayer.x),currentPosition.y-(2*distancePlayer.y),0);
            StartCoroutine(TpCooldown());
        }
        if (distancePlayer.y>_cameraHalfHeight+outOfBoundOffSet||distancePlayer.y<-_cameraHalfHeight-outOfBoundOffSet)
        {
            transform.position = new Vector3(currentPosition.x-(2*distancePlayer.x),currentPosition.y-(2*distancePlayer.y),0);
            StartCoroutine(TpCooldown());
        }
    }

    private IEnumerator TpCooldown()
    {
        _tpOnCooldown = true;
        yield return new WaitForSeconds(4);
        _tpOnCooldown = false;
        yield return null;
    }
} 

