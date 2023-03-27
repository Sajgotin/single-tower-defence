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
    [SerializeField] Weaponry _cannon, _machinegun, _heavyCannon;
    public enum WeaponrySelect { Machinegun, Cannon, HeavyCannon }
    [SerializeField] WeaponrySelect _activeWeapon;
    public WeaponrySelect ActiveWeapon { get { return _activeWeapon; } }
    [SerializeField] bool _cannonBought;
    [SerializeField] bool _heavyCannonBought;

    [Header("Firerate Upgrade")]
    [SerializeField] Button _fireRateButton;
    public Button FireRateButton { get { return _fireRateButton; } }
    [SerializeField] TextMeshProUGUI _fireRateButtonText;
    [SerializeField] TextMeshProUGUI _fireRateCostText;

    [Header("Reload Speed Upgrade")]
    [SerializeField] Button _reloadTimeButton;
    [SerializeField] TextMeshProUGUI _reloadTimeButtonText;
    [SerializeField] TextMeshProUGUI _reloadTimeCostText;

    [Header("Ammo Capacity Upgrade")]
    [SerializeField] Button _ammoCapacityButton;
    [SerializeField] TextMeshProUGUI _ammoCapacityButtonText;
    [SerializeField] TextMeshProUGUI _ammoCapacityCostText;

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

    [Header("Weapon Select Buttons")]
    [SerializeField] Button _machinegunButton;
    [SerializeField] TextMeshProUGUI _machinegunButtonText;
    [SerializeField] Button _cannonButton;
    [SerializeField] TextMeshProUGUI _cannonButtonText;
    [SerializeField] TextMeshProUGUI _cannonCostText;
    [SerializeField] Button _heavyCannonButton;
    [SerializeField] TextMeshProUGUI _heavyCannonButtonText;
    [SerializeField] TextMeshProUGUI _heavyCannonCostText;

    private void Start()
    {
        _shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        _turretScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Turret>();
    }

    private void OnEnable()
    {
        UpdateShopData();
    }

    private void OnDisable()
    {
        _shootingScript.PrepareWeapon();
    }

    void UpdateShopData()
    {
        if(_activeWeapon == WeaponrySelect.Cannon)
        {
            _fireRateCostText.text = ((int)_cannon.fireRateCost).ToString();
            _fireRateButtonText.text = _cannon.fireRateLevel.ToString();
            _fireRateButton.interactable = true;
            if (_cannon.fireRateLevel >= _cannon.maxFireRateLevel)
            {
                _fireRateButton.interactable = false;
                _fireRateButtonText.text = "MAX";
                _fireRateCostText.gameObject.SetActive(false);
            }
            _cannonButton.interactable = false;
            _cannonButtonText.text = " - ";
            _cannonCostText.gameObject.SetActive(false);
            _machinegunButton.interactable = true;
            _machinegunButtonText.text = "USE";
            CheckHeavyCannonButton();
        }
        else if (_activeWeapon == WeaponrySelect.Machinegun)
        {
            _fireRateCostText.text = ((int)_machinegun.fireRateCost).ToString();
            _fireRateButtonText.text = _machinegun.fireRateLevel.ToString();
            _fireRateButton.interactable = true;
            if (_machinegun.fireRateLevel >= _machinegun.maxFireRateLevel)
            {
                _fireRateButton.interactable = false;
                _fireRateButtonText.text = "MAX";
                _fireRateCostText.gameObject.SetActive(false);
            }
            _machinegunButton.interactable = false;
            _machinegunButtonText.text = " - ";
            CheckCannonButton();
            CheckHeavyCannonButton();
            
            
        }
        else if (_activeWeapon == WeaponrySelect.HeavyCannon)
        {
            _fireRateCostText.text = ((int)_heavyCannon.fireRateCost).ToString();
            _fireRateButtonText.text = _heavyCannon.fireRateLevel.ToString();
            _fireRateButton.interactable = true;
            if (_heavyCannon.fireRateLevel >= _heavyCannon.maxFireRateLevel)
            {
                _fireRateButton.interactable = false;
                _fireRateButtonText.text = "MAX";
                _fireRateCostText.gameObject.SetActive(false);
            }
            _heavyCannonButton.interactable = false;
            _heavyCannonButtonText.text = " - ";
            _heavyCannonCostText.gameObject.SetActive(false);
            _machinegunButton.interactable = true;
            _machinegunButtonText.text = "USE";
            CheckCannonButton();
        }
    }

    void CheckHeavyCannonButton()
    {
        if (_heavyCannonBought)
        {
            _heavyCannonButtonText.text = "USE";
            _heavyCannonCostText.gameObject.SetActive(false);
            _heavyCannonButton.interactable = true;
        }
        else
        {
            _heavyCannonButtonText.text = "BUY";
            _heavyCannonCostText.gameObject.SetActive(true);
            _heavyCannonButton.interactable = true;
        }
    }

    void CheckCannonButton()
    {
        if (_cannonBought)
        {
            _cannonButtonText.text = "USE";
            _cannonCostText.gameObject.SetActive(false);
            _cannonButton.interactable = true;
        }
        else
        {
            _cannonButtonText.text = "BUY";
            _cannonCostText.gameObject.SetActive(true);
            _cannonButton.interactable = true;
        }
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
        if (_activeWeapon == WeaponrySelect.Cannon && Points.Instance.points >= _cannon.fireRateCost)
        {
            if (_cannon.UpgradeFireRate(1.4f))
            {
                Points.Instance.points -= (int)_cannon.fireRateCost;
                _fireRateCostText.text = ((int)_cannon.fireRateCost).ToString();
                UIManager.Instance.UpdateGoldValue();
                _fireRateButtonText.text = _cannon.fireRateLevel.ToString();
                if (_cannon.fireRateLevel >= _cannon.maxFireRateLevel)
                {
                    _fireRateButton.interactable = false;
                    _fireRateButtonText.text = "MAX";
                    _fireRateCostText.gameObject.SetActive(false);
                }
            } 
        }
        else if (_activeWeapon == WeaponrySelect.Machinegun && Points.Instance.points >= _machinegun.fireRateCost)
        {
            if (_machinegun.UpgradeFireRate(1.3f))
            {
                Points.Instance.points -= (int)_machinegun.fireRateCost;
                _fireRateCostText.text = ((int)_machinegun.fireRateCost).ToString();
                UIManager.Instance.UpdateGoldValue();
                _fireRateButtonText.text = _machinegun.fireRateLevel.ToString();
                if (_machinegun.fireRateLevel >= _machinegun.maxFireRateLevel)
                {
                    _fireRateButton.interactable = false;
                    _fireRateButtonText.text = "MAX";
                    _fireRateCostText.gameObject.SetActive(false);
                }
            }
        }
        else if (_activeWeapon == WeaponrySelect.HeavyCannon && Points.Instance.points >= _heavyCannon.fireRateCost)
        {
            if (_heavyCannon.UpgradeFireRate(1.4f))
            {
                Points.Instance.points -= (int)_heavyCannon.fireRateCost;
                _fireRateCostText.text = ((int)_heavyCannon.fireRateCost).ToString();
                UIManager.Instance.UpdateGoldValue();
                _fireRateButtonText.text = _heavyCannon.fireRateLevel.ToString();
                if (_heavyCannon.fireRateLevel >= _heavyCannon.maxFireRateLevel)
                {
                    _fireRateButton.interactable = false;
                    _fireRateButtonText.text = "MAX";
                    _fireRateCostText.gameObject.SetActive(false);
                }
            }
        }
    }

    public void UpgradeReloadTime()
    {
        if (_activeWeapon == WeaponrySelect.Cannon && Points.Instance.points >= _cannon.reloadTimeCost)
        {
            if (_cannon.UpgradeReloadTime(1.4f))
            {
                Points.Instance.points -= (int)_cannon.reloadTimeCost;
                _cannon.reloadTimeLevel++;
                _reloadTimeCostText.text = ((int)_cannon.reloadTimeCost).ToString();
                UIManager.Instance.UpdateGoldValue();
                _reloadTimeButtonText.text = _cannon.reloadTimeLevel.ToString();
                if (_cannon.reloadTimeLevel >= _cannon.maxReloadTimeLevel)
                {
                    _reloadTimeButton.interactable = false;
                    _reloadTimeButtonText.text = "MAX";
                    _reloadTimeCostText.gameObject.SetActive(false);
                }
            } 
        }     
        else if (_activeWeapon == WeaponrySelect.Machinegun && Points.Instance.points >= _machinegun.reloadTimeCost)
        {
            if (_machinegun.UpgradeReloadTime(1.3f))
            {
                Points.Instance.points -= (int)_machinegun.reloadTimeCost;
                _machinegun.reloadTimeLevel++;
                _reloadTimeCostText.text = ((int)_machinegun.reloadTimeCost).ToString();
                UIManager.Instance.UpdateGoldValue();
                _reloadTimeButtonText.text = _machinegun.reloadTimeLevel.ToString();
                if (_machinegun.reloadTimeLevel >= _machinegun.maxReloadTimeLevel)
                {
                    _reloadTimeButton.interactable = false;
                    _reloadTimeButtonText.text = "MAX";
                    _reloadTimeCostText.gameObject.SetActive(false);
                }
            }
        }
        else if (_activeWeapon == WeaponrySelect.HeavyCannon && Points.Instance.points >= _heavyCannon.reloadTimeCost)
        {
            if (_heavyCannon.UpgradeReloadTime(1.2f))
            {
                Points.Instance.points -= (int)_heavyCannon.reloadTimeCost;
                _heavyCannon.reloadTimeLevel++;
                _reloadTimeCostText.text = ((int)_heavyCannon.reloadTimeCost).ToString();
                UIManager.Instance.UpdateGoldValue();
                _reloadTimeButtonText.text = _heavyCannon.reloadTimeLevel.ToString();
                if (_heavyCannon.reloadTimeLevel >= _heavyCannon.maxReloadTimeLevel)
                {
                    _reloadTimeButton.interactable = false;
                    _reloadTimeButtonText.text = "MAX";
                    _reloadTimeCostText.gameObject.SetActive(false);
                }
            }
        }
    }

    public void UpgradeAmmoCapacity()
    {
        if(_activeWeapon == WeaponrySelect.Cannon && Points.Instance.points >= _cannon.ammoCapacityCost)
        {
            _cannon.UpgradeAmmoCapacity();
            Points.Instance.points -= (int)_cannon.ammoCapacityCost;
            _cannon.ammoCapacityLevel++;
            _ammoCapacityCostText.text = ((int)_cannon.ammoCapacityCost).ToString();     
            UIManager.Instance.UpdateGoldValue();
            UIManager.Instance.UpdateAmmoText();
            _ammoCapacityButtonText.text = _cannon.ammoCapacityLevel.ToString();
        }  
        else if (_activeWeapon == WeaponrySelect.Machinegun && Points.Instance.points >= _machinegun.ammoCapacityCost)
        {
            _machinegun.UpgradeAmmoCapacity();
            Points.Instance.points -= (int)_machinegun.ammoCapacityCost;
            _machinegun.ammoCapacityLevel++;
            _ammoCapacityCostText.text = ((int)_machinegun.ammoCapacityCost).ToString();
            UIManager.Instance.UpdateGoldValue();
            UIManager.Instance.UpdateAmmoText();
            _ammoCapacityButtonText.text = _machinegun.ammoCapacityLevel.ToString();
        }
        else if (_activeWeapon == WeaponrySelect.HeavyCannon && Points.Instance.points >= _heavyCannon.ammoCapacityCost)
        {
            _heavyCannon.UpgradeAmmoCapacity();
            Points.Instance.points -= (int)_heavyCannon.ammoCapacityCost;
            _heavyCannon.ammoCapacityLevel++;
            _ammoCapacityCostText.text = ((int)_heavyCannon.ammoCapacityCost).ToString();
            UIManager.Instance.UpdateGoldValue();
            UIManager.Instance.UpdateAmmoText();
            _ammoCapacityButtonText.text = _heavyCannon.ammoCapacityLevel.ToString();
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

    public void BuyAndSelectMachinegun()
    {
        _activeWeapon = WeaponrySelect.Machinegun;
        UpdateShopData();
        _shootingScript.PrepareWeapon();

    }

    public void BuyAndSelectCannon()
    {
        if(!_cannonBought && Points.Instance.points >= 10000)
        {
            Points.Instance.points -= 10000;
            UIManager.Instance.UpdateGoldValue();
            _cannonBought = true;
            _activeWeapon = WeaponrySelect.Cannon;
            UpdateShopData();
            _shootingScript.PrepareWeapon();
        }
        if (_cannonBought)
        {
            _activeWeapon = WeaponrySelect.Cannon;
            UpdateShopData();
            _shootingScript.PrepareWeapon();
        }
    }
    
    public void BuyAndSelectHeavyCannon()
    {
        if(!_heavyCannonBought && Points.Instance.points >= 100000)
        {
            Points.Instance.points -= 100000;
            UIManager.Instance.UpdateGoldValue();
            _heavyCannonBought = true;
            _activeWeapon = WeaponrySelect.HeavyCannon;
            UpdateShopData();
            _shootingScript.PrepareWeapon();
        }
        if(_heavyCannonBought)
        {
            _activeWeapon = WeaponrySelect.HeavyCannon;
            UpdateShopData();
            _shootingScript.PrepareWeapon();
        }
    }
}
