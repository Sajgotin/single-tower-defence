using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject _crosshair;
    public float maxTowerHealth = 100f;
    public float towerHealth = 100f;

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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UIManager.Instance.ToggleUpgradeMenu();
        }

        if (towerHealth <= 0) 
        {
            UIManager.Instance.ToggleUpgradeMenu();
            towerHealth = maxTowerHealth;
            UIManager.Instance.UpdateTowerHealthSlider();        
            ResetWave();
        }
    }

    void ResetWave()
    {
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in activeEnemies)
        {
            enemy.GetComponent<HealthBar>().DestroyHealthBar();
            Destroy(enemy);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
