using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInputSystem : MonoBehaviour
{
    public float speed = 1.0f;
    private Animator animator;
    private Vector2 moveMonster = Vector2.up;
    private Transform transformMonster;
    [SerializeField] Transform[] wayPoint;
    [SerializeField] BoxCollider boxBird;
    private GameObject target;

    private bool isWaiting = true;
    private float waitTime = 1.0f;
    private float waitCounter = 0.0f;

    private int currentWayPointIndex = 0;
    private bool targetFind = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        transformMonster = GetComponent<Transform>();
        target = GameObject.Find("Knight");
    }

    private void Update()
    {
        TriggerPlayer();
        if( targetFind == false)
        {
            CheckWayPoints();
        }
        else
        {
            FollowPlayer();
        }
    }

    void CheckWayPoints()
    {
        if (isWaiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                isWaiting = false;
            }
        }
        else
        {
            Transform wp = wayPoint[currentWayPointIndex];
            if (Vector3.Distance(transformMonster.position, wp.position) < 1f)
            {
                animator.SetBool("isWalking", false);
                waitCounter = 0.0f;
                isWaiting = true;
                currentWayPointIndex = (currentWayPointIndex + 1) % wayPoint.Length;
            }
            else
            {
                animator.SetBool("isWalking", true);
                transformMonster.position = Vector3.MoveTowards(transformMonster.position, wp.position, speed * Time.deltaTime);
            }
        }
    }


    private void TriggerPlayer()
    {
        if(Vector3.Distance(transformMonster.position, target.transform.position) < 5f)
        {
            targetFind = true;
            animator.SetBool("isRunning", true);
        }
        else
        {
            targetFind = false;
            animator.SetBool("isRunning", false);
        }
    }

    void FollowPlayer()
    {
        transformMonster.position = Vector3.MoveTowards(transformMonster.position, target.transform.position, speed * Time.deltaTime);
    }
}
