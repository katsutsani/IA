using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    Coroutine _invuTime;
    protected int health = 150;
    protected int _secondPhase = 0;
    protected bool _isInSecondPhase = false;

    [SerializeField] BossStateManager _stateManager;

    private void Awake()
    {
        _secondPhase = (health / 2);
    }
    public bool TakeHit()
    {
        if (health > 0)
        {
            health--;
            if(health <= _secondPhase && _isInSecondPhase)
            {
                _stateManager._isInSecondPhase = true;
            }
            gameObject.layer = LayerMask.NameToLayer("Invu");
            _invuTime = StartCoroutine(InvuTimer());
            return false;
        }
        else 
        { 
            return true; 
        }
    }

    public void DestroyEntity()
    {
        Destroy(gameObject);
    }

    IEnumerator InvuTimer()
    {
        yield return new WaitForSeconds(2);
        gameObject.layer = gameObject.layer = LayerMask.NameToLayer("player");
        StopCoroutine(_invuTime);
    }
}
