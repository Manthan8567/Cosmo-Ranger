using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Image healthBar;
    public Image expBar;
    public Text levelText;

    public Health playerHealth;
    public ExperienceManager playerExperience;

    private void Start()
    {
        if (playerHealth != null)
        {
            //playerHealth.OnTakeDamage += (damage) => UpdateHealthBar(damage);
            //playerHealth.OnHeal += UpdateHealthBar;

            playerHealth.OnTakeDamage += (damage) => UpdateHealthBar(damage);
            playerHealth.OnHeal += UpdateHealthBar;
        }

        if (playerExperience != null)
        {
            playerExperience.OnGainExperience += UpdateExpBar;
            playerExperience.OnLevelUp += UpdateLevelText;
        }


        UpdateExpBar();
        UpdateLevelText();
        UpdateHealthBar(0);
    }

    // Modify UpdateHealthBar method to accept amount healed
    public void UpdateHealthBar(int amount)
    {
        if (healthBar != null && playerHealth != null)
        {
            healthBar.fillAmount = playerHealth.CurrHealth / playerHealth.MaxHealth;
        }
    }

    public void UpdateExpBar()
    {
        if (expBar != null && playerExperience != null)
        {
            if (playerExperience.currentExperience >= playerExperience.expForNextLevel)
            {
                // Player has leveled up, reset the experience bar to empty
                expBar.fillAmount = 0f;
            }
            else
            {
                // Update the fill amount based on current experience and exp for next level
                expBar.fillAmount = playerExperience.currentExperience / playerExperience.expForNextLevel;
            }
        }
    }

    public void UpdateLevelText()
    {
        if (levelText != null && playerExperience != null)
        {
            levelText.text = $"{playerExperience.currentLevel}";
        }
    }
}
