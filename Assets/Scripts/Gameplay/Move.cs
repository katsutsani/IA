using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    public float Speed = 2.0f;
    private Vector2 _moveVector = Vector2.zero;
    private Vector3 finalVectorForward;
    private Vector3 finalVectorRight;
    private void Awake()
    {
        gameObject.SetActive(true);
    }
    public void OnMovementPerformed(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>();
    }

    public void OnMovementCancelled(InputAction.CallbackContext context)
    {
        _moveVector = Vector2.zero;

    }

    public void HandleMovement(Rigidbody playerBody, Animator animator, Vector2 moveVector)
    {
        if (_moveVector != Vector2.zero || moveVector != Vector2.zero)
        {
            if(_moveVector.y > 0)
            {
                animator.SetBool("isWalkingBackwards", false);
                animator.SetBool("isWalking", true);
            }
            else if (_moveVector.y < 0)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBackwards", true);
            }
            if (_moveVector.x > 0)
            {
                animator.SetBool("isWalking", true);
            }
            else if (_moveVector.x < 0)
            {
                animator.SetBool("isWalking", true);
            }

        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingBackwards", false);
        }
        playerBody.velocity = new Vector3(_moveVector.x, 0.0f, _moveVector.y) * Speed;
    }
}
