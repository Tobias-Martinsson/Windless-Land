using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class RangedAiPatrol : State
{
    public float aggroDistance;
    public float Speed;
    SomeAgent Agent;

    private Transform CurrentPatrol;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
    }

    public override void Enter()
    {
        CurrentPatrol = Agent.GetPatrolPoint();
        Agent.NavAgent.SetDestination(CurrentPatrol.position);
        Agent.NavAgent.speed = Speed;

        Agent.animator.SetTrigger("StopBow");
        Agent.animator.SetFloat("XSpeed", 0);
        Agent.animator.SetFloat("YSpeed", 1);
    }
    public override void RunUpdate()
    {
        if (Agent.NavAgent.remainingDistance < 2.0f)
        {
            CurrentPatrol = Agent.GetPatrolPoint();
            Agent.NavAgent.SetDestination(CurrentPatrol.position);

            Agent.animator.SetFloat("XSpeed", 0);
            Agent.animator.SetFloat("YSpeed", 1);
        }

        if (!Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer) && (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) < aggroDistance))
        {
            StateMachine.ChangeState<RangedAiAimState>();
        }
    }
}
