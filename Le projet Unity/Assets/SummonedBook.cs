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
    [SerializeField] private List<float> listOfArray;
    [SerializeField] private List<float> listToUse;
    [SerializeField] private float timeBetweenPaper;

    private void Start()
    {
        listToUse = listOfArray.ToList();
    }

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
        if (listToUse.Count == 0)
        {
            listToUse = listOfArray.ToList();
        }
        int sortedInt = Random.Range(0, listToUse.Count - 1);
        GameObject currentPaper = Instantiate(projectile);
        currentPaper.transform.position = transform.position;
        float angleOfThePaper = angleOfThebookInDegree + Random.Range(listToUse[sortedInt]-15,listToUse[sortedInt]+15);
        Paper currentPaperScript = currentPaper.GetComponent<Paper>();
        currentPaperScript.direction = new Vector2(Mathf.Cos((angleOfThePaper) * 2 * Mathf.PI / 360),Mathf.Sin((angleOfThePaper) * 2 * Mathf.PI / 360));//Quaternion.AngleAxis(angleOfThePaper, Vector3.forward ) * Vector3.up);
        currentPaperScript.damage = damage;
        currentPaperScript.speed = paperSpeed;
    }
}
