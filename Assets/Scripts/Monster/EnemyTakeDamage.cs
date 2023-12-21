using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    public int health = 90;

    [SerializeField] ParticleSystem _particle;
    [SerializeField] MonsterInfo _monsterInfo;

    public bool EnemyTakeHit(int Damage)
    {
        if (health > 0)
        {
            health -= Damage;
            if (_monsterInfo)
            {
                _monsterInfo.health = health;
                _monsterInfo.TakeHit();
            }
            return false;
        }
        else
        {
            DestroyEntity();
            return true;
        }

    }

    public void DestroyEntity()
    {
        Destroy(gameObject);
    }
}
