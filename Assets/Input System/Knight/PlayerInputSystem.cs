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
    public GameObject Defense;

    public Collider Weapon;
    public Collider Shield;

    Move moveScript;
    MeleeAttack attackScript;
    Defend defenseScript;

    LookAt rotationScript;

    public Rigidbody CharacterBody;
    public float Speed = 2.0f;
    private PlayerInput playerInput;
    PlayerInputActions input;
    public Animator animator;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveScript = moveObject.GetComponent<Move>();
        rotationScript = Aim.GetComponent<LookAt>();
        attackScript = MeleeAttack.GetComponent<MeleeAttack>();
        if (Shield)
        {
            defenseScript = Defense.GetComponent<Defend>();

        }
        input = new PlayerInputActions();

    }

    private void Update()
    {
        moveScript.HandleMovement(CharacterBody, animator, Vector2.zero);
        rotationScript.Rotate(CharacterBody.GetComponent<Transform>());
        input.Player.Attack.started += ctx =>
        {
            attackScript.HandleAttack(Weapon,animator);
        };
        input.Player.Defend.started += ctx =>
        {
            defenseScript.HandleDefense(Shield, animator);
        };
        input.Player.Defend.canceled += ctx =>
        {
            defenseScript.StopDefending(Shield, animator);
        };
    }

    void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += moveScript.OnMovementPerformed;
        input.Player.Move.canceled += moveScript.OnMovementCancelled;




    }

    void OnDisable()
    {
        input.Player.Disable();
        input.Player.Move.performed -= moveScript.OnMovementPerformed;
        input.Player.Move.canceled -= moveScript.OnMovementCancelled;
    }

}
