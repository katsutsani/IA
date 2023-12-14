using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckRangePlayer : Node
{
    LayerMask myLayerMask = LayerMask.GetMask("player");

    private Transform transform;
    private Animator animator;

    public CheckRangePlayer(Transform transform)
    {
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null )
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, MonsterData.rangeSee, myLayerMask);
            Debug.Log(colliders.Length);
            if(colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                animator.SetBool("isWalking", true);

                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
