using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 3;

    EnemyCombat target = null;

    int fireballDamage = 30;


    void Update()
    {
        MoveToTarget(target);
    }

    public void SetTarget(EnemyCombat target)
    {
        this.target = target;
    }

    public void MoveToTarget(EnemyCombat target)
    {
        transform.LookAt(GetAimingPos(target));
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public Vector3 GetAimingPos(EnemyCombat target)
    {
        if (target == null)
        {
            return Vector3.zero;
        }

        CapsuleCollider targetCapsuleCol = target.GetComponent<CapsuleCollider>();

        // projectile will hit the middle of target's collider
        return target.transform.position + Vector3.up * targetCapsuleCol.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyCombat enemy = other.GetComponent<EnemyCombat>();

        if (enemy != null)
        {
            enemy.TakeDamage(fireballDamage);

            AudioManager.Singleton.PlaySoundEffect("HitFireball");

            Destroy(this.gameObject);
        }
    }
}
