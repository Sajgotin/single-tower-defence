using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weaponry", order = 1)]
public class Weaponry : ScriptableObject
{
    [Header("General Data")]
    public string weaponName;
    public Sprite weaponSprite;
    public int damage;
    [Header("Firerate Data")]
    public float fireRate;
    public int fireRateLevel;
    public int maxFireRateLevel;
    public float fireRateCost;
    public float fireRateCostMultiplier;
    [Header("Reload Time Data")]
    public float reloadTime;
    public int reloadTimeLevel;
    public int maxReloadTimeLevel;
    public float reloadTimeCost;
    public float reloadTimeCostMultiplier;
    [Header("Ammo Capacity Data")]
    public int maxAmmoCapacity;
    public int ammoCapacity;
    public int ammoCapacityLevel;
    public float ammoCapacityCost;

    public void UpgradeFireRate()
    {
        if (fireRateLevel < maxFireRateLevel)
        {
            Points.Instance.points -= (int)fireRateCost;
            UIManager.Instance.UpdateGoldValue();
            fireRateCost *= fireRateCostMultiplier;
            fireRate *= .9f;
            fireRateLevel++;
        }
    }

    public void UpgradeReloadTime()
    {
        if (reloadTimeLevel < maxReloadTimeLevel)
        {
            Points.Instance.points -= (int)reloadTimeCost;
            UIManager.Instance.UpdateGoldValue();
            reloadTimeCost *= reloadTimeCostMultiplier;
            reloadTime *= .9f;
            reloadTimeLevel++;
        }   
    }

    public void UpgradeAmmoCapacity()
    {
        Points.Instance.points -= (int)ammoCapacityCost;
        ammoCapacityLevel++;
        maxAmmoCapacity++;
        ammoCapacity++;
        ammoCapacityCost += 100;
        UIManager.Instance.UpdateGoldValue();
        UIManager.Instance.UpdateAmmoText();
    }
}
