using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _currentWaveText;
    [SerializeField] private Slider _towerHealthSlider;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _crosshair;

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
            Time.timeScale = 1;
            _crosshair.SetActive(true);
            Cursor.visible = false;
        }
    }
}
