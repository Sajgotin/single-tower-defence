using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBarSlider;
    private Enemy _enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        _enemyScript = GetComponent<Enemy>();
        _healthBarSlider.maxValue = _enemyScript.MaxHealth;
        _healthBarSlider.value = _enemyScript.Health;
    }

    private void Update()
    {
        FollowTarget();
    }

    public void UpdateHealthBar()
    {
        if(!_healthBarSlider.gameObject.activeSelf) _healthBarSlider.gameObject.SetActive(true);
        _healthBarSlider.value = _enemyScript.Health;
    }

    public void SetupHealthBar(Canvas canvas)
    {
        _healthBarSlider.transform.SetParent(canvas.transform);
        _healthBarSlider.gameObject.SetActive(false);
    }

    void FollowTarget()
    {
        _healthBarSlider.transform.position = new Vector2(transform.position.x, transform.position.y - .2f);
    }

    public void DestroyHealthBar()
    {
        Destroy(_healthBarSlider.gameObject);
    }
}
