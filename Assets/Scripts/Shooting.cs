using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject _explosion;
    [SerializeField] GameObject _muzzleFlash;
    [SerializeField] Transform _muzzleFlashPos;
    [SerializeField] Upgrades _upgradesScript;
    [SerializeField] Weaponry _cannon, _machinegun, _heavyCannon;
    [SerializeField] SpriteRenderer _spriteRendererWeapon;
    [SerializeField] float _fireRate;
    [SerializeField] float _reloadTime;
    [SerializeField] float _timer;
    [SerializeField] int _maxAmmoCapacity;
    [SerializeField] int _ammoCapacity;
    public int AmmoCapacity { get { return _ammoCapacity; } }
    [SerializeField] int _damage;
    public int Damage { get { return _damage; } }
    [SerializeField] bool _isReloading;

    private void Awake()
    {
        _spriteRendererWeapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<SpriteRenderer>(); 
    }

    private void Start()
    {
        _timer = _fireRate;
        PrepareWeapon();
        _ammoCapacity = _maxAmmoCapacity;
        UIManager.Instance.UpdateAmmoText();
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

            SetExplosionAndMuzzleFlashScale();
            Instantiate(_explosion, CalculateMouseToWorldPosition(), Quaternion.identity);
            Instantiate(_muzzleFlash, _muzzleFlashPos.position, _muzzleFlashPos.rotation);
            SoundController.Instance.PlayExplosion();
        }
    }

    void SetExplosionAndMuzzleFlashScale()
    {
        if (_upgradesScript.ActiveWeapon == Upgrades.WeaponrySelect.Machinegun)
        {
            _muzzleFlash.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            _explosion.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }
        else if (_upgradesScript.ActiveWeapon == Upgrades.WeaponrySelect.Cannon)
        {
            _muzzleFlash.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _explosion.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (_upgradesScript.ActiveWeapon == Upgrades.WeaponrySelect.HeavyCannon)
        {
            _muzzleFlash.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            _explosion.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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

    public void PrepareWeapon()
    {
        if (_upgradesScript.ActiveWeapon == Upgrades.WeaponrySelect.Cannon)
        {
            _spriteRendererWeapon.sprite = _cannon.weaponSprite;
            _fireRate = _cannon.fireRate;
            _reloadTime = _cannon.reloadTime;
            _maxAmmoCapacity = _cannon.maxAmmoCapacity;
            _damage = _cannon.damage;
            if (_ammoCapacity > _maxAmmoCapacity) _ammoCapacity = _maxAmmoCapacity;
        }
        else if (_upgradesScript.ActiveWeapon == Upgrades.WeaponrySelect.Machinegun)
        {
            _spriteRendererWeapon.sprite = _machinegun.weaponSprite;
            _fireRate = _machinegun.fireRate;
            _reloadTime = _machinegun.reloadTime;
            _maxAmmoCapacity = _machinegun.maxAmmoCapacity;
            _damage = _machinegun.damage;
            if (_ammoCapacity > _maxAmmoCapacity) _ammoCapacity = _maxAmmoCapacity;
        }
        else if (_upgradesScript.ActiveWeapon == Upgrades.WeaponrySelect.HeavyCannon)
        {
            _spriteRendererWeapon.sprite = _heavyCannon.weaponSprite;
            _fireRate = _heavyCannon.fireRate;
            _reloadTime = _heavyCannon.reloadTime;
            _maxAmmoCapacity = _heavyCannon.maxAmmoCapacity;
            _damage = _heavyCannon.damage;
            if (_ammoCapacity > _maxAmmoCapacity) _ammoCapacity = _maxAmmoCapacity;
        }
    }

    Vector3 CalculateMouseToWorldPosition()
    {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenToWorld.z = 0;

        return screenToWorld;
    }
}
