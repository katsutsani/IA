using System.Collections;
using UnityEngine;

public class BossStartShortJumpState : BossBaseState
{
    Coroutine _startShortJump;
    public override void EnterState(BossStateManager boss)
    {
        _startShortJump = boss.StartCoroutine(PrepareJump(boss));
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }

    IEnumerator PrepareJump(BossStateManager boss)
    {
        yield return new WaitForSeconds(3);
        boss.SwitchState(boss._inAirShortState);
        boss.StopCoroutine(_startShortJump);

    }
}
