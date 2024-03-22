using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] DamageText damageTextPrefab;

    private void Start()
    {
        health.OnTakeDamage += Spawn;
    }

    // This will get damage value from OnTakeDamage() event
    public void Spawn(int damage)
    {
        DamageText _damageText = Instantiate(damageTextPrefab, transform);
        _damageText.UpdateDamageText(damage);
    }
}
