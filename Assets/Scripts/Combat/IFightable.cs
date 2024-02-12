using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFightable
{
    public void CheckAttackCondition();
    public void Attack(GameObject target);
    public void TakeDamage(int damage);
    public void Die();
    public bool IsDead();
}
