using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;


public class PlayerCombat : MonoBehaviour, IFightable
{
    [SerializeField] Projectile fireball;
    [SerializeField] Transform rightHandTransform;

    newHealth target;
    Animator _animator;

    float punchRadius = 3;
    float punchCoolTime = 1.5f;
    int punchDamage = 20;

    float fireballRadius = 7;
    float fireballCoolTime = 2;

    float timeSinceAttack = 2;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;

        // Left mouse -> Punch
        if (InputManager.punchInput == 1)
        {
            CheckAttackCondition(AttackType.PUNCH, punchRadius, punchCoolTime);
        }

        // Press 1 -> Fireball 
        if (InputManager.spell_fireballInput == 1)
        {
            CheckAttackCondition(AttackType.FIREBALL, fireballRadius, fireballCoolTime);
        }
    }

    public void CheckAttackCondition(AttackType type, float attackRadius, float attackCoolTime)
    {
        // Shoot a ray based on the mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, attackRadius, LayerMask.GetMask("Enemy")))
        {
            target = hit.transform.GetComponent<newHealth>();

            if (timeSinceAttack > attackCoolTime)
            {
                switch (type)
                {
                    case AttackType.PUNCH: { Punch(); break; }
                    case AttackType.FIREBALL: { ThrowFireball(); break; }
                }
            }
        }
    }

    public void Punch()
    {
        this.transform.LookAt(target.transform);

        // This will call Hit() event
        _animator.SetTrigger("Punch");

        timeSinceAttack = 0;
    }

    // Animation(Punch) event
    private void Hit()
    {
        AudioManager.Singleton.PlaySoundEffect("PlayerPunch");

        target.TakeDamage(punchDamage);
    }

    public void ThrowFireball()
    {
        // This will call Throw() & StartThrowing() event
        _animator.SetTrigger("Fireball");

        timeSinceAttack = 0;
    }

    // Animation(Fireball) event
    private void StartThrowing()
    {
        this.transform.LookAt(target.transform);
    }

    // Animation(Fireball) event
    private void Throw()
    {
        this.transform.LookAt(target.transform);

        Projectile tempProjectile = Instantiate(fireball, rightHandTransform.position, Quaternion.identity);
        tempProjectile.SetTarget(target);

        AudioManager.Singleton.PlaySoundEffect("Fireball");
    }
}