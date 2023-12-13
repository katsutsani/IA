using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInputSystem : MonoBehaviour
{
    private Rigidbody monsterBody;
    public float speed = 1.0f;
    private Animator animator;
    private Vector2 moveMonster = Vector2.up;
    Transform _transform;
    [SerializeField] Transform[] _wayPoint;

    private bool _isWaiting = true;
    private float _waitTime = 1.0f;
    private float _waitCounter = 0.0f;

    private int _currentWayPointIndex = 0;

    private void Awake()
    {
        monsterBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        MonsterMove();
    }

    void MonsterMove()
    {
        CheckWayPoints();
    }

    void CheckWayPoints()
    {
        if (_isWaiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _isWaiting = false;
            }
        }
        else
        {
            Transform wp = _wayPoint[_currentWayPointIndex];
            if (Vector3.Distance(_transform.position, wp.position) < 1f)
            {
                animator.SetBool("isWalking", false);
                _waitCounter = 0.0f;
                _isWaiting = true;
                _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoint.Length;
            }
            else
            {
                animator.SetBool("isWalking", true);
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, speed * Time.deltaTime);
            }
        }
    }

}
