using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // Singleton instance
    public static PlayerStateMachine Instance { get; private set; }

    // Your existing serialized fields...

    private void Awake()
    {
        // Ensure there's only one instance of PlayerStateMachine
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the object persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }
    }

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

    // Default attack damage values
    private int[] defaultAttackDamages = { 10, 15, 25 };


    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));

        // Load attack damage values when the game starts
        LoadAttackDamages();
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

    private void HandleTakeDamage(int damage)
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    // Function to update attack damage based on level 
    public void UpdateAttackDamageForLevel(int level)
    {

        // Loop through all attacks and update their damage
        foreach (Attack attack in Attacks)
        {

            int oldDamage = attack.Damage; // Store the original damage

            // Example: Increase damage by 2 points per level So, After reaching Level 2 = 1st Attack damage would be (10 + 4)[lvl(2) * (2)] =14 then Level 3 = 16 ..
            int damageIncreaseAsInt = (int)(level * 2f); // Cast float to int
            attack.IncreaseDamage(damageIncreaseAsInt);

            Debug.Log($"Attack '{attack.AnimationName}' damage increased from {oldDamage} to {attack.Damage}");
        }
    }

    // Function to update attack damage based on number of sword in the inventory 
    public void UpdateAttackDamageForSwordsBought()
    {
        // Get the number of sword items in the inventory
        int attackBonus = 5;
        // Loop through all attacks and update their damage based on sword quantity
        foreach (Attack attack in Attacks)
        {
            int oldDamage = attack.Damage; // Store the original damage

            // Example: Increase damage by 2 points per sword
            int damageIncrease = attackBonus;

            // Update attack's damage
            attack.IncreaseDamage(damageIncrease);

            Debug.Log($"Attack '{attack.AnimationName}' damage increased from {oldDamage} to {attack.Damage} based on sword quantity");
        }
    }

    private void LoadAttackDamages()
    {
        for (int i = 0; i < Attacks.Length; i++)
        {
            // Use PlayerPrefs to get the saved attack damage value
            int savedDamage = PlayerPrefs.GetInt($"AttackDamage{i}", defaultAttackDamages[i]);

            // Apply the saved or default attack damage to the Attack object
            Attacks[i].Damage = savedDamage;
        }
    }

    // Function to save attack damage values to PlayerPrefs
    private void SaveAttackDamages()
    {
        for (int i = 0; i < Attacks.Length; i++)
        {
            // Save the current attack damage value to PlayerPrefs
            PlayerPrefs.SetInt($"AttackDamage{i}", Attacks[i].Damage);
        }

        // Save PlayerPrefs data to disk
        PlayerPrefs.Save();
    }

    public void ResetAttackDamageToDefault()
    {
        for (int i = 0; i < Attacks.Length; i++)
        {
            // Set the attack's damage to its default value
            Attacks[i].Damage = defaultAttackDamages[i];
        }
    }

    private void OnDestroy()
    {
        
        SaveAttackDamages();
    }
    private void OnApplicationQuit()
    {
        ResetAttackDamageToDefault();
    }
}
