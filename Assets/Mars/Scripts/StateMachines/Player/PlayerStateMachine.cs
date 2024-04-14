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
}
