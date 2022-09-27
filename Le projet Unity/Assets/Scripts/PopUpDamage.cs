using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpDamage : MonoBehaviour
{
    private GameObject textDamage;
    public static PopUpDamage instance;
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //textMesh = transform.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    public void DamageTextPlayer(int damageAmount)
    {
        Instantiate(textDamage, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
        textDamage.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());
    }
    
    public void DamageText(int damageAmount)
    {
        Instantiate(textDamage, new Vector3(player.transform.position.x,player.transform.position.y + 1,-5), Quaternion.identity);
        textDamage.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());
    }
    
    private void Update()
    {
        Destroy(transform.parent.gameObject,1f);
    }
}
