using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Node
{
    private Animator _animator;
    private Transform _transform;
    MonsterInfo _monsterInfo;

    private float _healthTime = 3.0f;
    private float _healthCounter = 0.0f;

    public Health(Transform transform)
    {
        this._transform = transform;
        this._animator = _transform.GetComponent<Animator>();
        this._monsterInfo = _transform.GetComponent<MonsterInfo>();
    }

    public override NodeState Evaluate()
    {
        _healthCounter += Time.deltaTime;

        if (_healthCounter >= _healthTime)
        {
            bool healFull = _monsterInfo.Heal();
            if (healFull)
            {
                ClearData("SafeZone");
                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                _healthCounter = 0.0f;
            }
        }
            state = NodeState.RUNNING;
        return state;
    }
}
