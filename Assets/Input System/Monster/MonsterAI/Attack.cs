using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    private Animator _animator;
    private Transform _lastTarget;
    private EnemyAI _enemyAI;

    private float attackTime = 1.0f;
    private float attackCounter = 0.0f;

    public Attack(Transform transform)
    {
        this._animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (_lastTarget != null && Vector3.Distance(target.position, _animator.transform.position) > MonsterDataBird.rangeAttack)
        {
            ClearData("target");

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
