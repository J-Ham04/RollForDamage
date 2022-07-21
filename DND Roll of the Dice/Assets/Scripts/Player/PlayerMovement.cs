using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;
    Vector2 movementInput;

    // FIELDS
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedCap;

    Rigidbody2D rb;
    Animator anim;
    public float stunned = 0f;

    private bool usingController;

    // PROPERTIES
    public float curSpeed => rb.velocity.magnitude;

    // METHODS
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        controls = new PlayerControls();
    }

    private void Update()
    {
        usingController = controls.Gameplay.AimPositionJoystick.IsPressed();

        HandleInput();

        if (usingController == true)
        {
            HandleFacingDirectionController(controls.Gameplay.AimPositionJoystick.ReadValue<Vector2>());
        }
        else HandleFacingDirectionMouse();

        HandleAnimations();
    }

    private void HandleInput()
    {
        controls.Gameplay.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movementInput = Vector2.zero;
    }

    void HandleFacingDirectionMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        if (mousePos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else if (mousePos.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void HandleFacingDirectionController(Vector2 jPos)
    {
        Vector2 mousePos = jPos * 10;

        if (mousePos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (mousePos.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void HandleAnimations()
    {
        if (curSpeed >= 0.01)
        {
            anim.SetBool("moving", true);
        }else
        {
            anim.SetBool("moving", false);
        }
    }

    private void FixedUpdate()
    {
        if (stunned > 0)
        {
            stunned -= Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity += movementInput * moveSpeed;
        }

        if (curSpeed > moveSpeedCap)
        {
            float reduction = moveSpeedCap / curSpeed;
            rb.velocity *= reduction;
        }

    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
