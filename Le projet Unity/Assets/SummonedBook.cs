using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SummonedBook : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    public float angleOfThebookInDegree;
    public float effectDuration;
    public int damage;
    public float paperSpeed;
    private float timer;
    private float timerPaper;
    [SerializeField] private float timeBetweenPaper;
    public float sprayAngle;
    public int numberOfQuarter;
    
    private void Update()
    {
        transform.position = CharacterController.instance.transform.position;
        timer += Time.deltaTime;
        timerPaper += Time.deltaTime;
        if (timerPaper>= timeBetweenPaper)
        {
            SummonPaper();
            timerPaper = 0;
        }
        if (timer >= effectDuration)
        {
            DestroyImmediate(gameObject);
        }
    }

    private void SummonPaper()
    {
        GameObject currentPaper = Instantiate(projectile);
        currentPaper.transform.position = transform.position;
        float angleOfThePaper = angleOfThebookInDegree+90 + Random.Range(-numberOfQuarter/2,1+numberOfQuarter/2)*(sprayAngle/numberOfQuarter);
        Paper currentPaperScript = currentPaper.GetComponent<Paper>();
        currentPaperScript.direction = new Vector2(Mathf.Cos((angleOfThePaper) * 2 * Mathf.PI / 360),Mathf.Sin((angleOfThePaper) * 2 * Mathf.PI / 360));
        currentPaperScript.damage = damage;
        currentPaperScript.speed = paperSpeed;
    }
}
