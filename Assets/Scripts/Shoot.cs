using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private Transform _muzzleFlashPos;

    private void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenToWorld.z = 0;

            Instantiate(_explosion, screenToWorld, Quaternion.identity);
            Instantiate(_muzzleFlash, _muzzleFlashPos.position, _muzzleFlashPos.rotation);
        }
        
    }
}
