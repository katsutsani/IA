using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventFeedBack : MonoBehaviour
{

    [SerializeField] UnityEvent _feedback;

    public void TakeDamage(int damage)
    {
        _feedback.Invoke();
    }
}
