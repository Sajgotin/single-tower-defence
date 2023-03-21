using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [SerializeField] GameObject[] _towerObjects;
    [SerializeField] Shooting _shootingScript;
    [SerializeField] Turret _turretScript;

    [Header("Firerate Upgrade")]
    [SerializeField] Button _fireRateButton;
    [SerializeField] TextMeshProUGUI _fireRateButtonText;
    [SerializeField] TextMeshProUGUI _fireRateCostText;
    [SerializeField] int _fireRateLevel = 0;
    [SerializeField] float _fireRateCost;

    [Header("Reload Speed Upgrade")]
    [SerializeField] Button _reloadTimeButton;
    [SerializeField] TextMeshProUGUI _reloadTimeButtonText;
    [SerializeField] TextMeshProUGUI _reloadTimeCostText;
    [SerializeField] int _reloadTimeLevel = 0;
    [SerializeField] float _reloadTimeCost;

    [Header("Ammo Capacity Upgrade")]
    [SerializeField] Button _ammoCapacityButton;
    [SerializeField] TextMeshProUGUI _ammoCapacityButtonText;
    [SerializeField] TextMeshProUGUI _ammoCapacityCostText;
    [SerializeField] int _ammoCapacityLevel = 0;
    [SerializeField] float _ammoCapacityCost;

    [Header("Armor Upgrade")]
    [SerializeField] Button _armorButton;
    [SerializeField] TextMeshProUGUI _armorButtonText;
    [SerializeField] TextMeshProUGUI _armorCostText;
    [SerializeField] int _armorLevel = 0;
    [SerializeField] float _armorCost;

    [Header("Autorepair Upgrade")]
    [SerializeField] Button _autorepairButton;
    [SerializeField] TextMeshProUGUI _autorepairButtonText;
    [SerializeField] TextMeshProUGUI _autorepairCostText;
    [SerializeField] int _autorepairLevel = 0;
    [SerializeField] float _autorepairCost;

    private void Start()
    {
        _shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        _turretScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Turret>();
    }

    public void UpgradeTower()
    {
        if(Points.Instance.points >= 500)
        {
            Points.Instance.points -= 500;
            int button = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            _towerObjects[button].SetActive(true);
            EventSystem.current.currentSelectedGameObject.SetActive(false);
            UIManager.Instance.UpdateGoldValue();
            GameManager.Instance.maxTowerHealth += 100f;
            GameManager.Instance.towerHealth += 100f;
            UIManager.Instance.UpdateTowerHealthSlider();
        }
    }

    public void UpgradeFireRate()
    {
        if (_fireRateLevel < 20 && Points.Instance.points >= _fireRateCost)
        {
            _shootingScript.UpgradeFireRate();
            Points.Instance.points -= (int)_fireRateCost;
            _fireRateLevel++;
            _fireRateCost *= 1.4f;
            _fireRateCostText.text = ((int)_fireRateCost).ToString();      
            UIManager.Instance.UpdateGoldValue();
            _fireRateButtonText.text = _fireRateLevel.ToString();
            if( _fireRateLevel >= 20)
            {
                _fireRateButton.interactable = false;
                _fireRateButtonText.text = "MAX";
                _fireRateCostText.gameObject.SetActive(false);
            }
        }
    }

    public void UpgradeReloadTime()
    {
        if (_reloadTimeLevel < 20 && Points.Instance.points >= _reloadTimeCost)
        {
            _shootingScript.UpgradeReloadTime();
            Points.Instance.points -= (int)_reloadTimeCost;
            _reloadTimeLevel++;
            _reloadTimeCost *= 1.4f;
            _reloadTimeCostText.text = ((int)_reloadTimeCost).ToString();     
            UIManager.Instance.UpdateGoldValue();
            _reloadTimeButtonText.text = _reloadTimeLevel.ToString();
            if(_reloadTimeLevel >= 20)
            {
                _reloadTimeButton.interactable = false;
                _reloadTimeButtonText.text = "MAX";
                _reloadTimeCostText.gameObject.SetActive(false);
            }
        }      
    }

    public void UpgradeAmmoCapacity()
    {
        if(Points.Instance.points >= _ammoCapacityCost)
        {
            _shootingScript.UpgradeAmmoCapacity();
            Points.Instance.points -= (int)_ammoCapacityCost;
            _ammoCapacityLevel++;
            _ammoCapacityCost += 100;
            _ammoCapacityCostText.text = ((int)_ammoCapacityCost).ToString();     
            UIManager.Instance.UpdateGoldValue();
            UIManager.Instance.UpdateAmmoText();
            _ammoCapacityButtonText.text = _ammoCapacityLevel.ToString();
        }  
    }

    public void UpgradeArmor()
    {
        if(Points.Instance.points >= _armorCost)
        {
            _turretScript.UpgradeTurretArmor();
            Points.Instance.points -= (int)_armorCost;
            _armorLevel++;
            _armorCost *= 1.4f;
            _armorCostText.text = ((int)_armorCost).ToString();
            UIManager.Instance.UpdateGoldValue();
            _armorButtonText.text = _armorLevel.ToString();
            if (_armorLevel >= 10)
            {
                _armorButton.interactable = false;
                _armorButtonText.text = "MAX";
                _armorCostText.gameObject.SetActive(false);
            }
        }
    }

    public void UpgradeAutorepair()
    {
        if (Points.Instance.points >= _autorepairCost)
        {
            _turretScript.UpgradeAutorepair();
            Points.Instance.points -= (int)_autorepairCost;
            _autorepairLevel++;
            _autorepairCost *= 1.4f;
            _autorepairCostText.text = ((int)_autorepairCost).ToString();
            UIManager.Instance.UpdateGoldValue();
            _autorepairButtonText.text = _autorepairLevel.ToString();
            if (_autorepairLevel >= 10)
            {
                _autorepairButton.interactable = false;
                _autorepairButtonText.text = "MAX";
                _autorepairCostText.gameObject.SetActive(false);
            }
        }
    }
}
