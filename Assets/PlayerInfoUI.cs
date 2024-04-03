using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoUI : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI currentExpText;
    public TextMeshProUGUI nextLevelExpText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attack1Text;
    public TextMeshProUGUI attack2Text;
    public TextMeshProUGUI attack3Text;
    public TextMeshProUGUI totalKillText; // New field for displaying total kills

    private ExperienceManager experienceManager;
    private PlayerStateMachine playerStateMachine;

    private int totalKills = 0; // Variable to store total kills

    private void Start()
    {
        // Find and assign the ExperienceManager component
        experienceManager = FindObjectOfType<ExperienceManager>();

        // Find and assign the PlayerStateMachine component
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();

        // Subscribe to events
        if (experienceManager != null)
        {
            experienceManager.OnGainExperience += UpdateUI;
            experienceManager.OnLevelUp += UpdateUI;
        }

        // Update UI initially
        UpdateUI();
    }

    private void Update()
    {
        // Continuously update UI to reflect changes in player health
        UpdateHealthUI();
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to avoid memory leaks
        if (experienceManager != null)
        {
            experienceManager.OnGainExperience -= UpdateUI;
            experienceManager.OnLevelUp -= UpdateUI;
        }
    }

    private void UpdateUI()
    {
        if (experienceManager == null || playerStateMachine == null)
            return;

        // Update UI elements with player information
        currentLevelText.text = "Current Level: Level " + experienceManager.currentLevel.ToString();
        currentExpText.text = "Current Exp: " + experienceManager.currentExperience.ToString() + " exp";
        nextLevelExpText.text = "Next Level: " + experienceManager.expForNextLevel.ToString() + " exp required";
        attack1Text.text = "Attack 1 Damage: " + GetAttackDamage(0).ToString();
        attack2Text.text = "Attack 2 Damage: " + GetAttackDamage(1).ToString();
        attack3Text.text = "Attack 3 Damage: " + GetAttackDamage(2).ToString();
        totalKillText.text = "Total Kill: " + totalKills.ToString() + " kills";
    }

    private void UpdateHealthUI()
    {
        if (playerStateMachine == null)
            return;

        // Update health UI element
        healthText.text = "Health: " + playerStateMachine.Health.CurrHealth.ToString();
    }

    private int GetAttackDamage(int attackIndex)
    {
        // Ensure the attack index is within the range of attacks
        if (attackIndex < 0 || attackIndex >= playerStateMachine.Attacks.Length)
            return 0;

        // Return the damage of the specified attack
        return playerStateMachine.Attacks[attackIndex].Damage;
    }

    // Method to increment total kill count
    public void IncrementTotalKills()
    {
        totalKills++;
        UpdateUI(); // Update UI after incrementing total kills
    }
}
