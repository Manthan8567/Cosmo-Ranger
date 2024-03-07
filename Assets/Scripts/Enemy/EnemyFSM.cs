using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private PatrolPath patrolPath;

    private NavMeshAgent agent;

    private float distanceCloseEnough = 2f;

    private float patrolSpeed = 0.7f;

    private float chaseRadius = 5;
    private float chaseSpeed = 1;

    private float attackRadius = 1.5f;
    private float attackCoolTime = 2;

    private float distanceBetweenTarget;

    private EnemyState currentState = EnemyState.PATROL;
    private Vector3 initialPosition;

    private int currWayPointIndex = 0;

    private Animator animator;


    void Awake()
    {
        initialPosition = this.transform.position;

        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();

        agent.SetDestination(GetCurrentWayPoint());
    }

    void Update()
    {
        distanceBetweenTarget = Vector3.Distance(this.transform.position, target.position);

        switch (currentState)
        {
            case EnemyState.IDLE:
                Idle();
                break;

            case EnemyState.PATROL:
                Patrol();
                break;

            case EnemyState.CHASE:
                Chase();
                break;

            case EnemyState.ATTACK:
                Attack();
                break;
        }
    }

    public void Idle()
    {
        // Action
        this.animator.SetBool("isWalking", false);

        // Transition (IDLE -> CHASE)
        if (distanceBetweenTarget < chaseRadius)
        {
            currentState = EnemyState.CHASE;
        }
    }

    public void Patrol()
    {
        // Action
        WalkingAround();

        // Transition (PATROL -> CHASE)
        if (distanceBetweenTarget < chaseRadius)
        {
            currentState = EnemyState.CHASE;
        }
    }

    private void WalkingAround()
    {
        this.animator.SetBool("isWalking", true);

        agent.speed = patrolSpeed;
        agent.SetDestination(GetCurrentWayPoint());

        if (patrolPath != null)
        {
            if (IsAtWayPoint())
            {
                currWayPointIndex = patrolPath.GetNextIndex(currWayPointIndex);
            }
        }
    }

    private Vector3 GetCurrentWayPoint()
    {
        return patrolPath.GetWayPoint(currWayPointIndex);
    }

    private bool IsAtWayPoint()
    {
        float distanceBetweenWayPoint = Vector3.Distance(this.transform.position, GetCurrentWayPoint());

        //UnityEngine.Debug.Log(distanceBetweenWayPoint);

        return distanceBetweenWayPoint < distanceCloseEnough;
    }

    public void Chase()
    {
        // Action
        agent.speed = chaseSpeed;
        agent.SetDestination(target.position);

        this.animator.SetBool("isWalking", true);      

        // Transition (CHASE -> ATTACK / PATROL)
        if (distanceBetweenTarget < attackRadius)
        {
            currentState = EnemyState.ATTACK;
            this.animator.SetBool("isWalking", false);
        }

        if (distanceBetweenTarget > chaseRadius)
        {
            currentState = EnemyState.PATROL;
        }
    }

    public void Attack()
    {
        // Action
        GetComponent<EnemyCombat>().CheckAttackCondition(AttackType.PUNCH, attackRadius, attackCoolTime);

        // Transition (ATTACK -> CHASE)
        if (distanceBetweenTarget > attackRadius)
        {
            currentState = EnemyState.CHASE;
        }
    }

    public enum EnemyState
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK
    }
}
