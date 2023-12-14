using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MeleeAttack : MonoBehaviour
{
    private bool isAttacking = false;

    public void Attack()
    {
        isAttacking = true;
        // Add animation attack
    }

    public void EndAttack()
    {
        isAttacking = false;
        // back to idle or movement
    }
}
