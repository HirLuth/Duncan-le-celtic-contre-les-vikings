using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurBehaviour : MonoBehaviour
{
    private float timeToCollideTimer;
    public float timeToCollide;
    public float timeToDestroy;
    
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 80);
    }

    // Update is called once per frame
    void Update()
    {
        timeToCollideTimer += Time.deltaTime;

        if (timeToCollideTimer >= timeToCollide)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            Destroy(gameObject, timeToDestroy);
        }
    }
}
