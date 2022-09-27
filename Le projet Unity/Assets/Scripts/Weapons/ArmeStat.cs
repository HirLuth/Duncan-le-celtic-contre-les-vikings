using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public class ArmeStat
    {
        public ItemManager.WeaponsType weaponType;
        [Header("Stats")]
        public int level;
        public bool isTransformed;
        public int damage;
        public int numberOfProjectile;
        public float coolDown;
        public float projectileSize;
        public float projectileSpeed;
        public float timeOfTheEffect;
        
        [Header("Display and Menu")]
        public Sprite sprite;
        public string nameInMenus;
        public string description;
    }
}
