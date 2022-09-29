using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Shield : MonoBehaviour
{
    [SerializeField] private Color colorShieldIdle;
    [SerializeField] private Color colorShieldActivated;
    [SerializeField] private SpriteRenderer shieldSprite;
    [SerializeField] private Armes armes;
    [SerializeField] private List<float> coolDownPerLevel;
    [SerializeField] private List<float> timeEffectPerLevel;
    private float timerCoolDown;
    private float timerOftheShield;
    
    // Start is called before the first frame update
    void Start()
    {
        shieldSprite = CharacterController.instance.shield.GetComponent<SpriteRenderer>();
        shieldSprite.enabled = true;
        CharacterController.instance.shieldAvailable = true;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CharacterController.instance.shieldActivated);
        Debug.Log(CharacterController.instance.shieldAvailable);
        if (!CharacterController.instance.shieldActivated && !CharacterController.instance.shieldAvailable)
        {
            timerCoolDown += Time.deltaTime;
            shieldSprite.enabled = false;
        }
        
        if (timerCoolDown >= armes.coolDown*coolDownPerLevel[armes.level])
        {
            shieldSprite.enabled = true;
            shieldSprite.color = colorShieldIdle;
            CharacterController.instance.shieldAvailable = true;
            timerCoolDown = 0;
        }
        
        if (CharacterController.instance.shieldActivated)
        {
            Debug.Log(42);
            timerOftheShield += Time.deltaTime;
            shieldSprite.color = colorShieldActivated;
        }

        if (timerOftheShield >= armes.timeOfTheEffect*timeEffectPerLevel[armes.level])
        {
            CharacterController.instance.shieldActivated = false;
            timerOftheShield = 0;
        }

        
    }
}
