using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject _explosion;
    [SerializeField] GameObject _muzzleFlash;
    [SerializeField] Transform _muzzleFlashPos;
    [SerializeField] float _fireRate;
    [SerializeField] float _reloadTime;
    [SerializeField] float _timer;
    [SerializeField] int _maxAmmoCapacity;
    [SerializeField] int _ammoCapacity;
    public int AmmoCapacity { get { return _ammoCapacity; } }
    [SerializeField] int _damage;
    public int Damage { get { return _damage; } }
    [SerializeField] bool _isReloading;

    private void Start()
    {
        _timer = _fireRate;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        CheckAmmoCapacity();

        Shot();
    }

    void Shot()
    {
        if (Input.GetMouseButton(0) && _timer > _fireRate && Time.timeScale > 0 && _ammoCapacity > 0)
        {
            _timer = 0;
            _ammoCapacity--;
            UIManager.Instance.UpdateAmmoText();

            Instantiate(_explosion, CalculateMouseToWorldPosition(), Quaternion.identity);
            Instantiate(_muzzleFlash, _muzzleFlashPos.position, _muzzleFlashPos.rotation);
        }
        
    }

    void CheckAmmoCapacity()
    {
        if (_ammoCapacity <= 0) _isReloading = true;
        else _isReloading = false;
        if (_isReloading) ReloadWeapon();
    }

    void ReloadWeapon()
    {
        if (_timer >= _reloadTime)
        {
            _ammoCapacity = _maxAmmoCapacity;
            UIManager.Instance.UpdateAmmoText();
        }  
    }

    Vector3 CalculateMouseToWorldPosition()
    {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenToWorld.z = 0;

        return screenToWorld;
    }

    public void UpgradeFireRate()
    {
        _fireRate *= .9f;
    }

    public void UpgradeReloadTime()
    {
        _reloadTime *= .9f;
    }

    public void UpgradeAmmoCapacity()
    {
        _maxAmmoCapacity++;
        _ammoCapacity++;
    }
}
