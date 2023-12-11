using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private Rigidbody playerBody;
    public float Speed = 2.0f;
    private PlayerInput playerInput;
    PlayerInputActions input;
    private InputAction moveAction;
    public bool isAttacking;
    private Animator animator;
    private Vector2 moveVector = Vector2.zero;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        input = new PlayerInputActions();

        input.Player.Attack.performed += ctx =>
        {
            isAttacking = true;
            // Add animation attack
        };
        input.Player.Attack.canceled += ctx =>
        {
            isAttacking = false;
            // back to idle or movement

        };
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        playerBody.velocity = new Vector3(moveVector.x, 0.0f, moveVector.y) * Speed;
    }

    void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;

    }

    void OnDisable()
    {
        input.Player.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;
    }

    public void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        animator.SetBool("isWalking", true);
    }

    public void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
        animator.SetBool("isWalking", false);

    }
}
