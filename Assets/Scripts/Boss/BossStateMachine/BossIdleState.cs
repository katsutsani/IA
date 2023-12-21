using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BossIdleState : BossBaseState
{

    Coroutine _stopMovementsCoroutine;
    Coroutine _movementBeforeNextAttack;

    Vector3 initialPos;
    Vector3 finalPos;

    float MaxX;
    float MaxZ;
    int dirX;
    int dirZ;
    int signX = 1;
    int signZ = 1;

    bool _hasToMove = false;
    bool _hasTravelHalf;
    bool _halfX = false;
    bool _halfZ = false;
    bool _otherHalfX = false;
    bool _otherHalfZ = false;
    bool _end;


    public override void EnterState(BossStateManager boss)
    {
        float timeBeforeNextAttack = Random.Range(10, 15);
        _stopMovementsCoroutine = boss.StartCoroutine(StopMouvements(timeBeforeNextAttack, boss));
        _movementBeforeNextAttack = boss.StartCoroutine(movements(boss.GetComponent<Rigidbody>(), boss));
    }
    public override void UpdateState(BossStateManager boss)
    {

        if (_hasToMove)
        {
            if (!_hasTravelHalf)
            {
                if (signX == 1 && boss.transform.position.x >= initialPos.x + (MaxX / 3))
                {
                    _halfX = true;
                }
                else if (signX == -1 && boss.transform.position.x <= initialPos.x + (MaxX / 3))
                {
                    _halfX = true;
                }

                if (signZ == 1 && boss.transform.position.z >= initialPos.z + (MaxZ / 3))
                {
                    _halfZ = true;
                }
                else if (signZ == -1 && boss.transform.position.z <= initialPos.z + (MaxZ / 3))
                {
                    _halfZ = true;
                }
                if (_halfX && _halfZ)
                {
                    _hasTravelHalf = true;
                }
                boss._bossRigidbody.transform.Translate(new Vector3(1f * signX, 1f, 1f * signZ) * boss._speed * Time.deltaTime);

            }
            else if (_hasTravelHalf && !_end)
            {
                if (signX == 1 && boss.transform.position.x >= finalPos.x)
                {
                    _otherHalfX = true;
                }
                else if (signX == -1 && boss.transform.position.x <= finalPos.x)
                {
                    _otherHalfX = true;
                }

                if (signZ == 1 && boss.transform.position.z >= finalPos.z)
                {
                    _otherHalfZ = true;
                }
                else if (signZ == -1 && boss.transform.position.z <= finalPos.z)
                {
                    _otherHalfZ = true;
                }
                if (_otherHalfX && _otherHalfZ)
                {
                    _end = true;
                }
                boss._bossRigidbody.transform.Translate(new Vector3(1f * signX, 0.0f, 1f * signZ) * boss._speed * Time.deltaTime);
            }
        }
        else
        {
            if (!boss._isInSecondPhase)
            {
                int Jump = Random.Range(1, 2);
                if (Jump == 1)
                {
                    boss.SwitchState(boss._startShortJumpState);
                }
                else
                {
                    boss.SwitchState(boss._startLongJumpState);
                }
            }
            else
            {
                boss.SwitchState(boss._prepareShootingState);
            }

        }
    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }

    IEnumerator movements(Rigidbody _bossRigidbody, BossStateManager boss)
    {
        while (true)
        {
            boss._bossRigidbody.transform.rotation = Quaternion.identity;
            _halfX = false;
            _halfZ = false;
            _otherHalfX = false;
            _otherHalfZ = false;
            _end = false;
            _hasTravelHalf = false;
            initialPos = _bossRigidbody.position;
            MaxX = Random.Range(-5, 5);
            MaxZ = Random.Range(-5, 5);
            if (MaxX < 0)
            {
                signX = -1;
            }
            if (MaxZ < 0)
            {
                signZ = -1;
            }
            finalPos = new Vector3(initialPos.x + MaxX, initialPos.y, initialPos.z + MaxZ);
            _hasToMove = true;
            float WaitBeforeNextMovement = Random.Range(5, 7);
            yield return new WaitForSeconds(WaitBeforeNextMovement);
        }
    }

    IEnumerator StopMouvements(float number, BossStateManager boss)
    {
        yield return new WaitForSeconds(number);
        boss.StopCoroutine(_movementBeforeNextAttack);
        boss.StopCoroutine(_stopMovementsCoroutine);
        _hasToMove = false;
    }
}
