using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent agent;

    private float chaseRadius = 5;

    private float attackRadius = 1.5f;
    private float attackCoolTime = 2;

    private float distanceBetweenTarget;

    private EnemyState currentState = EnemyState.IDLE;
    private Vector3 initialPosition;

    private Animator animator;


    void Awake()
    {
        initialPosition = this.transform.position;

        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
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
        agent.SetDestination(initialPosition);

        // Transition (PATROL -> IDLE)
        float distanceBetweenInitPos = Vector3.Distance(this.transform.position, initialPosition);

        if (distanceBetweenInitPos < 2)
        {
            agent.SetDestination(initialPosition);

            currentState = EnemyState.IDLE;
        }

        // Transition (PATROL -> Chase)
        if (distanceBetweenTarget < chaseRadius)
        {
            currentState = EnemyState.CHASE;
        }
    }

    public void Chase()
    {
        // Action
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
