using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossInAirLongState : BossBaseState
{
    Coroutine _longJump;

    Vector3 initialPos;
    Vector3 finalPos;

    bool _inAir = true;
    bool _jumpMovement = false;

    float _jumpForce;
    float elapsedTime;

    public override void EnterState(BossStateManager boss)
    {
        initialPos = boss._bossRigidbody.position;

        finalPos = new Vector3(initialPos.x + boss._door.playerGameObject.transform.position.x, initialPos.y, initialPos.z + boss._door.playerGameObject.transform.position.z);
        _jumpForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * Vector3.Magnitude(finalPos - initialPos));
        _inAir = true;
        _jumpMovement = false;
        elapsedTime = 0;
        _longJump = boss.StartCoroutine(longJump(boss));
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }

    IEnumerator longJump(BossStateManager boss)
    {
        boss._bossRigidbody.AddForce(Vector3.up * 15, ForceMode.Impulse);
        yield return new WaitForSeconds(1);
        boss._bossRigidbody.GetComponentInChildren<MeshRenderer>().enabled = false;
        boss._bossRigidbody.GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(4);
        boss.SwitchState(boss._longJumpLandState);
    }
}