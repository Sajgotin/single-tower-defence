using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private Controls _controls;
    [SerializeField] TextMeshProUGUI _goldText;
    [SerializeField] TextMeshProUGUI _currentWaveText;
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] Slider _towerHealthSlider;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _settingsMenu;
    [SerializeField] GameObject _settingsButton;
    [SerializeField] GameObject _upgradeMenu;
    [SerializeField] GameObject _crosshair;
    [SerializeField] Shooting _shootingScript;
    [SerializeField] Upgrades _upgradesScript;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        } 
        _controls = new Controls();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();   
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    private void Start()
    {
        _shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        UpdateAmmoText();
        UpdateGoldValue();
        _towerHealthSlider.maxValue = GameManager.Instance.maxTowerHealth;
        _towerHealthSlider.value = GameManager.Instance.towerHealth;
    }

    private void Update()
    {
        if (_controls.Player.Pause.triggered)
        {
            TogglePauseMenu();
        }
    }

    public void UpdateGoldValue()
    {
        _goldText.text = "Gold: " + Points.Instance.points;
    }

    public void UpdateTowerHealthSlider()
    {
        _towerHealthSlider.maxValue = GameManager.Instance.maxTowerHealth;
        _towerHealthSlider.value = GameManager.Instance.towerHealth;
    }

    public void UpdateWaveText()
    {
        _currentWaveText.text = "Wave: " + EnemySpawner.Instance.WaveCount;
    }

    public void UpdateAmmoText()
    {
        _ammoText.text = "Ammo: " + _shootingScript.AmmoCapacity;
    }

    void TogglePauseMenu()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        if (_pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
            _crosshair.SetActive(false);
            Cursor.visible = true;
            EventSystem.current.SetSelectedGameObject(_settingsButton);
        }
        else
        {
            if (!_upgradeMenu.activeSelf)
            {
                Time.timeScale = 1;
                _crosshair.SetActive(true);
                Cursor.visible = false;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("UpgradeMenu").GetComponent<Upgrades>().FireRateButton.gameObject);
            }
        }
    }

    public void ToggleUpgradeMenu()
    {
        _upgradeMenu.SetActive(!_upgradeMenu.activeSelf);
        if (_upgradeMenu.activeSelf)
        {
            Time.timeScale = 0;
            _crosshair.SetActive(false);
            Cursor.visible = true;
            _upgradesScript.UpdateShopData();
            //For Gamepad input probably
            EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("UpgradeMenu").GetComponent<Upgrades>().FireRateButton.gameObject);
        }
        else
        {
            _shootingScript.PrepareWeapon();
            if (!_pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                _crosshair.SetActive(true);
                Cursor.visible = false;
            }
        }
    }

    public void SettingsButton()
    {
        _settingsMenu.SetActive(true);
    }

    public void BackButton()
    {
        EventSystem.current.SetSelectedGameObject(_settingsButton);
        _settingsMenu.SetActive(false);
    }
}
