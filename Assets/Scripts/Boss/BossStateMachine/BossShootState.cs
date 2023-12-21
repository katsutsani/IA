using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossShootState : BossBaseState
{
    Coroutine _startShooting;
    public override void EnterState(BossStateManager boss)
    {
        _startShooting = boss.StartCoroutine(PrepareJump(boss));
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
    IEnumerator PrepareJump(BossStateManager boss)
    {
        for (int i = 0; i < boss._nbShoot; i++)
        {
            boss._shootBullet.InstantiateBullet(boss._bulletPrefab,boss._bulletSpawnPoint, boss._bulletSpeed);
            yield return new WaitForSeconds(4);

        }
        boss.StopCoroutine(_startShooting);
        boss.SwitchState(boss._timeAfterShootingState);
    }


}

