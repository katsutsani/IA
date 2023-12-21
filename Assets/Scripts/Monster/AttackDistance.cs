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

    private float attackTime = 1.0f;
    private float attackCounter = 0.0f;

    public AttackDistance(Transform transform)
    {
        this._animator = transform.GetComponent<Animator>();
        this._agent = transform.GetComponent<NavMeshAgent>();
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
