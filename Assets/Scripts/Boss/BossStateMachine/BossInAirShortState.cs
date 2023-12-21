using UnityEngine;

public class BossInAirShortState : BossBaseState
{
    float MaxX;
    float MaxZ;

    int dirX;
    int dirZ;
    int signX = 1;
    int signZ = 1;

    bool _hasToMove = false;
    bool _halfX = false;
    bool _halfZ = false;
    bool _otherHalfX = false;
    bool _otherHalfZ = false;
    bool _end;
    bool _hasTravelHalf;

    Vector3 initialPos;
    Vector3 finalPos;

    public override void EnterState(BossStateManager boss)
    {
        _halfX = false;
        _halfZ = false;
        _otherHalfX = false;
        _otherHalfZ = false;
        _end = false;
        _hasTravelHalf = false;
        signX = 1;
        signZ = 1;
        initialPos = boss._bossRigidbody.position;
        MaxX = boss._door.playerGameObject.transform.position.x -  initialPos.x ;
        MaxZ = boss._door.playerGameObject.transform.position.z - initialPos.z;

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
    }
    public override void UpdateState(BossStateManager boss)
    {
        if (_hasToMove)
        {
            if (!_hasTravelHalf)
            {
                if (signX == 1 && boss.transform.position.x >= initialPos.x + (MaxX / 2))
                {
                    _halfX = true;
                }
                else if (signX == -1 && boss.transform.position.x <= initialPos.x + (MaxX / 2))
                {
                    _halfX = true;
                }

                if (signZ == 1 && boss.transform.position.z >= initialPos.z + (MaxZ / 2))
                {
                    _halfZ = true;
                }
                else if (signZ == -1 && boss.transform.position.z <= initialPos.z + (MaxZ / 2))
                {
                    _halfZ = true;
                }
                if (_halfX && !_halfZ)
                {
                    boss._bossRigidbody.transform.Translate(new Vector3(0.0f * signX, 2f, 1f * signZ) * boss._speed * Time.deltaTime);
                }
                else if (!_halfX && _halfZ)
                {
                    boss._bossRigidbody.transform.Translate(new Vector3(1f * signX, 2f, 0.0f * signZ) * boss._speed * Time.deltaTime);
                }
                else if (_halfX && _halfZ)
                {
                    _hasTravelHalf = true;
                }
                else
                {
                    boss._bossRigidbody.transform.Translate(new Vector3(1f * signX, 2.0f, 1f * signZ) * boss._speed * Time.deltaTime);
                }

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
                if (_otherHalfX && !_otherHalfZ)
                {
                    boss._bossRigidbody.transform.Translate(new Vector3(0.0f * signX, 0.0f, 1f * signZ) * boss._speed * Time.deltaTime);
                }
                else if (!_otherHalfX && _otherHalfZ)
                {
                    boss._bossRigidbody.transform.Translate(new Vector3(1f * signX, 0.0f, 0.0f * signZ) * boss._speed * Time.deltaTime);
                }
                else if (_otherHalfX && _otherHalfZ)
                {
                    _end = true;
                }
                else
                {
                    boss._bossRigidbody.transform.Translate(new Vector3(1f * signX, 0.0f, 1f * signZ) * boss._speed * Time.deltaTime);
                }
            }
            else if (_end)
            {
                boss.SwitchState(boss._shortJumpLandState);
            }
        }

    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
}