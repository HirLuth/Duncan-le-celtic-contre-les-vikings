using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [Header("Skill1")] 
    public bool gotSword;
    public float cooldownSkill1Timer;
    public float cooldownSkill1;
    public GameObject indicateurSkill1;
    public float waitIndicationSkill1;
    public float waitIndicationSkill1Timer;
    private GameObject indic;
    private bool isAttackingSkill1;
    private Vector2 truc;


    //public static IAMonstre1 instance; 
        
    [Header("Defence")] 
    public int health;

    private void Start()
    {
        player = CharacterController.instance.gameObject;
        ListeMonstres.instance.ennemyList.Add(gameObject);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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


        if (gotSword)
        {
            Vector2 charPos;

            cooldownSkill1Timer += Time.deltaTime;
            if (cooldownSkill1Timer >= cooldownSkill1)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                isMoving = false;
                cooldownSkill1Timer = 0;
                GameObject indication = Instantiate(indicateurSkill1, CharacterController.instance.transform.position,
                    Quaternion.identity);
                indic = indication;
                charPos = CharacterController.instance.transform.position;
                truc = charPos;
                isAttackingSkill1 = true;
            }

            if (isAttackingSkill1)
            {
                waitIndicationSkill1Timer += Time.deltaTime;
                if (waitIndicationSkill1Timer >= waitIndicationSkill1)
                {
                  
                    Destroy(indic);
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                    isMoving = true;
                    transform.position = truc;
                    waitIndicationSkill1Timer = 0;
                    isAttackingSkill1 = false;
                }
            }
           
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
        if (health <= 0)
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
