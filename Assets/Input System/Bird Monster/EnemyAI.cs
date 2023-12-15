using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{

    protected int health = 3;

    public bool TakeHit()
    {
        if (health > 0)
        {
            health--;
            return false;
        }
        else
        {
            Debug.Log("je suis la");
            return true;
        }
    }

    public void DestroyEntity()
    {
        Destroy(gameObject);
    }
}
