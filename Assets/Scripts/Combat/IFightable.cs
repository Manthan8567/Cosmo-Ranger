using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IFightable
{
    public void CheckAttackCondition(AttackType type, float attackRadius, float attackCoolTime);
}

public enum AttackType
{
    PUNCH,
    FIREBALL
}