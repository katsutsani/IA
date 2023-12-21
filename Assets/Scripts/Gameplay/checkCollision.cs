using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem _hitEnnemyParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monsters") || other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Destroy(other.gameObject);
            var Particle = Instantiate(_hitEnnemyParticle, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Particle.Play();
        }

    }
}
