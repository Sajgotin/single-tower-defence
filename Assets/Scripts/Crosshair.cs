using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
    private Controls _controls;
    private PlayerInput _playerInput;
    private GameObject _crosshair;
    private Vector2 _moveInput;

    private void Awake()
    {
        _controls = new Controls();
        _playerInput = GameObject.FindGameObjectWithTag("Tower").GetComponent<PlayerInput>();
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
        _crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        CrosshairToMousePos();
        CrosshairBoundaries();
    }

    void CrosshairToMousePos()
    {
        if (_crosshair != null)
        {
            _moveInput = _controls.Player.Move.ReadValue<Vector2>();
            _crosshair.transform.position = new Vector3(_crosshair.transform.position.x + _moveInput.x * Time.deltaTime, 
                                                        _crosshair.transform.position.y + _moveInput.y * Time.deltaTime, 0);
        }
        else
        {
            Debug.LogWarning("No crosshair detected");
        }
    }

    void CrosshairBoundaries()
    {
        if (Camera.main.WorldToScreenPoint(_crosshair.transform.position).x > Screen.width)
        {  
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
            screenToWorld.y = _crosshair.transform.position.y;
            _crosshair.transform.position = screenToWorld;
        }
        if(Camera.main.WorldToScreenPoint(_crosshair.transform.position).x < 0)
        {
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            screenToWorld.y = _crosshair.transform.position.y;
            _crosshair.transform.position = screenToWorld;
        }
        if(Camera.main.WorldToScreenPoint(_crosshair.transform.position).y > Screen.height)
        {
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
            screenToWorld.x = _crosshair.transform.position.x;
            _crosshair.transform.position = screenToWorld;
        }
        if(Camera.main.WorldToScreenPoint(_crosshair.transform.position).y < 0)
        {
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            screenToWorld.x = _crosshair.transform.position.x;
            _crosshair.transform.position = screenToWorld;
        }
    }
}
