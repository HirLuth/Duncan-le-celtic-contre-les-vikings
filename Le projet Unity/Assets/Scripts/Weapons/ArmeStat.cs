using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class ArmeStat
{
    public ItemManager.WeaponsType weaponType;
    public int level;
    public bool isTransformed;
    public int damage;
    public int numberOfProjectile;
    public float coolDown;
    public float projectileSize;
    public float projectileSpeed;
    public float timeOfTheEffect;

}
