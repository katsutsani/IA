using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackDistance : NodeEmile
{
    private Animator _animator;
    private Transform _lastTarget;
    private EnemyAI _enemyAI;
    private NavMeshAgent _agent;
    private MonsterInfo _monsterInfo;

    private float attackTime = 1.0f;
    private float attackCounter = 0.0f;

    private string _name;
    private string[] _nameSplit;
    private float rangeAttack;

    public AttackDistance(Transform transform)
    {
        this._animator = transform.GetComponent<Animator>();
        this._agent = transform.GetComponent<NavMeshAgent>();
        this._monsterInfo = transform.GetComponent<MonsterInfo>();
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
                rangeAttack = MonsterDataCactoro.rangeAttackDistance;
                break;
            case "Ninja":
                rangeAttack = MonsterDataNinja.rangeAttackDistance;
                break;
            default:
                break;
        }
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (_lastTarget != null && Vector3.Distance(target.position, _animator.transform.position) > MonsterDataNinja.rangeAttackDistance)
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

            bool isDead = _monsterInfo.ShootBullet();

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
