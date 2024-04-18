using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private float experienceValue = 50; // Set experience points awarded on death 

    public override void Enter()
    {
        // Update total kills count
        GameObject playerInfoUIObject = GameObject.FindWithTag("PlayerInfoUI");
        if (playerInfoUIObject != null)
        {
            PlayerInfoUI playerInfoUI = playerInfoUIObject.GetComponent<PlayerInfoUI>();
            if (playerInfoUI != null)
            {
                playerInfoUI.IncrementTotalKills();
            }
        }

        GameObject questManagerGO = GameObject.FindWithTag("QuestManager");
        if (questManagerGO != null)
        {
            QuestManager questManager = questManagerGO.GetComponent<QuestManager>();
            if (questManager != null)
            {
                questManager.UpdateProgression();
            }
        }

        Debug.Log($"Enemy awarded {experienceValue.ToString()} experience points!");
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);

        ExperienceManager.Singleton.AddExperience(experienceValue);
    }

    public override void Tick(float deltaTime) { }

    public override void Exit() { }
}


