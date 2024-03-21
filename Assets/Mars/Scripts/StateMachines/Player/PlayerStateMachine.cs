using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader2 InputReader2 { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    public Transform MainCameraTransform { get; private set; }

    public int Experience { get; private set; } = 0; // Added experience variable
    public int CurrentLevel { get; private set; } = 1; // Added current level variable

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    public void AddExperience(int experienceAmount)
    {
        Experience += experienceAmount; // Add experience to the player's total

        // Check for level up and handle it (without UI)
        int requiredExperience = GetRequiredExperienceForLevel(CurrentLevel);
        if (Experience >= requiredExperience)
        {
            CurrentLevel++;
            Debug.Log($"Leveled Up! Current Level: {CurrentLevel}"); // Log level up message

            // Update attack damage based on level 
            UpdateAttackDamageForLevel(CurrentLevel);

            // Restore player's health on level up
            Health.RestoreHealth(); 
        }
    }

    // Function to update attack damage based on level 
    private void UpdateAttackDamageForLevel(int level)
    {
     
        // Loop through all attacks and update their damage
        foreach (Attack attack in Attacks)
        {
            // Example: Increase damage by 3 points per level
            int damageIncreaseAsInt = (int)(level * 3f); // Cast float to int
            attack.IncreaseDamage(damageIncreaseAsInt);
        }
    }

    // Function to calculate required experience for a level 
    private int GetRequiredExperienceForLevel(int level)
    {
        return level * 100; // Example: 100 experience points per level
    }
}
