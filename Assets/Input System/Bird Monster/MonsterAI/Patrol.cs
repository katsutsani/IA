using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Patrol : NodeEmile
{
    private Transform _transform;
    private Transform[] _waypoints;
    private Animator _animator;

    private int currentWaypointIndex = 0;

    private bool isWaiting = true;
    private float waitTime = 1.0f;
    private float waitCounter = 0.0f;

    public Patrol(Transform transform, Transform[] waypoints) 
    { 
        _animator = transform.GetComponent<Animator>();
        this._transform = transform;
        this._waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        if (isWaiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                isWaiting = false;
                _animator.SetBool("isWalking", true);
            }
        }
        else
        {
            Transform wp = _waypoints[currentWaypointIndex];
            if (Vector3.Distance(_transform.position, wp.position) < 1f)
            {
                _transform.position = wp.position;
                waitCounter = 0.0f;
                isWaiting = true;

                currentWaypointIndex = (currentWaypointIndex + 1) % _waypoints.Length;
                _animator.SetBool("isWalking", true);
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, MonsterData.speed * Time.deltaTime);
                _transform.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING; 
        return state;
    }
}
