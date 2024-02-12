using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour, IFightable
{
    [SerializeField] private Transform target;
    [SerializeField] Image hpBarBG; // To turn off when it's dead
    [SerializeField] Image hpBar;

    Animator _animator;

    int maxHP = 100;
    int currHP;

    bool isDead = false;

    private int attackDamage = 10;

    private float timeBetweenAttacks = 2;
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

    public void Attack(GameObject target)
    {
        this.transform.LookAt(target.transform);

        target.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        _animator.SetTrigger("Punch");

        timeSinceLastAttack = 0;
    }

    public void CheckAttackCondition()
    {
        PlayerCombat targetCombatSystem = target.GetComponent<PlayerCombat>();

        if (targetCombatSystem != null && !targetCombatSystem.IsDead())
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                Attack(target.gameObject);
            }
        }
    }

    public void Die()
    {
        _animator.SetTrigger("Die");

        // To prevent enemy following player after death
        GetComponent<EnemyFSM>().enabled = false;
        // To prevent player being able to attack dead enemy
        GetComponent<BoxCollider>().enabled = false;

        hpBarBG.enabled = false;    
    }

    public void TakeDamage(int damage)
    {
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
