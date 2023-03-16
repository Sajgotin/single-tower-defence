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
}
