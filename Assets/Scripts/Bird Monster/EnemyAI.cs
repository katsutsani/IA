using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{
    Coroutine _invuTime;
    protected int health = 3;

    public bool TakeHit()
    {
        if (health > 0)
        {
            health--;
            gameObject.layer = LayerMask.NameToLayer("Invu");
            _invuTime = StartCoroutine(InvuTimer());
            return false;
        }
        else
        {
            Debug.Log("je suis la");
            gameObject.layer = LayerMask.NameToLayer("Default");
            gameObject.transform.position = new Vector3(0.8135204f, 0.02000046f, -1.324965f);
            return true;
        }
    }

    public void DestroyEntity()
    {
        gameObject.layer = LayerMask.NameToLayer("player");
    }

    IEnumerator InvuTimer()
    {
        yield return new WaitForSeconds(2);
        gameObject.layer = gameObject.layer = LayerMask.NameToLayer("player");
        StopCoroutine(_invuTime);
    }
}
