using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;

    public void Rotate(Transform entityTransform)
    {

        var dir = target.position - entityTransform.position;

        var deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg +90;
        entityTransform.rotation = Quaternion.Euler(0, deg, 0);
    }
}
