using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Controls _controls;
    [SerializeField] GameObject _crosshair;
    [SerializeField] Turret _turretScript;
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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        _turretScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controls.Player.Back.triggered)
        {
            UIManager.Instance.CloseOneMenu();
        }

        if (towerHealth <= 0) 
        {
            UIManager.Instance.ToggleUpgradeMenu();
            towerHealth = maxTowerHealth;
            UIManager.Instance.UpdateTowerHealthSlider();        
            ResetWave();
        }

        if (towerHealth > 0 && towerHealth < maxTowerHealth && Time.timeScale > 0) 
        { 
            AutorepairTurret();
            UIManager.Instance.UpdateTowerHealthSlider();
        }
        if (towerHealth > maxTowerHealth) towerHealth = maxTowerHealth;
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

    void AutorepairTurret()
    {
        towerHealth += _turretScript.AutorepairValue * Time.deltaTime;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        MainMenu.saveExist = true;
        SaveSystem.SaveData(nameof(MainMenu.saveExist), true);
    }
}
