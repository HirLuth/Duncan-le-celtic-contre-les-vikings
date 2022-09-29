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
    public SpriteRenderer playerSp;
    public static IABoss instance;
    public List<bool> triggerAttackBoss;

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
    public float waitIndicationSkill5;
    public float waitIndicationSkill5Timer;

    [Header("Skill6 - Dash")]
    public bool gotSpear;
    public bool isDashing;
    public float cooldownSkill6Timer;
    public float cooldownSkill6;
    private float truc6;
    private Vector2 direction6;
    private GameObject indic6;
    private bool isAttackingSkill6;
    public float waitIndicationSkill6;
    public float waitIndicationSkill6Timer;
    public float dureeDashTimer;
    public float dureeDash;
    public float speedDash;
    public GameObject indication6;
    
    [Header("Skill7 - AOE")] 
    public bool gotBouclier;
    public float cooldownSkill7Timer;
    public float cooldownSkill7;
    private Vector2 truc7;
    private GameObject indic7;
    private bool isAttackingSkill7;
    public float waitIndicationSkill7;
    public float waitIndicationSkill7Timer;
    public GameObject pic7;
    public float radius;


    //public static IAMonstre1 instance; 
        
    [Header("Defence")] 
    public int health;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        player = CharacterController.instance.gameObject;
        ListeMonstres.instance.ennemyList.Add(gameObject);
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerSp = CharacterController.instance.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (layerEmpty.transform.position.y > player.transform.position.y)
        {
            playerSp.sortingOrder = 2;
        }
        else
        {
            playerSp.sortingOrder = 0;
        }

        CheckIfInBound();

        if (CharacterController.instance.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }

        if (isDashing)
        {
            colldownDmg = 0.025f;
        }
        else
        {
            colldownDmg = 0.3f;
        }
        


        Vector2 target = new Vector2(player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y);

        if (isMoving)
        {
            transform.Translate(target.normalized * speed * Time.deltaTime, Space.Self);
        }

        if (isTouching)
        {
            timerDmg += Time.deltaTime;
            CharacterController.isTakingDamage = true;

            if (timerDmg > colldownDmg)
            {
                CharacterController.instance.TakeDamage(Damages);
                GameObject text = Instantiate(textDamagePlayer,
                    new Vector3(player.transform.position.x, player.transform.position.y + 1, -5), Quaternion.identity);
                textDamagePlayer.GetComponentInChildren<TextMeshPro>().SetText(Damages.ToString());
                timerDmg = 0;
            }
        }


        if (isTouching == false)
        {
            CharacterController.isTakingDamage = false;
        }


        if (triggerAttackBoss[1] || gotSword) // Skill 1
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

        if (triggerAttackBoss[2] || gotSerpe) // Skill 2
        {
            cooldownSkill2Timer += Time.deltaTime;
            if (cooldownSkill2Timer >= cooldownSkill2)
            {
                isMoving = false;
                cooldownSkill2Timer = 0;
                GameObject murObj = Instantiate(mur,
                    (Vector2)CharacterController.instance.transform.position + new Vector2(
                        CharacterController.instance.VectorDepla.x * 9, CharacterController.instance.VectorDepla.y * 9),
                    Quaternion.identity);
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

        if (triggerAttackBoss[5] || gotBaton) // Skill 3
        {
            cooldownSkill3Timer += Time.deltaTime;
            if (cooldownSkill3Timer >= cooldownSkill3)
            {
                cooldownSkill3Timer = 0;
                GameObject indication = Instantiate(indicateurSkill3,
                    (Vector2)CharacterController.instance.transform.position + new Vector2(
                        CharacterController.instance.VectorDepla.x * 9.5f,
                        CharacterController.instance.VectorDepla.y * 9.5f),
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

        if (triggerAttackBoss[6] || gotCarnyx) // Skill 4
        {
            cooldownSkill4Timer += Time.deltaTime;
            if (cooldownSkill4Timer >= cooldownSkill4)
            {
                cooldownSkill4Timer = 0;
                GameObject indication = Instantiate(indicateurSkill4,
                    (Vector2)CharacterController.instance.transform.position + new Vector2(
                        CharacterController.instance.VectorDepla.x * 9.5f,
                        CharacterController.instance.VectorDepla.y * 9.5f),
                    Quaternion.identity);
                indic4 = indication;
                isAttackingSkill3 = true;
            }

            if (isAttackingSkill4)
            {
                waitIndicationSkill4Timer += Time.deltaTime;
                if (waitIndicationSkill4Timer >= waitIndicationSkill3)
                {
                    Destroy(indic4);
                    GameObject picObj = Instantiate(pic, indic4.transform.position, Quaternion.identity);
                    waitIndicationSkill4Timer = 0;
                    isAttackingSkill4 = false;
                }
            }
        }

        if (triggerAttackBoss[4] || gotLivre) // Skill 5
        {
            cooldownSkill5Timer += Time.deltaTime;
            if (cooldownSkill5Timer >= cooldownSkill5)
            {
                isMoving = false;
                cooldownSkill5Timer = 0;
                isAttackingSkill5 = true;
                //sp.sprite = SpriteOeilRouge;
            }

            if (isAttackingSkill5)
            {
                waitIndicationSkill5Timer += Time.deltaTime;
                if (waitIndicationSkill5Timer >= waitIndicationSkill5)
                {
                    //sp.sprite = SpriteOeilPasRouge;
                    GameObject fireballObj = Instantiate(fireball, launcher.transform.position, Quaternion.identity);
                    Vector2 dir = new Vector2(
                        CharacterController.instance.transform.position.x - fireballObj.transform.position.x,
                        CharacterController.instance.transform.position.y - fireballObj.transform.position.y);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    fireballObj.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    fireballObj.GetComponent<FirballBehaviour>().direction = dir;
                    isMoving = true;
                    waitIndicationSkill5Timer = 0;
                    isAttackingSkill5 = false;
                }
            }
        }

        if (triggerAttackBoss[0] || gotSpear) // Skill 6
        {
            cooldownSkill6Timer += Time.deltaTime;
            if (cooldownSkill6Timer >= cooldownSkill6)
            {
                isMoving = false;
                cooldownSkill6Timer = 0;
                isAttackingSkill6 = true;
                Vector2 dir = new Vector2(CharacterController.instance.transform.position.x - transform.position.x,
                    CharacterController.instance.transform.position.y - transform.position.y).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                
                GameObject indication = Instantiate(indication6, (Vector2)transform.position,Quaternion.identity);
                indication.transform.localRotation = Quaternion.Euler(0, 0, angle);
                    
                indic6 = indication;
                truc6 = angle;
                direction6 = dir;
            }

            if (isAttackingSkill6)
            {
                waitIndicationSkill6Timer += Time.deltaTime;
                if (waitIndicationSkill6Timer >= waitIndicationSkill5)
                {
                    isDashing = true;
                    Destroy(indic6);
                    transform.localRotation = Quaternion.AngleAxis(truc6, Vector3.forward);
                    dureeDashTimer += Time.deltaTime;
                    transform.position = new Vector3(transform.position.x + direction6.x * speedDash, transform.position.y + direction6.y * speedDash, 0);
                    if (dureeDashTimer >= dureeDash)
                    {
                        isDashing = false;
                        waitIndicationSkill6Timer = 0;
                        isAttackingSkill6 = false;
                        transform.localRotation = new Quaternion(0, 0, 0, 0);
                        isMoving = true;
                        dureeDashTimer = 0;
                    }
                }
            }
        }

        if (triggerAttackBoss[3] || gotBouclier) // Skill 7
        {
            cooldownSkill7Timer += Time.deltaTime;
            if (cooldownSkill7Timer >= cooldownSkill7)
            {
                isMoving = false;
                cooldownSkill7Timer = 0;
                isAttackingSkill7 = true;
            }
            
            if (isAttackingSkill7)
            {
                waitIndicationSkill7Timer += Time.deltaTime;
                if (waitIndicationSkill7Timer >= waitIndicationSkill7)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var radians = 2 * MathF.PI / 10 * i;
                        
                        var vertical = MathF.Sin(radians);
                        var horizontal = MathF.Cos(radians);

                        var spawnDir = new Vector3(horizontal, vertical, 0);
                        var spawnPos = transform.position + spawnDir * radius;

                        var enemy = Instantiate(pic7, spawnPos, Quaternion.identity) as GameObject;
                    }

                    isMoving = true;
                    isAttackingSkill7 = false;
                    waitIndicationSkill7Timer = 0;
                }
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
            transform.position.Set(currentPosition.x,currentPosition.y-2*distancePlayer.y,0);
        }
    }
}
