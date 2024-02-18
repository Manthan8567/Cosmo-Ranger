using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour, IFightable
{
    [SerializeField] private Transform target;
    [SerializeField] Image hpBarBG; // To turn off when it's dead
    [SerializeField] Image hpBar;
    [SerializeField] UnityEvent<int> OnTakeDamage;

    Animator _animator;

    int maxHP = 100;
    int currHP;

    bool isDead = false;

    private int attackDamage = 10;

    private float timeSinceLastAttack;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        currHP = maxHP;
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public void Punch()
    {
        this.transform.LookAt(target.transform);

        _animator.SetTrigger("Punch");

        timeSinceLastAttack = 0;
    }

    // Animation event
    void Hit()
    {
        target.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
    }

    public void CheckAttackCondition(AttackType type, float attackRadius, float attackCoolTime)
    {
        PlayerCombat targetCombatSystem = target.GetComponent<PlayerCombat>();

        if (targetCombatSystem != null && !targetCombatSystem.IsDead())
        {
            if (timeSinceLastAttack > attackCoolTime)
            {
                if (type == AttackType.PUNCH)
                {
                    Punch();
                }
            }
        }
    }

    public void Die()
    {
        _animator.SetTrigger("Die");

        // To prevent enemy following player after death
        GetComponent<EnemyFSM>().enabled = false;
        // To prevent player being able to attack dead enemy
        GetComponent<CapsuleCollider>().enabled = false;

        hpBarBG.enabled = false;    
    }

    public void TakeDamage(int damage)
    {
        OnTakeDamage?.Invoke(damage);

        currHP -= damage;

        // Update HP UI
        hpBar.fillAmount = currHP / 100f;

        // Death
        if (currHP <= 0)
        {
            currHP = 0;

            // This prevent die several times
            if (!isDead)
            {
                Die();
                isDead = true;
            }
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
