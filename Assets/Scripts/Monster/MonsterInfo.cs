using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    [SerializeField] public int health = 90;
    private bool healTime = true;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private EnemyTakeDamage _enemyTakeDamage;

    public void Awake()
    {
        _enemyTakeDamage.health = health;
    }

    public int TakeHit()
    {
        if (health > 0)
        {
            if(health < 45 && healTime == true)
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

    public bool ShootBullet()
    {
        if (health > 0)
        {
            var Bullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * 10;
            return false;
        }
        return true;
    }

    public bool Heal()
    {
        if(health <= 45)
        {
            healTime = false;
            health += 25;
            _enemyTakeDamage.health = health;
            return true;
        }
        else
        {
            return false;
        }
    }
}
