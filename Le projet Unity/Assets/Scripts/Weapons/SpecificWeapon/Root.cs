using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Root : MonoBehaviour
{
    public int damage;
    public float size;
    private float timer;
    [SerializeField] float timeToDisapear;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale *= size;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDisapear)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(11);
        if (col.gameObject.CompareTag("Monstre"))
        {
            col.gameObject.GetComponent<IAMonstre1>().TakeDamage(damage);
            col.gameObject.GetComponent<IAMonstre1>().DamageText(damage);
        }
    }
    
}
