using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weaponry", order = 1)]
public class Weaponry : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public float fireRate;
    public float reloadTime;
    public int maxAmmoCapacity;
    public int ammoCapacity;
    public int ammoCapacityLevel;
    public float ammoCapacityCost;
    public int damage;
    public int maxFireRateLevel;
    public int fireRateLevel;
    public float fireRateCost;
    public int maxReloadTimeLevel;
    public int reloadTimeLevel;
    public float reloadTimeCost;

    public bool UpgradeFireRate()
    {
        if (fireRateLevel < maxFireRateLevel)
        {
            fireRate *= .9f;
            fireRateLevel++;
            return true;
        }
        else return false;
    }

    public bool UpgradeReloadTime()
    {
        if (reloadTimeLevel < maxReloadTimeLevel)
        {
            reloadTime *= .9f;
            reloadTimeLevel++;
            return true;
        }
        else return false;
        
    }

    public void UpgradeAmmoCapacity()
    {
        maxAmmoCapacity++;
        ammoCapacity++;
        ammoCapacityCost += 100;
    }
}
