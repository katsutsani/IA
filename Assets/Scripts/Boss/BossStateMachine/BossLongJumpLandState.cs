using System.Collections;
using UnityEngine;

public class BossLongJumpLandState : BossBaseState
{
    Coroutine _waitStatic;
    public override void EnterState(BossStateManager boss)
    {
        boss._bossRigidbody = boss.GetComponent<Rigidbody>();
        boss._bossRigidbody.GetComponentInChildren<MeshRenderer>().enabled = true;
        boss._bossRigidbody.position = new Vector3(boss._door.playerGameObject.transform.position.x, 10.0f, boss._door.playerGameObject.transform.position.z);
        boss._bossRigidbody.GetComponent<Rigidbody>().useGravity = true;

        boss._bossRigidbody.AddForce(Vector3.down * 50, ForceMode.Impulse);
        _waitStatic = boss.StartCoroutine(WaitStatic(boss));
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            boss._door.playerGameObject.GetComponent<EnemyAI>().TakeHit();
        }
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            float force = 1.5f;
            boss._shakeScreen.StartShakeScreen(force);
        }
    }

    IEnumerator WaitStatic(BossStateManager boss)
    {
        yield return new WaitForSeconds(9);
        boss.SwitchState(boss._idleState);
        boss.StopCoroutine(_waitStatic);
    }
}
