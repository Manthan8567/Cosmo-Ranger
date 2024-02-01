using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float chaseRadius = 10;
    private float chaseSpeed = 3;

    private float patrolSpeed = 2;

    private float attackRadius = 1.5f;

    private float timeBetweenAttacks = 1.5f;
    private float timeSinceLastAttack;

    private EnemyState currentState = EnemyState.IDLE;
    private Vector3 initialPosition;

    private Animator animator;


    void Awake()
    {
        initialPosition = this.transform.position;
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

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
        // Transition (IDLE -> CHASE)
        float distance = Vector3.Distance(this.transform.position, target.position);

        this.animator.SetBool("isWalking", false);

        if (distance < chaseRadius)
        {
            currentState = EnemyState.CHASE;
        }
    }

    public void Patrol()
    {
        // Action
        this.transform.position = Vector3.MoveTowards(this.transform.position, initialPosition, patrolSpeed * Time.deltaTime);
        this.transform.LookAt(initialPosition);

        // Transition (PATROL -> IDLE)
        float distance = Vector3.Distance(this.transform.position, initialPosition);

        if (distance < 0.1f)
        {
            this.transform.position = initialPosition;
            currentState = EnemyState.IDLE;
        }
    }

    public void Chase()
    {
        // Action
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, chaseSpeed * Time.deltaTime);
        this.transform.LookAt(target);

        this.animator.SetBool("isWalking", true);

        // Transition (CHASE -> ATTACK / PATROL)
        float distance = Vector3.Distance(this.transform.position, target.position);

        if (distance < attackRadius)
        {
            currentState = EnemyState.ATTACK;
            this.animator.SetBool("isWalking", false);
        }

        if (distance > chaseRadius)
        {
            currentState = EnemyState.PATROL;
        }
    }

    public void Attack()
    {
        // Action
        // This is only for the test. We should implement the real attack system later. -24/01/29
        this.transform.LookAt(target.position);

        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            UnityEngine.Debug.Log("Attacked");
            animator.SetTrigger("Punch");

            timeSinceLastAttack = 0;
        }

        // Transition (ATTACK -> CHASE)
        float distance = Vector3.Distance(this.transform.position, target.position);

        if (distance > attackRadius)
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
