using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    private GameObject _turret;
    private GameObject _cannon;
    private GameObject _crosshair;
    [SerializeField] float _armor;
    [SerializeField] int _autorepairValue;
    public int AutorepairValue { get { return _autorepairValue; } }
    public float Armor { get { return _armor; } }

    // Start is called before the first frame update
    void Start()
    {
        _turret = GameObject.Find("Turret");
        _cannon = GameObject.Find("Cannon");
        _crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            AimCannon();
            RotateTurret();
        }   
    }

    void AimCannon()
    {
        Vector2 direction = new Vector2(_crosshair.transform.position.x - _cannon.transform.position.x, _crosshair.transform.position.y - _cannon.transform.position.y);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _cannon.transform.eulerAngles = new Vector3(0, 0, rotation - 180);
    }

    void RotateTurret()
    {
        if (Input.mousePosition.x < Screen.width / 2) _turret.transform.rotation = Quaternion.Euler(0, 0, 0);
        else _turret.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void UpgradeTurretArmor()
    {
        _armor *= .9f;
    }

    public void UpgradeAutorepair()
    {
        _autorepairValue++;
    }
}
