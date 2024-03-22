using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public void UpdateDamageText(int damage)
    {
        GetComponent<TextMeshProUGUI>().text = damage.ToString();
    }

    // This will be called by DamageText-Animation-Event
    public void DestroyDamageText()
    {
        Destroy(gameObject);
    }
}
