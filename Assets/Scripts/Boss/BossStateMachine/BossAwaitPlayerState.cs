using UnityEngine;

public class BossAwaitPlayerState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        Debug.Log("Add Await Player Animation of the boss");
    }
    public override void UpdateState(BossStateManager boss)
    {
        //Add Code to check if player enter the room
        if (boss._door.PlayerPassDoor)
        {
            boss.SwitchState(boss._idleState);
        }
    }
    public override void OnCollisionEnter(BossStateManager boss,Collision collision)
    {

    }
}
