using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public Health playerHealth; // Reference to the Health component
    public int currentLevel = 1;
    public int currentExperience = 0;
    public int expForNextLevel = 100;

    public PlayerStateMachine playerStateMachine; // Get a Reference to the PlayerStateMachine to update attack damage

    public void AddExperience(int experience)
    {
        currentExperience += experience;

        // Check if level up is required
        if (currentExperience >= expForNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExperience -= expForNextLevel;
        expForNextLevel *= 2;

        // Restore max health upon leveling up 
        if (playerHealth != null)
        {
            playerHealth.Heal(playerHealth.GetMaxHealth()); // Restore health using public method from Health class
        }

        // Update attack damage upon leveling up
        if (playerStateMachine != null)
        {
            playerStateMachine.UpdateAttackDamageForLevel(currentLevel);
        }

        Debug.Log($"Leveled Up! Current Level: {currentLevel}");
        Debug.Log($"Current Experience: {currentExperience}");
        Debug.Log($"Required Exp for Next Level: {expForNextLevel}");
        Debug.Log($"Health fully restored to: {playerHealth.GetMaxHealth()}"); // Debug message for health restoration
    }


}