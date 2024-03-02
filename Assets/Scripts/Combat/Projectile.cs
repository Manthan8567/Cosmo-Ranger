using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 3;

    newHealth target = null;

    int fireballDamage = 30;


    void Update()
    {
        MoveToTarget(target);
    }

    public void SetTarget(newHealth target)
    {
        this.target = target;
    }

    public void MoveToTarget(newHealth target)
    {
        transform.LookAt(GetAimingPos(target));
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public Vector3 GetAimingPos(newHealth target)
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
        newHealth enemy = other.GetComponent<newHealth>();

        if (enemy != null && enemy.CompareTag("Enemy"))
        {
            enemy.TakeDamage(fireballDamage);

            AudioManager.Singleton.PlaySoundEffect("HitFireball");

            Destroy(this.gameObject);
        }
    }
}
