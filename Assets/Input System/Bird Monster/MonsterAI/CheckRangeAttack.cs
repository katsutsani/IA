using BehaviorTree;
using UnityEngine;


public class CheckRangeAttack : Node
{
    private Transform _transform;
    private Animator _animator;


    public CheckRangeAttack(Transform transform)
    {
        this._transform = transform;
        this._animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;

        if (Vector3.Distance(_transform.position, target.position) < MonsterData.rangeAttack)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isPunch", true);

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
