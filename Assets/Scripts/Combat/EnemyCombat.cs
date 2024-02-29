using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static CursorManager;

public class EnemyCombat : MonoBehaviour, IFightable
{
    [SerializeField] private PlayerCombat target;
    [SerializeField] Image hpBarBG; // To turn off when it's dead
    [SerializeField] Image hpBar;
    [SerializeField] UnityEvent<int> OnTakeDamage;
    [SerializeField] GameObject[] dropItems;

    Animator _animator;
    EnemyFSM _enemyFSM;

    int maxHP = 100;
    int currHP;

    bool isDead = false;

    private int attackDamage = 10;

    private float timeSinceLastAttack;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyFSM = GetComponent<EnemyFSM>();

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
        AudioManager.Singleton.PlaySoundEffect("SandZombieAttack");

        target.TakeDamage(attackDamage);
    }

    public void CheckAttackCondition(AttackType type, float attackRadius, float attackCoolTime)
    {
        if (target != null && !target.IsDead())
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

        AudioManager.Singleton.PlaySoundEffect("SandZombieDeath");

        // To prevent enemy following player after death
        _enemyFSM.enabled = false;
        // To prevent player being able to attack dead enemy
        this.GetComponent<CapsuleCollider>().enabled = false;

        hpBarBG.enabled = false;   
        
        // Enemy drops the items
        foreach(GameObject item in dropItems)
        {
            Instantiate(item, this.transform.position, Quaternion.identity, this.transform);
        }
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
