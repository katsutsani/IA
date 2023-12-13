using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public IdleState idleState;
    public override State RunCurrentState()
    {
        if (!idleState.canSeeThePlayer)
        {
            idleState.knightAnimator.SetBool("canSeeThePlayer", false);
            return idleState;
        }
        else return this;
    }
}
