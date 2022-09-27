using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpDamage : MonoBehaviour
{
    private TextMeshPro textMesh;
    public static PopUpDamage instance;
    public int text;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        textMesh = transform.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    public void ShowDamage(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        text = damageAmount;
    }
    
    private void Update()
    {
        Destroy(transform.parent.gameObject,1f);
    }
}
