using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class AttackState : State
{
    public ChaseState chaseState;

    public override State RunCurrentState()
    {
        float distance = Vector3.Distance(chaseState._transform.position, chaseState.idleState.patrolState._transform.position);
        if (distance > 2f)
        {
            chaseState.idleState.orcAnimator.SetBool("canAttack", false);
            return chaseState;
        }
        return this;
    }
}
