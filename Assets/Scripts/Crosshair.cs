using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private GameObject _crosshair;
    // Start is called before the first frame update
    void Start()
    {
        _crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        CrosshairToMousePos();
    }

    void CrosshairToMousePos()
    {
        if (_crosshair != null)
        {
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenToWorld.z = 0;
            _crosshair.transform.position = screenToWorld;
        }
        else
        {
            Debug.LogWarning("No crosshair detected");
        }
    }
}
