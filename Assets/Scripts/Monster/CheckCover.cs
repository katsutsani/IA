using BehaviorTree;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckCover : NodeEmile
{
    private MonsterInfo _monsterInfo;
    private Transform _transform;
    private Animator _animator;

    LayerMask myLayerMask = LayerMask.GetMask("SafeZone");

    public CheckCover(Transform transform)
    {
        this._transform = transform;
        this._monsterInfo = transform.GetComponent<MonsterInfo>();
        this._animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (_monsterInfo.TakeHit() == 0)
        {

            state = NodeState.FAILURE;
            return state;
        }

        object t = GetData("SafeZone");

        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, 100, myLayerMask);

            List<Transform> safeZones = new List<Transform>();

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    safeZones.Add(collider.transform);
                }

                Transform farthestSafeZone = FindFarthestSafeZone(colliders);

                parent.parent.SetData("SafeZone", farthestSafeZone);
            }
            else
            {
                state = NodeState.FAILURE;
                return state;
            }
        }

        if (_monsterInfo.TakeHit() == 1)
        {

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }

    private Transform FindFarthestSafeZone(Collider[] colliders)
    {
        Transform farthestSafeZone = null;
        float maxDistance = 0f;

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(_transform.position, collider.transform.position);

            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestSafeZone = collider.transform;
            }
        }

        return farthestSafeZone;
    }
}
