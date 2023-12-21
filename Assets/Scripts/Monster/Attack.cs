using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : NodeEmile
{
    private Animator _animator;
    private Transform _lastTarget;
    private EnemyAI _enemyAI;
    private NavMeshAgent _agent;

    private float attackTime = 1.0f;
    private float attackCounter = 0.0f;

    private string _name;
    private string[] _nameSplit;
    private float rangeAttack;

    public Attack(Transform transform)
    {
        this._animator = transform.GetComponent<Animator>();
        this._agent = transform.GetComponent<NavMeshAgent>();
    }

    void SplitName()
    {
        _name = _animator.transform.name;
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

    public override NodeState Evaluate()
    {
        GetMonster();

        Transform target = (Transform)GetData("target");

        if (_lastTarget != null && Vector3.Distance(target.position, _animator.transform.position) > rangeAttack)
        {
            _animator.SetBool("isPunch", false);
            _animator.SetBool("isWalking", true);

            state = NodeState.FAILURE;
            return state;
        }

        if (_lastTarget != target)
        {
            _lastTarget = target;
            _enemyAI = _lastTarget.GetComponent<EnemyAI>();
        }


        attackCounter += Time.deltaTime;

        if(attackCounter >= attackTime)
        {

            bool isDead = _enemyAI.TakeHit();

            if (isDead)
            {
                ClearData("target");

                _enemyAI.DestroyEntity();
                _animator.SetBool("isPunch", false);
                _animator.SetBool("isWalking", true);
            }
            else
            {
                attackCounter = 0.0f;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
