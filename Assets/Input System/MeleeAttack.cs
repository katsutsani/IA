using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MeleeAttack : MonoBehaviour
{
    float currentClipLength;
    Coroutine _animation;
    Coroutine _checkCollision;
    public void HandleAttack(Collider Weapon, Animator animator)
    {
        animator.SetBool("isAttacking", true);
        Weapon.enabled = true;
        AnimatorClipInfo[] currentAnimation = animator.GetCurrentAnimatorClipInfo(0);
        currentClipLength = currentAnimation[0].clip.length;
        _animation = StartCoroutine(StopAnimation(Weapon,animator));
        _checkCollision = StartCoroutine(CheckCollision(Weapon));

    }

    IEnumerator StopAnimation(Collider Weapon, Animator animator)
    {
        yield return new WaitForSeconds(currentClipLength-0.5f);
        Weapon.enabled = false;
        animator.SetBool("isAttacking", false);
        StopCoroutine(_checkCollision);
        StopCoroutine(_animation);
    }

    IEnumerator CheckCollision(Collider Weapon)
    {
        if (Weapon.isTrigger)
        {
            Debug.Log("test");
        }

        yield return null;
    }
}
