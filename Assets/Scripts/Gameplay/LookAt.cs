using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;

    private void Awake()
    {
        gameObject.SetActive(true);
    }
    public void Rotate(Transform entityTransform)
    {

        //var dir = target.position - entityTransform.position;
        entityTransform.LookAt(new Vector3(target.position.x, entityTransform.position.y, target.position.z));
        //var deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg +90;
        //entityTransform.rotation = Quaternion.Euler(0, deg, 0);
    }
}
