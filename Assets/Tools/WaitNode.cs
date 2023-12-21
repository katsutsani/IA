using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : Action_node
{
    public float duration = 1;
    float StartTime;

    protected override void OnStart()
    {
        StartTime = Time.time;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (Time.time - StartTime < duration)
        {
            return State.Success;

        }
        return State.Running;
    }
}
