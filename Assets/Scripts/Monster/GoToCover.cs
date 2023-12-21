using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GoToCover : NodeEmile
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private Animator _animator;

    public GoToCover(Transform transform)
    {
        this._transform = transform;
        this._agent = _transform.GetComponent<NavMeshAgent>(); 
        this._animator = _transform.GetComponent<Animator>();

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("SafeZone");
        
        if (Vector3.Distance(target.position, _agent.transform.position) < 0.7f)
        {
            _agent.ResetPath();
            _animator.SetBool("isWalking", false);

            state = NodeState.SUCCESS;
            return state;
        }
        _agent.speed = 4;
        _agent.SetDestination(target.position);

        _animator.SetBool("isWalking", true);

        state = NodeState.RUNNING;
        return state;


    }
}
