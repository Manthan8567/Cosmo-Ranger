using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static CursorManager;

public class EnemyCombat : MonoBehaviour, IFightable
{
    [SerializeField] private newHealth target;

    Animator _animator;

    private int attackDamage = 10;

    private float timeSinceLastAttack;


    private void Start()
    {
        _animator = GetComponent<Animator>();
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

    // Animation(Punch) event
    void Hit()
    {
        AudioManager.Singleton.PlaySoundEffect("SandZombieAttack");

        target.TakeDamage(attackDamage);
    }

    public void CheckAttackCondition(AttackType type, float attackRadius, float attackCoolTime)
    {
        if (target != null && !target.IsDead)
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
}
