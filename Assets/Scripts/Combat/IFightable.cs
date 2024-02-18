using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IFightable
{
    public void CheckAttackCondition(AttackType type, float attackRadius, float attackCoolTime);
    public void Punch();
    public void TakeDamage(int damage);
    public void Die();
    public bool IsDead();
}

public enum AttackType
{
    PUNCH,
    SPELL
}