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
        float distance = Vector3.Distance(chaseState._transform.position, chaseState.idleState.patrolState._transform.position);
        if (noiseLevel > 0 && distance < 10)
        {
            canHearThePlayer = true;
            orcAnimator.SetBool("canHearThePlayer", true);
        }
        else
        {
            brain.GetComponent<PlayerInputSystem>().noiseLevel = 0;
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
