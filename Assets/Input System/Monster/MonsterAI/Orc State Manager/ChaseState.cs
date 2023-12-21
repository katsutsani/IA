using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ChaseState : State
{
    public Transform _transform;

    public IdleState idleState;
    public AttackState attackState;

    public override State RunCurrentState()
    {
        idleState._agent.SetDestination(_transform.position);
        float distance = Vector3.Distance(_transform.position, idleState.patrolState._transform.position);
        if (distance <= 2f)
        {
            idleState.orcAnimator.SetBool("canAttack", true);
            return attackState;
        }
        if (!idleState.canHearThePlayer)
        {
            return idleState;
        }
        else return this;
        
    }
}