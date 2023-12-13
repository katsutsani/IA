using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool canSeeThePlayer;

    public Animator knightAnimator;

    public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            knightAnimator.speed = 0.5f;
            knightAnimator.SetBool("canSeeThePlayer", true);
            return chaseState;
        }
        else return this;
    }
}
