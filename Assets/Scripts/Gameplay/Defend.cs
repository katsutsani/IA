using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : MonoBehaviour
{
    Coroutine _checkCollision;

    private void Awake()
    {
        gameObject.SetActive(true);
    }
    public void HandleDefense(Collider Shield, Animator animator)
    {
        animator.SetBool("isBlocking", true);
        Shield.enabled = true;
        _checkCollision = StartCoroutine(CheckCollision(Shield));

    }

    public void StopDefending(Collider Shield,Animator animator)
    {
        Shield.enabled = false;
        animator.SetBool("isBlocking", false);
        StopCoroutine(_checkCollision);
    }

    IEnumerator CheckCollision(Collider Shield)
    {
        if (Shield.isTrigger)
        {
            Debug.Log("test");
        }

        yield return null;
    }
}
