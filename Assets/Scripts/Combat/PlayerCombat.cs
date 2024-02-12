using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCombat : MonoBehaviour, IFightable
{
    [SerializeField] Image hpBar;
    
    Animator _animator;
    newPlayerMovement _newPlayerMovement;

    int maxHP = 100;
    int currHP;

    float attackRadius = 5;

    float attackCool = 1.5f;
    float timeSinceAttack = 0;

    int playerDamage = 20;

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

        CheckAttackCondition();
    }

    public void CheckAttackCondition()
    {
        // Shoot a ray based on the mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Get all the hits info
        RaycastHit[] hits = Physics.RaycastAll(ray, attackRadius);

        // Check the hits one by one if it's enemy
        foreach (RaycastHit hit in hits)
        {
            GameObject target = hit.transform.gameObject;

            if (target == null) { continue; }

            else if (hit.transform.CompareTag("Enemy"))
            {
                if (timeSinceAttack > attackCool)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Attack(target);
                    }
                }
            }
        }
    }

    public void Attack(GameObject target)
    {
        this.transform.LookAt(target.transform);
        target.GetComponent<EnemyCombat>().TakeDamage(playerDamage);

        _animator.SetTrigger("Punch");

        timeSinceAttack = 0;
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
