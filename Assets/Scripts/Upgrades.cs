using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [SerializeField] GameObject[] _towerObjects;
    [SerializeField] GameObject _upgradeMenu;
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

    private void Awake()
    {
        _shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        _turretScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Turret>();
        if (MainMenu.saveExist) Load();
        UpdateShopData();
        _upgradeMenu.SetActive(false);   
    }

    private void OnEnable()
    {
        UpdateShopData();
    }

    private void OnDisable()
    {
        _shootingScript.PrepareWeapon();
    }

    public void UpdateShopData()
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
            _reloadTimeCostText.text = ((int)_cannon.reloadTimeCost).ToString();
            _reloadTimeButtonText.text = _cannon.reloadTimeLevel.ToString();
            if (_cannon.reloadTimeLevel >= _cannon.maxReloadTimeLevel)
            {
                _reloadTimeButton.interactable = false;
                _reloadTimeButtonText.text = "MAX";
                _reloadTimeCostText.gameObject.SetActive(false);
            }
            _ammoCapacityCostText.text = ((int)_cannon.ammoCapacityCost).ToString();
            _ammoCapacityButtonText.text = _cannon.ammoCapacityLevel.ToString();
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
            _reloadTimeCostText.text = ((int)_machinegun.reloadTimeCost).ToString();
            _reloadTimeButtonText.text = _machinegun.reloadTimeLevel.ToString();
            if (_machinegun.reloadTimeLevel >= _machinegun.maxReloadTimeLevel)
            {
                _reloadTimeButton.interactable = false;
                _reloadTimeButtonText.text = "MAX";
                _reloadTimeCostText.gameObject.SetActive(false);
            }
            _ammoCapacityCostText.text = ((int)_machinegun.ammoCapacityCost).ToString();
            _ammoCapacityButtonText.text = _machinegun.ammoCapacityLevel.ToString();
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
            _reloadTimeCostText.text = ((int)_heavyCannon.reloadTimeCost).ToString();
            _reloadTimeButtonText.text = _heavyCannon.reloadTimeLevel.ToString();
            if (_heavyCannon.reloadTimeLevel >= _heavyCannon.maxReloadTimeLevel)
            {
                _reloadTimeButton.interactable = false;
                _reloadTimeButtonText.text = "MAX";
                _reloadTimeCostText.gameObject.SetActive(false);
            }
            _ammoCapacityCostText.text = ((int)_heavyCannon.ammoCapacityCost).ToString();
            _ammoCapacityButtonText.text = _heavyCannon.ammoCapacityLevel.ToString();
            _heavyCannonButton.interactable = false;
            _heavyCannonButtonText.text = " - ";
            _heavyCannonCostText.gameObject.SetActive(false);
            _machinegunButton.interactable = true;
            _machinegunButtonText.text = "USE";
            CheckCannonButton();
        }

        _armorCostText.text = ((int)_armorCost).ToString();
        _armorButtonText.text = _armorLevel.ToString();
        if (_armorLevel >= 10)
        {
            _armorButton.interactable = false;
            _armorButtonText.text = "MAX";
            _armorCostText.gameObject.SetActive(false);
        }

        _autorepairCostText.text = ((int)_autorepairCost).ToString();
        _autorepairButtonText.text = _autorepairLevel.ToString();
        if (_autorepairLevel >= 10)
        {
            _autorepairButton.interactable = false;
            _autorepairButtonText.text = "MAX";
            _autorepairCostText.gameObject.SetActive(false);
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
            if (_cannon.UpgradeFireRate())
            {
                Points.Instance.points -= (int)_cannon.fireRateCost;
                UIManager.Instance.UpdateGoldValue();
                _cannon.fireRateCost *= 1.4f;
                _fireRateCostText.text = ((int)_cannon.fireRateCost).ToString();
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
            if (_machinegun.UpgradeFireRate())
            {
                Points.Instance.points -= (int)_machinegun.fireRateCost;
                UIManager.Instance.UpdateGoldValue();
                _machinegun.fireRateCost *= 1.3f;
                _fireRateCostText.text = ((int)_machinegun.fireRateCost).ToString();  
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
            if (_heavyCannon.UpgradeFireRate())
            {
                Points.Instance.points -= (int)_heavyCannon.fireRateCost;
                UIManager.Instance.UpdateGoldValue();
                _heavyCannon.fireRateCost *= 1.4f;
                _fireRateCostText.text = ((int)_heavyCannon.fireRateCost).ToString();
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
            if (_cannon.UpgradeReloadTime())
            {
                Points.Instance.points -= (int)_cannon.reloadTimeCost;
                UIManager.Instance.UpdateGoldValue();
                _cannon.reloadTimeCost *= 1.4f;
                _reloadTimeCostText.text = ((int)_cannon.reloadTimeCost).ToString();
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
            if (_machinegun.UpgradeReloadTime())
            {
                Points.Instance.points -= (int)_machinegun.reloadTimeCost;
                UIManager.Instance.UpdateGoldValue();
                _machinegun.reloadTimeCost *= 1.3f;
                _reloadTimeCostText.text = ((int)_machinegun.reloadTimeCost).ToString();
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
            if (_heavyCannon.UpgradeReloadTime())
            {
                Points.Instance.points -= (int)_heavyCannon.reloadTimeCost;
                UIManager.Instance.UpdateGoldValue();
                _heavyCannon.reloadTimeCost *= 1.2f;
                _reloadTimeCostText.text = ((int)_heavyCannon.reloadTimeCost).ToString();
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
            Points.Instance.points -= (int)_cannon.ammoCapacityCost;
            _cannon.UpgradeAmmoCapacity();
            _cannon.ammoCapacityLevel++;
            _ammoCapacityCostText.text = ((int)_cannon.ammoCapacityCost).ToString();     
            _ammoCapacityButtonText.text = _cannon.ammoCapacityLevel.ToString();
            UIManager.Instance.UpdateGoldValue();
            UIManager.Instance.UpdateAmmoText();
        }  
        else if (_activeWeapon == WeaponrySelect.Machinegun && Points.Instance.points >= _machinegun.ammoCapacityCost)
        {
            Points.Instance.points -= (int)_machinegun.ammoCapacityCost;
            _machinegun.UpgradeAmmoCapacity();
            _machinegun.ammoCapacityLevel++;
            _ammoCapacityCostText.text = ((int)_machinegun.ammoCapacityCost).ToString();
            _ammoCapacityButtonText.text = _machinegun.ammoCapacityLevel.ToString();
            UIManager.Instance.UpdateGoldValue();
            UIManager.Instance.UpdateAmmoText();
        }
        else if (_activeWeapon == WeaponrySelect.HeavyCannon && Points.Instance.points >= _heavyCannon.ammoCapacityCost)
        {   
            Points.Instance.points -= (int)_heavyCannon.ammoCapacityCost;
            _heavyCannon.UpgradeAmmoCapacity();
            _heavyCannon.ammoCapacityLevel++;
            _ammoCapacityCostText.text = ((int)_heavyCannon.ammoCapacityCost).ToString();
            _ammoCapacityButtonText.text = _heavyCannon.ammoCapacityLevel.ToString();
            UIManager.Instance.UpdateGoldValue();
            UIManager.Instance.UpdateAmmoText();
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

    void RecalculateFireRate()
    {
        _cannon.fireRateCost *= Mathf.Pow(1.4f, _cannon.fireRateLevel);
        _cannon.fireRate *= Mathf.Pow(.9f, _cannon.fireRateLevel);

        _machinegun.fireRateCost *= Mathf.Pow(1.3f, _machinegun.fireRateLevel);
        _machinegun.fireRate *= Mathf.Pow(.9f, _machinegun.fireRateLevel);

        _heavyCannon.fireRateCost *= Mathf.Pow(1.4f, _heavyCannon.fireRateLevel);
        _heavyCannon.fireRate *= Mathf.Pow(.9f, _heavyCannon.fireRateLevel);
    }

    void RecalculateReloadTime()
    {
        _cannon.reloadTimeCost *= Mathf.Pow(1.4f, _cannon.reloadTimeLevel);
        _cannon.reloadTime *= Mathf.Pow(.9f, _cannon.reloadTimeLevel);

        _machinegun.reloadTimeCost *= Mathf.Pow(1.3f, _machinegun.reloadTimeLevel);
        _machinegun.reloadTime *= Mathf.Pow(.9f, _machinegun.reloadTimeLevel);

        _heavyCannon.reloadTimeCost *= Mathf.Pow(1.2f, _heavyCannon.reloadTimeLevel);
        _heavyCannon.reloadTime *= Mathf.Pow(.9f, _heavyCannon.reloadTimeLevel);
    }

    void RecalculateTurretCost()
    {
        _armorCost *= Mathf.Pow(1.4f, _armorLevel);
        _autorepairCost *= Mathf.Pow(1.4f, _autorepairLevel);
    }

    void Save()
    {
        SaveSystem.SaveData(nameof(_towerObjects), _towerObjects);
        SaveSystem.SaveData(nameof(_cannonBought), _cannonBought);
        SaveSystem.SaveData(nameof(_heavyCannonBought), _heavyCannonBought);
        //Weaponry
        SaveSystem.SaveData(nameof(_cannon.maxAmmoCapacity) + "c", _cannon.maxAmmoCapacity);
        SaveSystem.SaveData(nameof(_cannon.fireRateLevel) + "c", _cannon.fireRateLevel);
        SaveSystem.SaveData(nameof(_cannon.reloadTimeLevel) + "c", _cannon.reloadTimeLevel);
        SaveSystem.SaveData(nameof(_cannon.ammoCapacityCost) + "c", _cannon.ammoCapacityCost);
        SaveSystem.SaveData(nameof(_cannon.ammoCapacityLevel) + "c", _cannon.ammoCapacityLevel);
        SaveSystem.SaveData(nameof(_machinegun.maxAmmoCapacity) + "m", _machinegun.maxAmmoCapacity);
        SaveSystem.SaveData(nameof(_machinegun.fireRateLevel) + "m", _machinegun.fireRateLevel);
        SaveSystem.SaveData(nameof(_machinegun.reloadTimeLevel) + "m", _machinegun.reloadTimeLevel);
        SaveSystem.SaveData(nameof(_machinegun.ammoCapacityCost) + "m", _machinegun.ammoCapacityCost);
        SaveSystem.SaveData(nameof(_machinegun.ammoCapacityLevel) + "m", _machinegun.ammoCapacityLevel);
        SaveSystem.SaveData(nameof(_heavyCannon.maxAmmoCapacity) + "h", _heavyCannon.maxAmmoCapacity);
        SaveSystem.SaveData(nameof(_heavyCannon.fireRateLevel) + "h", _heavyCannon.fireRateLevel);
        SaveSystem.SaveData(nameof(_heavyCannon.reloadTimeLevel) + "h", _heavyCannon.reloadTimeLevel);
        SaveSystem.SaveData(nameof(_heavyCannon.ammoCapacityCost) + "h", _heavyCannon.ammoCapacityCost);
        SaveSystem.SaveData(nameof(_heavyCannon.ammoCapacityLevel) + "h", _heavyCannon.ammoCapacityLevel);
        SaveSystem.SaveData(nameof(_activeWeapon), _activeWeapon);
        //Turret
        SaveSystem.SaveData(nameof(_armorLevel), _armorLevel);
        SaveSystem.SaveData(nameof(_autorepairLevel), _autorepairLevel);
    }

    void Load()
    {
        for(int i = 0; i < _towerObjects.Length; i++)
        {
            if (SaveSystem.LoadData(nameof(_towerObjects) + i, _towerObjects)) 
            { 
                _towerObjects[i].SetActive(true);
                GameObject.Find(i.ToString()).SetActive(false);
            }
        }
        _cannonBought = SaveSystem.LoadData(nameof(_cannonBought), _cannonBought);
        _heavyCannonBought = SaveSystem.LoadData(nameof(_heavyCannonBought), _heavyCannonBought);
        //Weaponry
        _cannon.maxAmmoCapacity = SaveSystem.LoadData(nameof(_cannon.maxAmmoCapacity) + "c", _cannon.maxAmmoCapacity);
        _cannon.fireRateLevel = SaveSystem.LoadData(nameof(_cannon.fireRateLevel) + "c", _cannon.fireRateLevel);
        _cannon.reloadTimeLevel = SaveSystem.LoadData(nameof(_cannon.reloadTimeLevel) + "c", _cannon.reloadTimeLevel);
        _cannon.ammoCapacityCost = SaveSystem.LoadData(nameof(_cannon.ammoCapacityCost) + "c", _cannon.ammoCapacityCost);
        _cannon.ammoCapacityLevel = SaveSystem.LoadData(nameof(_cannon.ammoCapacityLevel) + "c", _cannon.ammoCapacityLevel);
        _machinegun.maxAmmoCapacity = SaveSystem.LoadData(nameof(_machinegun.maxAmmoCapacity) + "m", _machinegun.maxAmmoCapacity);
        _machinegun.fireRateLevel = SaveSystem.LoadData(nameof(_machinegun.fireRateLevel) + "m", _machinegun.fireRateLevel);
        _machinegun.reloadTimeLevel = SaveSystem.LoadData(nameof(_machinegun.reloadTimeLevel) + "m", _machinegun.reloadTimeLevel);
        _machinegun.ammoCapacityCost = SaveSystem.LoadData(nameof(_machinegun.ammoCapacityCost) + "m", _machinegun.ammoCapacityCost);
        _machinegun.ammoCapacityLevel = SaveSystem.LoadData(nameof(_machinegun.ammoCapacityLevel) + "m", _machinegun.ammoCapacityLevel);
        _heavyCannon.maxAmmoCapacity = SaveSystem.LoadData(nameof(_heavyCannon.maxAmmoCapacity) + "h", _heavyCannon.maxAmmoCapacity);
        _heavyCannon.fireRateLevel = SaveSystem.LoadData(nameof(_heavyCannon.fireRateLevel) + "h", _heavyCannon.fireRateLevel);
        _heavyCannon.reloadTimeLevel = SaveSystem.LoadData(nameof(_heavyCannon.reloadTimeLevel) + "h", _heavyCannon.reloadTimeLevel);
        _heavyCannon.ammoCapacityCost = SaveSystem.LoadData(nameof(_heavyCannon.ammoCapacityCost) + "h", _heavyCannon.ammoCapacityCost);
        _heavyCannon.ammoCapacityLevel = SaveSystem.LoadData(nameof(_heavyCannon.ammoCapacityLevel) + "h", _heavyCannon.ammoCapacityLevel);
        _activeWeapon = SaveSystem.LoadData(nameof(_activeWeapon), _activeWeapon);
        RecalculateFireRate();
        RecalculateReloadTime();
        //Turret
        _armorLevel = SaveSystem.LoadData(nameof(_armorLevel), _armorLevel);
        _autorepairLevel = SaveSystem.LoadData(nameof(_autorepairLevel), _autorepairLevel);
        _turretScript.RecalculateAutorepair(_autorepairLevel);
        _turretScript.RecalculateTurretArmor(_armorLevel);
        RecalculateTurretCost();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
