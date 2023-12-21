using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class GoToPlayer : NodeEmile
{
    private Transform _transform;
    private NavMeshAgent _agent;

    public GoToPlayer(Transform transform)
    {
        this._transform = transform;
        this._agent = transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(_transform.position, target.position) > 0.01f && Vector3.Distance(_transform.position, target.position) < MonsterDataBird.rangeSee)
        {
            _agent.SetDestination(target.position);
            _transform.LookAt(target.position);
        }
        else
        {
            _agent.ResetPath();
            state = NodeState.FAILURE; 
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
