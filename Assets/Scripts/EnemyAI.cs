using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{

    protected int health = 5;
    Rigidbody rb;
    [SerializeField] ParticleSystem _getHit;
    [SerializeField] ParticleSystem _playerDead;
    [SerializeField] ParticleSystem _playerRespawn;
    Animator _animator;
    private bool _canRespawn;
    Coroutine _hitAnimation;
    Coroutine _DeathAnimation;
    Coroutine _invuTime;
    float currentClipLength;


    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    public bool TakeHit()
    {
        if (health > 0)
        {
            health--;
            rb.velocity = (-rb.transform.forward) * 1;
            var Particle = Instantiate(_getHit, gameObject.transform.position, gameObject.transform.rotation);
            Particle.Play();
            _animator.SetBool("getHit", true);
            AnimatorClipInfo[] currentAnimation = _animator.GetCurrentAnimatorClipInfo(0);
            currentClipLength = currentAnimation[0].clip.length;
            _hitAnimation = StartCoroutine(StopHitAnimation());
            gameObject.layer = LayerMask.NameToLayer("Invu");
            _invuTime = StartCoroutine(InvuTimer());
            return false;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            var Particle = Instantiate(_playerDead, gameObject.transform.position, gameObject.transform.rotation);
            Particle.Play();
            _animator.SetBool("isDead", true);
            AnimatorClipInfo[] currentAnimation = _animator.GetCurrentAnimatorClipInfo(0);
            currentClipLength = currentAnimation[0].clip.length;
            _DeathAnimation = StartCoroutine(StopDeathAnimation());
            return true;
        }
    }

    public void DestroyEntity()
    {
        gameObject.layer = LayerMask.NameToLayer("player");
       
    }

    IEnumerator StopHitAnimation()
    {
        yield return new WaitForSeconds(currentClipLength-2);
        _animator.SetBool("getHit", false);
        StopCoroutine(_hitAnimation);

    }

    IEnumerator StopDeathAnimation()
    {
        yield return new WaitForSeconds(currentClipLength + 3);
        gameObject.transform.position = new Vector3(0.8135204f, 0.02000046f, -1.324965f);
        var Particle = Instantiate(_playerRespawn, gameObject.transform.position, Quaternion.identity);
        _animator.SetBool("isDead", false);
        Particle.Play();
        StopCoroutine(_DeathAnimation);

    }
    IEnumerator InvuTimer()
    {
        yield return new WaitForSeconds(4);
        gameObject.layer = gameObject.layer = LayerMask.NameToLayer("player");
        StopCoroutine(_invuTime);
    }
}
