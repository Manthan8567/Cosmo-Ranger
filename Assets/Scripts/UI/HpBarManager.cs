using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HpBarManager : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;


    void Start()
    {
        ExperienceManager.Singleton.OnLevelUp += FillHPBarFull;
        health.OnTakeDamage += UpdateHPBar;
        health.OnDie += TurnOffHPBar;

        health.OnHeal += UpdateHPBar;

        // When the game starts, update HP bar.
        // Parameter doesn't do anything here.
        UpdateHPBar(0);
    }

    private void OnDisable()
    {
        ExperienceManager.Singleton.OnLevelUp -= FillHPBarFull;
        health.OnTakeDamage -= UpdateHPBar;
        health.OnDie -= TurnOffHPBar;

        health.OnHeal += UpdateHPBar;
    }

    public void FillHPBarFull()
    {
        hpBar.fillAmount = 1;

        UpdateHpText();
    }

    // Parameter doesn't do anything here.
    // The damage is already dealt to CurrHP in Health
    public void UpdateHPBar(int damage)
    {
        // Update hp bar
        hpBar.fillAmount = health.CurrHealth / health.MaxHealth;

        UpdateHpText();
    }

    private void UpdateHpText()
    {
        // Update hp text
        string currHp = health.CurrHealth.ToString();
        string maxHp = health.MaxHealth.ToString();
        hpText.text = currHp + "/" + maxHp;
    }

    private void TurnOffHPBar()
    {
        //hpBarBackground.enabled = false;
        this.gameObject.SetActive(false);
    }
}
