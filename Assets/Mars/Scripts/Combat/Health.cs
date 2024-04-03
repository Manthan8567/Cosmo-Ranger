using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currHealth;

    private bool isInvunerable;

    public event Action<int> OnTakeDamage;
    public event Action<int> OnHeal;
    public event Action OnDie;

    public float MaxHealth { get => maxHealth; }
    public float CurrHealth { get => currHealth; }

    public bool IsDead => currHealth == 0;

    private void Awake()
    {
        currHealth = maxHealth;
    }

    public void SetInvunerable(bool isInvunerable)
    {
        this.isInvunerable = isInvunerable;
    }

    public void DealDamage(int damage)
    {
        if (currHealth == 0) { return; }

        if (isInvunerable) { return; }

        currHealth = Mathf.Max(currHealth - damage, 0);

        OnTakeDamage?.Invoke(damage);

        if (currHealth == 0)
        {
            OnDie?.Invoke();
        }
    }

    public void Heal(float healAmount)
    {
        currHealth = Mathf.Min(currHealth + healAmount, maxHealth);
        OnHeal?.Invoke((int)healAmount);
    }
}