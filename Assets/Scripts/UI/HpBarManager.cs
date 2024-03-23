using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBarManager : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;

    private ExperienceManager experienceManager;


    void Start()
    {
        experienceManager = GameObject.FindWithTag("ExperienceManager").GetComponent<ExperienceManager>();

        experienceManager.OnLevelUp += FillHPBarFull;
        health.OnTakeDamage += UpdateHPBar;
        health.OnDie += TurnOffHPBar;

        // When the game starts, update HP bar.
        // Parameter doesn't do anything here.
        UpdateHPBar(0);
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
