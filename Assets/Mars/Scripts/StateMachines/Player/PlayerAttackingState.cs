using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private Attack currentAttack; // Use a more descriptive name

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        currentAttack = stateMachine.Attacks[attackIndex]; // Access Attack data
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(currentAttack.Damage, currentAttack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(currentAttack.AnimationName, currentAttack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= currentAttack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.InputReader2.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }

        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (currentAttack.ComboStateIndex == -1) { return; }

        if (normalizedTime < currentAttack.ComboAttackTime) { return; }

        stateMachine.SwitchState(
            new PlayerAttackingState(
                stateMachine,
                currentAttack.ComboStateIndex
            )
        );
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * currentAttack.Force);
        Debug.Log($"Experience Gained: {currentAttack.ExperiencePoints}"); // Add debug log
        stateMachine.AddExperience(currentAttack.ExperiencePoints); // Add experience to player

        alreadyAppliedForce = true;
    }

}
