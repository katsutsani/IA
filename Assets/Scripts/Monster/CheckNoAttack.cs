using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class CheckNoAttack : NodeEmile
{
    private Transform _transform;
    private Animator _animator;
    private NavMeshAgent _agent;

    LayerMask myLayerMask = LayerMask.GetMask("player");

    public CheckNoAttack(Transform transform)
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

                state = NodeState.FAILURE;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;
        }

        Transform target = (Transform)t;

        Vector3 direction = target.position - _transform.position;

        RaycastHit hit;

        direction.Normalize();

        Vector3 rayStart = _transform.position + Vector3.up * 1f;

        if (Vector3.Distance(_transform.position, target.position) < MonsterDataNinja.rangeAttack)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        if (Physics.Raycast(rayStart, direction, out hit, MonsterDataNinja.rangeAttackDistance))
        {
            _transform.LookAt(target.position);

            Debug.DrawRay(rayStart, direction * hit.distance, Color.red);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                _animator.SetBool("isWalking", false);
                _animator.SetBool("isPunch", true);

                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
