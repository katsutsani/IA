using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakeScreen : MonoBehaviour
{
    private CinemachineImpulseSource _source;

    public void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }
    public void StartShakeScreen(float force)
    {
        _source.GenerateImpulseWithForce(force);
    }
}
