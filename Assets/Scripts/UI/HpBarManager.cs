using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Image hpBarBackground;
    [SerializeField] Image hpBar;

    void Start()
    {
        health.OnTakeDamage += UpdateHPBar;
        health.OnDie += TurnOffHPBar;

        // When the game starts, update HP bar.
        // Parameter doesn't do anything here.
        UpdateHPBar(0);
    }

    // Parameter doesn't do anything here.
    // The damage is already dealt to CurrHP in Health
    public void UpdateHPBar(int damage)
    {
        hpBar.fillAmount = health.CurrHealth / health.MaxHealth;
    }

    private void TurnOffHPBar()
    {
        hpBarBackground.enabled = false;
    }
}
