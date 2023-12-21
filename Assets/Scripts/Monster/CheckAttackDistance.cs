using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class CheckAttackDistance : Node
{
    private Transform _transform;
    private Animator _animator;
    private NavMeshAgent _agent;

    LayerMask myLayerMask = LayerMask.GetMask("player");

    public CheckAttackDistance(Transform transform)
    {
        this._transform = transform;
        this._animator = transform.GetComponent<Animator>();
        this._agent = transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, MonsterDataNinja.rangeAttackDistance, myLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;

        Vector3 direction = target.position - _transform.position;

        RaycastHit hit;

        direction.Normalize();

        Vector3 rayStart = _transform.position + Vector3.up * 1f;

        if (Physics.Raycast(rayStart, direction, out hit, MonsterDataNinja.rangeAttackDistance))
        {
            _agent.SetDestination(target.position);
            _transform.LookAt(target.position);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                _animator.SetBool("isWalking", false);
                _animator.SetBool("isPunch", true);

                _agent.ResetPath();
                _agent.velocity = Vector3.zero;

                state = NodeState.SUCCESS;
                return state;
            }
        }

        state = NodeState.FAILURE;
        return state;
    }
}
