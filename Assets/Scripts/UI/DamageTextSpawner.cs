using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] DamageText damageTextPrefab;

    public void Spawn(int damage)
    {
        DamageText _damageText = Instantiate<DamageText>(damageTextPrefab, transform.parent);
        _damageText.UpdateDamageText(damage);
    }
}
