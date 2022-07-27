using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : InputManager
{
    // FIELDS
    [Header("Speed Variables")]
    [SerializeField] private float moveSpeed = 7.5f;
    [SerializeField] private float maxSpeed = 7.5f;
    [Range(0f, 1f)]
    [SerializeField] private float inputSmoothing;

    [Header("Other")]
    private float timeUntilEndOfStun;

    [Header("Visuals")]
    [SerializeField] private float moveAnimationTolerance = 0.1f;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 constantMovementInput;
    private Vector2 movementInput;
    private Vector2 movementDirection;

    // PROPERTIES
    public float curSpeed => rb.velocity.magnitude;
    public bool stunned
    {
        get
        {
            if (timeUntilEndOfStun > 0)
            {
                return true;
            }
            return false;
        }
    }
    public Vector2 moveInput
    {
        get
        {
            return movementInput;
        }
    }
    public Vector2 constantMoveInput
    {
        get
        {
            return constantMovementInput;
        }
    }

    // METHODS
    private void Awake()
    {
        base.Awake();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movementInput = Vector2.zero;

        controls.Gameplay.Move.performed += ctx => constantMovementInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        base.Update();

        HandleAnimations();

        if (usingController == true)
        {
            Vector2 joyStickPos = controls.Gameplay.AimPositionJoystick.ReadValue<Vector2>() * 100;
            HandleFacingDirection(joyStickPos);
        }
        else
        {
            HandleFacingDirection(mousePos);
        }

    }

    private void FixedUpdate()
    {
        if (stunned == true)
        {
            timeUntilEndOfStun -= Time.deltaTime;
            return;
        }

        Move();
        CapMovementVelocity();
    }

    private void HandleFacingDirection(Vector2 targetPos)
    {
        if (targetPos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (targetPos.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void HandleAnimations()
    {
        if (curSpeed >= moveAnimationTolerance)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }

    private void Move()
    {
        SmoothInput();
        rb.velocity = movementDirection * moveSpeed;
    }
    private void SmoothInput()
    {
        movementDirection = Vector2.Lerp(movementInput, movementDirection, inputSmoothing);
    }
    private void CapMovementVelocity()
    {
        if (curSpeed <= maxSpeed)
        {
            return;
        }

        float reduction = maxSpeed / curSpeed;
        rb.velocity *= reduction;
    }

    public void Stun(float stunTime)
    {
        timeUntilEndOfStun = stunTime;
    }
}
