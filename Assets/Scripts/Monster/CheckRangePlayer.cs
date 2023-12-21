using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckRangePlayer : NodeEmile
{
    LayerMask myLayerMask = LayerMask.GetMask("player");

    private Transform _transform;
    private Animator _animator;

    public CheckRangePlayer(Transform transform)
    {
        this._transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null )
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, MonsterDataBird.rangeSee, myLayerMask);

            if(colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                _animator.SetBool("isWalking", true);

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
