using System.Collections;
using UnityEngine;


// Heal event only in Sand Planet
public class HealEventManager : MonoBehaviour
{
    [SerializeField] Health playerHealth;

    private float healAmount = 10f;
    private float healCoolTime = 10f;


    private void Start()
    {
        StartCoroutine(HealPlayer());
    }

    IEnumerator HealPlayer()
    {
        yield return new WaitForSeconds(healCoolTime);

        playerHealth.Heal(healAmount);

        StartCoroutine(HealPlayer());
    }
}
