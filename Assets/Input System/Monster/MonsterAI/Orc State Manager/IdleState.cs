using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    public ChaseState chaseState;
    public PatrolState patrolState;

    public GameObject brain;

    public NavMeshAgent _agent;

    public bool canHearThePlayer;

    public Animator orcAnimator;

    public int noiseLevel;

    public override State RunCurrentState()
    {
        noiseLevel = brain.GetComponent<PlayerInputSystem>().noiseLevel;
        patrolState.isWaiting = false;
        orcAnimator.SetBool("isWaiting", false);

        if (noiseLevel > 0)
        {
            canHearThePlayer = true;
            orcAnimator.SetBool("canHearThePlayer", true);
        }
        if (canHearThePlayer)
        {
            return chaseState;
        }
        if (!patrolState.isWaiting)
        {
            return patrolState;
        }
        else return this;
    }
}
