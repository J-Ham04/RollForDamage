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

    Rigidbody2D rb;
    Animator anim;
    Vector2 move;
    public float stunned = 0f;

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
        HandleInput();
        HandleFacingDirection();
        HandleAnimations();
    }

    private void HandleInput()
    {
        controls.Gameplay.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movementInput = Vector2.zero;

        move = movementInput * moveSpeed;
    }

    void HandleFacingDirection()
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
        if(stunned > 0)
        {
            stunned -= Time.fixedDeltaTime;
        }else rb.velocity = move;

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
