using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    public Transform _transform;
    public Transform[] _waypoints;

    public ChaseState chaseState;
    public IdleState idleState;

    public bool isWaiting;/*
    private float waitTime = 1.0f;
    private float waitCounter = 0.0f;*/

    private int currentWaypointIndex = -1;
    private int lastWaypointIndex = -1;

    public override State RunCurrentState()
    {
        idleState.noiseLevel = idleState.brain.GetComponent<PlayerInputSystem>().noiseLevel;
        float distance = Vector3.Distance(chaseState._transform.position, chaseState.idleState.patrolState._transform.position);
        if (idleState.noiseLevel > 0 && distance < 10)
        {
            idleState.canHearThePlayer = true;
            idleState.orcAnimator.SetBool("canHearThePlayer", true);
        }
        else
        {
            idleState.brain.GetComponent<PlayerInputSystem>().noiseLevel = 0;
        }
        if (currentWaypointIndex < 0) 
        {
            currentWaypointIndex = Random.Range(0, _waypoints.Length - 1);
            if (currentWaypointIndex == lastWaypointIndex)
            {
                if (lastWaypointIndex != _waypoints.Length - 1)
                {
                    currentWaypointIndex++;
                }
                else
                {
                    currentWaypointIndex--;
                }
            }
        }
        if (idleState.canHearThePlayer)
        {
            return chaseState;
        }
        if (isWaiting)
        {
            return idleState;
        }
        Transform wp = _waypoints[currentWaypointIndex];
        if (Vector3.Distance(_transform.position, wp.position) < 1f)
        {
            _transform.position = wp.position;/*
            waitCounter = 0.0f;*/
            isWaiting = true;
            idleState.orcAnimator.SetBool("isWaiting", false);

            lastWaypointIndex = currentWaypointIndex;
            currentWaypointIndex = -1;
        }
        else
        {
            idleState._agent.SetDestination(wp.position);
        }
        return this;
    }
}
