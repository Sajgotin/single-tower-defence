using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Slider _healthBarSlider;
    private Enemy _enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        _enemyScript = GetComponent<Enemy>();
        _healthBarSlider.maxValue = _enemyScript.MaxHealth;
        _healthBarSlider.value = _enemyScript.Health;
    }

    public void UpdateHealthBar()
    {
        if(!_healthBar.activeSelf) _healthBar.SetActive(true);
        _healthBarSlider.value = _enemyScript.Health;
    }
}
