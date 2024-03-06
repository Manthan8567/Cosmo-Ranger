using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class newHealth : MonoBehaviour
{
    [SerializeField] Image hpBarBG; // To turn off when it's dead
    [SerializeField] Image hpBar;
    [SerializeField] UnityEvent<int> OnTakeDamage;
    [SerializeField] UnityEvent<bool> OnDie;
    [SerializeField] GameObject[] dropItems; // This is for enemies
    [SerializeField] float maxHP = 100;

    Animator _animator;

    float currHP;

    public bool IsDead { get; private set; }

    void Start()
    {
        _animator = GetComponent<Animator>();
        currHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        OnTakeDamage?.Invoke(damage);

        currHP -= damage;

        // Update HP UI
        hpBar.fillAmount = currHP / maxHP;

        // Death
        if (currHP <= 0)
        {
            currHP = 0;

            // This prevent die several times
            if (!IsDead)
            {
                Die();
                IsDead = true;
            }
        }
    }

    public void Die()
    {
        _animator.SetTrigger("Die");

        OnDie?.Invoke(IsDead);

        // Enemy drops the items
        foreach (GameObject item in dropItems)
        {
            Instantiate(item, this.transform.position, item.transform.rotation, this.transform);
        }
    }
}
