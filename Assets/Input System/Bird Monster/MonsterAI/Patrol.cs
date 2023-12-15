using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Patrol : Node
{
    private Transform transform;
    private Transform[] waypoints;
    private Animator animator;

    private int currentWaypointIndex = 0;

    private bool isWaiting = true;
    private float waitTime = 1.0f;
    private float waitCounter = 0.0f;

    public Patrol(Transform transform, Transform[] waypoints) 
    { 
        animator = transform.GetComponent<Animator>();
        this.transform = transform;
        this.waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        if (isWaiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                isWaiting = false;
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            Transform wp = waypoints[currentWaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 1f)
            {
                transform.position = wp.position;
                waitCounter = 0.0f;
                isWaiting = true;

                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                animator.SetBool("isWalking", true);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, MonsterData.speed * Time.deltaTime);
                transform.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING; 
        return state;
    }
}
