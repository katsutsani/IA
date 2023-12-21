using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossShootState : BossBaseState
{
    Coroutine _startShooting;
    Vector3 _bossVelocity;
    public override void EnterState(BossStateManager boss)
    {
        _startShooting = boss.StartCoroutine(PrepareJump(boss));
        _bossVelocity = boss._door.playerGameObject.GetComponent<Rigidbody>().velocity;
    }
    public override void UpdateState(BossStateManager boss)
    {
    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
    IEnumerator PrepareJump(BossStateManager boss)
    {
        Vector2 _velocity = Vector2.zero;
        if (_bossVelocity.x > 0)
        {
            _velocity.x = 2.5f;
        }
        else
        {
            _velocity.x = -2.5f;

        }
        if (_bossVelocity.y < 0)
        {
            _velocity.y = 2.5f;
        }
        else
        {
            _velocity.y = -2.5f;
        }
        boss._bossRigidbody.transform.LookAt(new Vector3(boss._door.playerGameObject.transform.position.x + _velocity.x, boss.gameObject.transform.position.y, boss._door.playerGameObject.transform.position.z + _velocity.y));
        boss._shootBullet.InstantiateBullet(boss._bulletPrefab, boss._bulletSpawnPoint, boss._bulletSpeed);
        if (boss._alreadyShoot < boss._nbShoot)
        {
            boss.SwitchState(boss._prepareShootingState);
        }
        else
        {
            boss.SwitchState(boss._timeAfterShootingState);
        }
        yield return null;
    }


}

