using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem _hitEnnemyParticle;
    [SerializeField] ParticleSystem _hitWalls;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monsters") || other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            var Particle = Instantiate(_hitEnnemyParticle, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Destroy(other.gameObject);
            Particle.Play();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            var Particle = Instantiate(_hitWalls, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Particle.Play();
        }

    }
}
