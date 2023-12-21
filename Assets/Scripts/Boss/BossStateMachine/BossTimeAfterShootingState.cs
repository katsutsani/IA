using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossTimeAfterShootingState : BossBaseState
{
    Coroutine _startLongJump;
    public override void EnterState(BossStateManager boss)
    {
       _startLongJump = boss.StartCoroutine(PrepareJump(boss));
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
    IEnumerator PrepareJump(BossStateManager boss)
    {
        yield return new WaitForSeconds(7);
        boss.StopCoroutine(_startLongJump);
        boss.SwitchState(boss._idleState);
    }


}

