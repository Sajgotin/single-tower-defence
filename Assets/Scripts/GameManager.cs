using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject _upgradeMenu;
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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _upgradeMenu.SetActive(!_upgradeMenu.activeSelf);
            if (_upgradeMenu.activeSelf) 
            {
                Time.timeScale = 0;
                _crosshair.SetActive(false);
            }
            else
            { 
                Time.timeScale = 1; 
                _crosshair.SetActive(true);
            }
        }

        if (towerHealth <= 0) Debug.Log("Tower destroyed!");
    }
}
