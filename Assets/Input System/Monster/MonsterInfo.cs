using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    private bool healTime = true;
    private bool attack = false;

    public int TakeHit()
    {
        if (health > 0)
        {
            if(health < 5 && healTime == true)
            {
                return 1;
            }
            return 0;
        }
        else
        {
            return 2;
        }
    }

    public void DestroyEntity()
    {

        gameObject.layer = LayerMask.NameToLayer("player");
    }

    public bool Heal()
    {
        if(health < 6)
        {
            healTime = false;
            health += 4f;

            return true;
        }
        else
        {
            return false;
        }
    }
}
