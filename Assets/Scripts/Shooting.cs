using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private Transform _muzzleFlashPos;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _timer;

    private void Start()
    {
        _timer = _fireRate;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        Shot();
    }

    void Shot()
    {
        if (Input.GetMouseButton(0) && _timer > _fireRate && Time.timeScale > 0)
        {
            _timer = 0;

            Instantiate(_explosion, CalculateMouseToWorldPosition(), Quaternion.identity);
            Instantiate(_muzzleFlash, _muzzleFlashPos.position, _muzzleFlashPos.rotation);
        }
        
    }

    Vector3 CalculateMouseToWorldPosition()
    {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenToWorld.z = 0;

        return screenToWorld;
    }
}
