using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public float Speed = 2.0f;
    private Vector2 moveVector = Vector2.zero;

    public void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;

    }

    public void HandleMovement(Rigidbody playerBody, Animator animator)
    {
        if(moveVector != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);

        }
        playerBody.velocity = new Vector3(moveVector.x, 0.0f, moveVector.y) * Speed;
    }
}
