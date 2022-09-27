using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Weapons
{
    public class Armes : MonoBehaviour

    {
        public static Armes instance;
        
        public ItemManager.WeaponsType weaponType;
        public Image image;
        public ArmesBaseStat baseStat;
        public ArmeStat armeStat;
        [SerializeField] private TMP_Text levelIndicator;

        public int level;
        public bool isTransformed;
        public int damage;
        public int numberOfProjectile;
        public float coolDown;
        public float projectileSize;
        public float projectileSpeed;
        public float timeOfTheEffect;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            armeStat = baseStat.listBaseStats[(int)weaponType];
            image.sprite = armeStat.sprite;
            level = armeStat.level;
            isTransformed = armeStat.isTransformed;
            damage = armeStat.damage;
            numberOfProjectile = armeStat.numberOfProjectile;
            coolDown = armeStat.coolDown;
            projectileSize = armeStat.projectileSize;
            projectileSpeed = armeStat.projectileSpeed;
            timeOfTheEffect = armeStat.timeOfTheEffect;
        }

        public void UpdateLevelIndicator()
        {
            levelIndicator.text = "" + level;
        }
    }
}
