using System.Collections;
using UnityEngine;

public class BossShortJumpLandState : BossBaseState
{
    Coroutine _waitStatic;
    public override void EnterState(BossStateManager boss)
    {
        float force = .5f;
        boss._shakeScreen.StartShakeScreen(force);
       _waitStatic = boss.StartCoroutine(WaitStatic(boss));
        boss._bossRigidbody.transform.LookAt(boss._door.playerGameObject.transform.position);

    }
    public override void UpdateState(BossStateManager boss)
    {

    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            boss._door.playerGameObject.GetComponent<EnemyAI>().TakeHit();
        }
    }
    IEnumerator WaitStatic(BossStateManager boss)
    {
        yield return new WaitForSeconds(5);
        boss.SwitchState(boss._idleState);
        boss.StopCoroutine(_waitStatic);
    }
}
