using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class IABoss : MonoBehaviour
{
    [SerializeField] private float outOfBoundOffSet;
    public GameObject player;
    public float speed;
    public float colldownDmg;
    public int Damages;
    public bool isMoving;
    private float timerDmg;
    private bool isTouching;
    private Rigidbody2D rb;
    public GameObject textDamage;
    public GameObject textDamagePlayer;
    public bool bossFight;
    public GameObject layerEmpty;

    [Header("Skill1 - TP")] 
    public bool gotSword;
    public float cooldownSkill1Timer;
    public float cooldownSkill1;
    public GameObject indicateurSkill1;
    public float waitIndicationSkill1;
    public float waitIndicationSkill1Timer;
    private GameObject indic;
    private bool isAttackingSkill1;
    private Vector2 truc1;
    
    [Header("Skill2 - Mur")] 
    public bool gotSerpe;
    public float cooldownSkill2Timer;
    public float cooldownSkill2;
    public GameObject mur;
    private Vector2 truc2;
    private bool isAttackingSkill2;
    public float waitToMoveSkill2;
    public float waitToMoveSkill2Timer;
    
    [Header("Skill3 - Pic")] 
    public bool gotBaton;
    public float cooldownSkill3Timer;
    public float cooldownSkill3;
    public GameObject pic;
    private Vector2 truc3;
    private GameObject indic3;
    private bool isAttackingSkill3;
    public float waitIndicationSkill3;
    public float waitIndicationSkill3Timer;
    public GameObject indicateurSkill3;
    
    [Header("Skill4 - Pièges")] 
    public bool gotCarnyx;
    public float cooldownSkill4Timer;
    public float cooldownSkill4;
    public GameObject piege;
    public GameObject indicateurSkill4;
    private Vector2 truc4;
    private GameObject indic4;
    private bool isAttackingSkill4;
    public float waitIndicationSkill4;
    public float waitIndicationSkill4Timer; 
    
    [Header("Skill5 - Fireball")] 
    public bool gotLivre;
    public float cooldownSkill5Timer;
    public float cooldownSkill5;
    public GameObject fireball;
    private Vector2 truc5;
    private GameObject indic5;
    private bool isAttackingSkill5;
    public GameObject launcher;
    public SpriteRenderer sp;
    public float waitIndicationSkill5;
    public float waitIndicationSkill5Timer;


    //public static IAMonstre1 instance; 
        
    [Header("Defence")] 
    public int health;

    private void Start()
    {
        player = CharacterController.instance.gameObject;
        ListeMonstres.instance.ennemyList.Add(gameObject);
        rb = gameObject.GetComponent<Rigidbody2D>();
        sp = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (layerEmpty.transform.position.y > player.transform.position.y)
        {
            sp.sortingOrder = 0;
        }
        else
        {
           sp.sortingOrder = 2;
        }
        
        CheckIfInBound();
        
        if (CharacterController.instance.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        }
        else
        {
            transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
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
                GameObject text = Instantiate(textDamagePlayer, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
                textDamagePlayer.GetComponentInChildren<TextMeshPro>().SetText(Damages.ToString());
                timerDmg = 0;
            }
        }
        
        
        if (isTouching == false)
        {
            CharacterController.isTakingDamage = false;
        }


        if (gotSword) // Skill 1
        {
            Vector2 charPos;

            cooldownSkill1Timer += Time.deltaTime;
            if (cooldownSkill1Timer >= cooldownSkill1)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                isMoving = false;
                cooldownSkill1Timer = 0;
                GameObject indication = Instantiate(indicateurSkill1, CharacterController.instance.transform.position,
                    Quaternion.identity);
                indic = indication;
                charPos = CharacterController.instance.transform.position;
                truc1 = charPos;
                isAttackingSkill1 = true;
            }

            if (isAttackingSkill1)
            {
                waitIndicationSkill1Timer += Time.deltaTime;
                if (waitIndicationSkill1Timer >= waitIndicationSkill1)
                {
                    Destroy(indic);
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    isMoving = true;
                    transform.position = truc1;
                    waitIndicationSkill1Timer = 0;
                    isAttackingSkill1 = false;
                }
            }
           
        }

        if (gotSerpe) // Skill 2
        {
            cooldownSkill2Timer += Time.deltaTime;
            if (cooldownSkill2Timer >= cooldownSkill2)
            {
                isMoving = false;
                cooldownSkill2Timer = 0;
                GameObject murObj = Instantiate(mur, (Vector2)CharacterController.instance.transform.position + new Vector2(CharacterController.instance.VectorDepla.x * 9,CharacterController.instance.VectorDepla.y * 9), Quaternion.identity);
                isAttackingSkill2 = true;
            }

            if (isAttackingSkill2)
            {
                waitToMoveSkill2Timer += Time.deltaTime;
                if (waitToMoveSkill2Timer >= waitToMoveSkill2)
                {
                    isMoving = true;
                    waitToMoveSkill2Timer = 0;
                    isAttackingSkill2 = false;  
                }
            }
        }
        
        if (gotBaton) // Skill 3
        {
            cooldownSkill3Timer += Time.deltaTime;
            if (cooldownSkill3Timer >= cooldownSkill3)
            {
                cooldownSkill3Timer = 0;
                GameObject indication = Instantiate(indicateurSkill3, (Vector2)CharacterController.instance.transform.position + new Vector2(CharacterController.instance.VectorDepla.x * 9.5f,CharacterController.instance.VectorDepla.y * 9.5f),
                    Quaternion.identity);
                indic3 = indication;
                isAttackingSkill3 = true;
            }

            if (isAttackingSkill3)
            {
                waitIndicationSkill3Timer += Time.deltaTime;
                if (waitIndicationSkill3Timer >= waitIndicationSkill3)
                {
                    Destroy(indic3);
                    GameObject picObj = Instantiate(pic, indic3.transform.position, Quaternion.identity);
                    waitIndicationSkill3Timer = 0;
                    isAttackingSkill3 = false;
                }
            }
        }
        
        if (gotCarnyx) // Skill 4
        {
            cooldownSkill4Timer += Time.deltaTime;
            if (cooldownSkill4Timer >= cooldownSkill4)
            {
                cooldownSkill4Timer = 0;
                GameObject indication = Instantiate(indicateurSkill4, (Vector2)CharacterController.instance.transform.position + new Vector2(CharacterController.instance.VectorDepla.x * 9.5f,CharacterController.instance.VectorDepla.y * 9.5f),
                    Quaternion.identity);
                indic3 = indication;
                isAttackingSkill3 = true;
            }

            if (isAttackingSkill3)
            {
                waitIndicationSkill3Timer += Time.deltaTime;
                if (waitIndicationSkill3Timer >= waitIndicationSkill3)
                {
                    Destroy(indic3);
                    GameObject picObj = Instantiate(pic, indic3.transform.position, Quaternion.identity);
                    waitIndicationSkill3Timer = 0;
                    isAttackingSkill3 = false;
                }
            }
        }
        
        if (gotLivre) // Skill 5
        {
            cooldownSkill5Timer += Time.deltaTime;
            if (cooldownSkill5Timer >= cooldownSkill5)
            {
                isMoving = false;
                cooldownSkill5Timer = 0;
                GameObject fireballObj = Instantiate(fireball, launcher.transform.position, Quaternion.identity);
                Vector2 dir = new Vector2(CharacterController.instance.transform.position.x - fireballObj.transform.position.x,
                    CharacterController.instance.transform.position.y - fireballObj.transform.position.y);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                fireballObj.transform.localRotation = Quaternion.AngleAxis(angle,Vector3.forward);
                fireballObj.GetComponent<FirballBehaviour>().direction = dir;
            }
        }
    }
    
    public void DamageText(int damageAmount)
    {
        if (bossFight)
        {
            Instantiate(textDamage, new Vector3(transform.position.x,transform.position.y + 1,-5), Quaternion.identity);
            textDamage.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());
        }
    }
    

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            ExpManager.instance.CreateExp(transform.position,Random.Range(1,3));
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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouching = false;
            timerDmg = 0;
        }
    }
    
    
    private void CheckIfInBound()
    {
        var refCamera = CameraController.instance.camera;
        var cameraHalfHeight = refCamera.orthographicSize;
        var cameraHalfWidth = refCamera.aspect * refCamera.orthographicSize;
        var currentPosition = transform.position;
        var playerPosition = CharacterController.instance.transform.position;
        if (currentPosition.x>cameraHalfWidth+outOfBoundOffSet||currentPosition.x<-cameraHalfWidth-outOfBoundOffSet)
        {
            var distancePlayer = currentPosition - playerPosition;
            transform.position.Set(currentPosition.x-2*distancePlayer.x,currentPosition.y,0);
        }
        if (currentPosition.y>cameraHalfHeight+outOfBoundOffSet||currentPosition.y<-cameraHalfHeight-outOfBoundOffSet)
        {
            var distancePlayer = currentPosition - playerPosition;
            Debug.Log(distancePlayer);
            transform.position.Set(currentPosition.x,currentPosition.y-2*distancePlayer.y,0);
        }
    }
}
