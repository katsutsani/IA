using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class GoToPlayer : Node
{
    private Transform transform;

    public GoToPlayer(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, MonsterData.speed * Time.deltaTime);
            transform.LookAt(target.position);
        }
        state = NodeState.RUNNING;
        return state;
    }
}
