using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public float Speed = 2.0f;
    private Vector2 _moveVector = Vector2.zero;
    private Vector3 finalVectorForward;
    private Vector3 finalVectorRight;

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
        finalVectorForward = Vector3.zero;
        finalVectorRight = Vector3.zero;
        if (_moveVector != Vector2.zero || moveVector != Vector2.zero)
        {
            if(_moveVector.y > 0)
            {
                animator.SetBool("isWalkingBackwards", false);
                finalVectorForward = playerBody.transform.forward * Speed;
                animator.SetBool("isWalking", true);
            }
            else if (_moveVector.y < 0)
            {
                animator.SetBool("isWalking", false);
                finalVectorForward = -(playerBody.transform.forward) * Speed;
                animator.SetBool("isWalkingBackwards", true);
            }
            if (_moveVector.x > 0)
            {
                finalVectorRight = playerBody.transform.right * Speed;
                animator.SetBool("isWalking", true);
            }
            else if (_moveVector.x < 0)
            {
                finalVectorRight = -(playerBody.transform.right) * Speed;
                animator.SetBool("isWalking", true);
            }
            playerBody.velocity = finalVectorForward + finalVectorRight;
            //if (_moveVector.y == 1 || moveVector.y == 1)
            //{
            //    playerBody.velocity = playerBody.transform.forward * Speed;
            //    animator.SetBool("isWalking", true);
            //}
            //else if (_moveVector.y == -1 || moveVector.y == -1)
            //{
            //    playerBody.velocity = -(playerBody.transform.forward) * Speed;
            //    animator.SetBool("isWalkingBackwards", true);
            //}
            //if (_moveVector.x == 1 || moveVector.x == 1)
            //{
            //    playerBody.velocity = playerBody.transform.right * Speed;
            //    animator.SetBool("isWalking", true);
            //}
            //else if (_moveVector.x == -1 || moveVector.x == -1)
            //{
            //    playerBody.velocity = -(playerBody.transform.right) * Speed;
            //    animator.SetBool("isWalking", true);

            //}

        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingBackwards", false);
        }
        //playerBody.velocity = new Vector3(_moveVector.x, 0.0f, _moveVector.y) * Speed;
    }
}
