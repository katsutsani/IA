using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputSystem : MonoBehaviour
{

    public GameObject moveObject;
    public GameObject Shoot;
    public GameObject Aim;
    public GameObject MeleeAttack;

    Move moveScript;
    MeleeAttack attackScript;
    LookAt rotationScript;

    public Rigidbody CharacterBody;
    public float Speed = 2.0f;
    private PlayerInput playerInput;
    PlayerInputActions input;
    public Animator animator;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        input = new PlayerInputActions();
        moveScript = moveObject.GetComponent<Move>();
        rotationScript = Aim.GetComponent<LookAt>();

    }

    private void Update()
    {
        moveScript.HandleMovement(CharacterBody, animator);
        rotationScript.Rotate(CharacterBody.GetComponent<Transform>());
    }

    void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += moveScript.OnMovementPerformed;
        input.Player.Move.canceled += moveScript.OnMovementCancelled;


        //input.Player.Attack.performed += ctx =>
        //{
        //    isAttacking = true;
        //    // Add animation attack
        //};
        //input.Player.Attack.canceled += ctx =>
        //{
        //    isAttacking = false;
        //    // back to idle or movement

        //};

    }

    void OnDisable()
    {
        input.Player.Disable();
        input.Player.Move.performed -= moveScript.OnMovementPerformed;
        input.Player.Move.canceled -= moveScript.OnMovementCancelled;
    }

}
