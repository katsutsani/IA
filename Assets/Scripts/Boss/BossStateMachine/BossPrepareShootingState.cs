using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossPrepareShootingState : BossBaseState
{
    Coroutine _prepareShooting;
    public override void EnterState(BossStateManager boss)
    {
        boss._nbShoot = Random.Range(2, 5);
        _prepareShooting = boss.StartCoroutine(PrepareJump(boss));
    }
    public override void UpdateState(BossStateManager boss)
    {
        boss._bossRigidbody.transform.LookAt(boss._door.playerGameObject.transform.position);
    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
    IEnumerator PrepareJump(BossStateManager boss)
    {
        yield return new WaitForSeconds(3);
        boss.StopCoroutine(_prepareShooting);
        boss.SwitchState(boss._shootingState);
    }


}

