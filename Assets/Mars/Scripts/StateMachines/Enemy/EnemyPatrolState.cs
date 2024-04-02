using UnityEngine;


public class EnemyPatrolState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private float animSpeed; // animation speed parameter

    private int currWayPointIndex = 0;
    private float distanceCloseEnoughToWayPoint = 2f;

    private float timeSinceReachWayPoint = 0;
    private float patrolBreakTime = 3f;


    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private Vector3 GetCurrWayPoint() { return stateMachine.PatrolPath.GetWayPoint(currWayPointIndex); }

    public override void Enter()
    {
        FaceTarget(GetCurrWayPoint());

        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Patrol(deltaTime);

        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
    }

    public override void Exit() { }

    private void Patrol(float deltaTime)
    {
        MoveToWayPoint(deltaTime);

        // Move to the next way point after waiting for patrolBreakTime
        if (stateMachine.PatrolPath != null)
        {
            if (IsAtWayPoint())
            {
                timeSinceReachWayPoint += deltaTime;

                if (timeSinceReachWayPoint > patrolBreakTime)
                {
                    // Change current way point to the next one
                    currWayPointIndex = stateMachine.PatrolPath.GetNextIndex(currWayPointIndex);

                    FaceTarget(GetCurrWayPoint());

                    timeSinceReachWayPoint = 0;
                }
            }
        }
    }

    private void MoveToWayPoint(float deltaTime)
    {
        // Move to the current way point
        if (stateMachine.Agent.isOnNavMesh)
        {
            Vector3 patrolSpeed = stateMachine.Agent.desiredVelocity.normalized * stateMachine.PatrolSpeed;

            stateMachine.Agent.destination = GetCurrWayPoint();

            Move(patrolSpeed, deltaTime);

            stateMachine.Agent.velocity = stateMachine.Controller.velocity;

            // Play walking animation based on its speed
            animSpeed = patrolSpeed.magnitude / 2;
            stateMachine.Animator.SetFloat(SpeedHash, animSpeed, AnimatorDampTime, deltaTime);
        }
    }

    private bool IsAtWayPoint()
    {
        float distanceBetweenWayPoint = Vector3.Distance(stateMachine.transform.position, GetCurrWayPoint());

        return distanceBetweenWayPoint < distanceCloseEnoughToWayPoint;
    }
}
