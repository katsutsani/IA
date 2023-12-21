using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem _hitEnnemyParticle;
    [SerializeField] ParticleSystem _hitWalls;
    [SerializeField] Animator _playerAnimator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monsters") || other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Boss"))
            {
                other.GetComponent<BossTakeDamage>().BossTakeHit(15);
            }
            else
            {
                other.GetComponent<EnemyTakeDamage>().EnemyTakeHit(15);
            }
            var Particle = Instantiate(_hitEnnemyParticle, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Particle.Play();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            var Particle = Instantiate(_hitWalls, other.gameObject.transform.position, other.gameObject.transform.rotation);
            gameObject.GetComponent<Collider>().enabled = false;
            _playerAnimator.SetBool("isAttacking", false);
            Particle.Play();
        }

    }
}
