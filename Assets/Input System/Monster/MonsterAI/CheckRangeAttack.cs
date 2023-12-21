using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class CheckRangeAttack : Node
{
    private Transform _transform;
    private Animator _animator;
    private NavMeshAgent _agent;

    LayerMask myLayerMask = LayerMask.GetMask("player");

    private string _name;
    private string[] _nameSplit;
    private float rangeAttack;

    void SplitName()
    {
        _name = _transform.name;
        _nameSplit = _name.Split('-');
    }

    void GetMonster()
    {
        SplitName();

        switch (_nameSplit[0])
        {
            case "Cactoro":
                rangeAttack = MonsterDataCactoro.rangeAttack;
                break;
            case "Bird":
                rangeAttack = MonsterDataBird.rangeAttack;
                break;
            case "Ninja":
                rangeAttack = MonsterDataNinja.rangeAttack;
                break;
            default:
                break;
        }
    }

    public CheckRangeAttack(Transform transform)
    {
        this._transform = transform;
        this._animator = transform.GetComponent<Animator>();
        this._agent = transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        GetMonster();

        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, rangeAttack, myLayerMask);

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

        if (Physics.Raycast(rayStart, direction, out hit, rangeAttack))
        {
            Debug.DrawRay(rayStart, direction * hit.distance, Color.red);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                _agent.SetDestination(_transform.position);
                _agent.ResetPath();
                _animator.SetBool("isWalking", false);
                _animator.SetBool("isPunch", true);
                Debug.Log("la");

                state = NodeState.SUCCESS;
                return state;
            }
        }

        state = NodeState.FAILURE;
        return state;
    }
}
