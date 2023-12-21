using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    private bool healTime = true;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;

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

    public bool ShootBullet()
    {
        if (health > 0)
        {
            var Bullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * 10;
            return false;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            gameObject.transform.position = new Vector3(0.8135204f, 0.02000046f, -1.324965f);
            return true;
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
