using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakeDamage : MonoBehaviour
{
    protected int health = 150;
    protected int _secondPhase = 0;
    protected bool _isInSecondPhase = false;

    [SerializeField] BossStateManager _stateManager;
    [SerializeField] ParticleSystem _particle;

    private void Awake()
    {
        _secondPhase = (health / 2);
    }
    public bool BossTakeHit(int Damage)
    {
        if (health > 0)
        {
            health -= Damage;
            if (health <= _secondPhase && !_isInSecondPhase)
            {
                _stateManager._isInSecondPhase = true;
                _isInSecondPhase = true;
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
