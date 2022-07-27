using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    protected static bool usingController;
    protected PlayerControls controls;
    protected Vector2 mousePos;

    protected void Awake()
    {
        controls = new PlayerControls();
    }

    protected void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (controls.Gameplay.GamepadDetection.IsPressed())
        {
            usingController = true;
        }else if (controls.Gameplay.KeyboardDetection.IsPressed())
        {
            usingController = false;
        }
    }

    protected void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    protected void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
