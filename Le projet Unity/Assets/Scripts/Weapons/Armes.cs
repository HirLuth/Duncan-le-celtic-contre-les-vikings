using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public class Armes : MonoBehaviour

    {

        public ItemManager.WeaponsType weaponType;
        public Image image;
        public ArmesBaseStat baseStat;
        public ArmeStat armeStat;
        public int level;
        public bool isTransformed;
        public int damage;
        public int numberOfProjectile;
        public float coolDown;
        public float projectileSize;
        public float projectileSpeed;
        public float timeOfTheEffect;

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
    }
}
