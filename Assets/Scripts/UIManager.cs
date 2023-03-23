using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _currentWaveText;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private Slider _towerHealthSlider;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private GameObject _crosshair;
    [SerializeField] private Shooting _shootingScript;

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
    }

    private void Start()
    {
        _shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        UpdateAmmoText();
        _towerHealthSlider.maxValue = GameManager.Instance.maxTowerHealth;
        _towerHealthSlider.value = GameManager.Instance.towerHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        }
        else
        {
            if (!_upgradeMenu.activeSelf)
            {
                Time.timeScale = 1;
                _crosshair.SetActive(true);
                Cursor.visible = false;
                _shootingScript.PrepareWeapon();
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
        }
        else
        {
            if (!_pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                _crosshair.SetActive(true);
                Cursor.visible = false;
            }
        }
    }
}
