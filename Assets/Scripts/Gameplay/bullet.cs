using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            collision.collider.gameObject.GetComponent<EnemyAI>().TakeHit();

        }
        Destroy(gameObject);

    }
}
