using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;


public class PlayerCombat : MonoBehaviour, IFightable
{
    [SerializeField] Image hpBar;
    [SerializeField] Projectile fireball;
    [SerializeField] Transform rightHandTransform;

    EnemyCombat target;
    Animator _animator;
    newPlayerMovement _newPlayerMovement;

    public UnityEvent<int> OnTakeDamage;

    int maxHP = 100;
    int currHP;

    float punchRadius = 4;
    float punchCoolTime = 1.5f;
    int punchDamage = 20;

    float spellRadius = 10;
    float spellCoolTime = 2;

    float timeSinceAttack = 0;

    bool isDead = false;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _newPlayerMovement = GetComponent<newPlayerMovement>();

        currHP = maxHP;
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;

        // Left mouse -> Punch
        if (InputManager.punchInput == 1)
        {
            CheckAttackCondition(AttackType.PUNCH, punchRadius, punchCoolTime);
        }

        // Press 1 -> Spell -Fireball 
        if (InputManager.spell_fireballInput == 1)
        {
            CheckAttackCondition(AttackType.SPELL, spellRadius, spellCoolTime);
        }
    }

    public void CheckAttackCondition(AttackType type, float attackRadius, float attackCoolTime)
    {
        // Shoot a ray based on the mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Get all the hits info
        RaycastHit[] hits = Physics.RaycastAll(ray, attackRadius);

        // Check the hits one by one if it's an enemy
        foreach (RaycastHit hit in hits)
        {
            target = hit.transform.GetComponent<EnemyCombat>();

            if (target == null) { continue; }

            if (timeSinceAttack > attackCoolTime)
            {
                if (type == AttackType.PUNCH)
                {
                    Punch();
                }

                if (type == AttackType.SPELL)
                {
                    ThrowFireball(target);
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

    // Animation event
    void Hit()
    {
        target.GetComponent<EnemyCombat>().TakeDamage(punchDamage);
    }

    public void ThrowFireball(EnemyCombat target)
    {
        this.transform.LookAt(target.transform);

        Projectile tempProjectile = Instantiate(fireball, rightHandTransform.position, Quaternion.identity);
        tempProjectile.SetTarget(target);

        _animator.SetTrigger("Spell_Fireball");

        timeSinceAttack = 0;
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

    public void Die()
    {
        _animator.SetTrigger("Die");

        // Player cannot move
        _newPlayerMovement.enabled = false;
    }

    public bool IsDead()
    {
        return isDead;
    }
}