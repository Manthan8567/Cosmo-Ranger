using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] PlayerStateMachine playerStateMachine;

    public GameObject uiPanel; // Reference to the UI panel GameObject

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI currentExpText;
    public TextMeshProUGUI nextLevelExpText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attack1Text;
    public TextMeshProUGUI attack2Text;
    public TextMeshProUGUI attack3Text;
    public TextMeshProUGUI totalKillText; // New field for displaying total kills

    //private ExperienceManager experienceManager;
    

    public int totalKills { get; private set; } = 0; // Variable to store total kills


    private void Start()
    {
        // Disable the UI panel initially
        uiPanel.SetActive(false);

        // Subscribe to events
        ExperienceManager.Singleton.OnLevelUp += ShowLevelUpUI;

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
        // Unsubscribe from events
        ExperienceManager.Singleton.OnLevelUp -= ShowLevelUpUI;
    }

    private void UpdateUI()
    {
        if (playerStateMachine == null)
            return;

        // Update UI elements with player information
        currentLevelText.text = "Current Level: Level " + ExperienceManager.Singleton.currentLevel.ToString();
        currentExpText.text = "Current Exp: " + ExperienceManager.Singleton.currentExperience.ToString() + " exp";
        nextLevelExpText.text = "Next Level: " + ExperienceManager.Singleton.expForNextLevel.ToString() + " exp required";
        attack1Text.text = "Attack 1 Damage: " + GetAttackDamage(0).ToString();
        attack2Text.text = "Attack 2 Damage: " + GetAttackDamage(1).ToString();
        attack3Text.text = "Attack 3 Damage: " + GetAttackDamage(2).ToString();
        totalKillText.text = "Total Kill: " + totalKills.ToString() + " kills";
    }

    private void ShowLevelUpUI()
    {
        // Enable the UI panel when the player levels up
        uiPanel.SetActive(true);

        // Update UI elements
        UpdateUI();

        // Start a coroutine to disable the UI panel after a certain duration
        StartCoroutine(DisableUIAfterDelay());
    }

    private IEnumerator DisableUIAfterDelay()
    {
        // Wait for a certain duration
        yield return new WaitForSeconds(5f); // Adjust the duration as needed

        // Disable the UI panel
        uiPanel.SetActive(false);
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
