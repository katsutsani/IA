using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossPrepareShootingState : BossBaseState
{
    Coroutine _prepareShooting;
    Vector3 _bossVelocity;

    public override void EnterState(BossStateManager boss)
    {
        boss._nbShoot = Random.Range(2, 5);
        _prepareShooting = boss.StartCoroutine(PrepareJump(boss));
        _bossVelocity = boss._door.playerGameObject.GetComponent<Rigidbody>().velocity;

    }
    public override void UpdateState(BossStateManager boss)
    {
        boss._bossRigidbody.transform.LookAt(new Vector3(boss._door.playerGameObject.transform.position.x, boss.gameObject.transform.position.y, boss._door.playerGameObject.transform.position.z));
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

