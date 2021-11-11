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
        CurrentPatrol = Agent.GetPatrolPoint;
        Agent.NavAgent.SetDestination(CurrentPatrol.position);
        Agent.NavAgent.speed = Speed;
    }
    public override void RunUpdate()
    {
        if (Agent.NavAgent.remainingDistance < 2.0f)
        {
            if (PrototypeScript.allowedMove == true)
            {
                CurrentPatrol = Agent.GetPatrolPoint;
                Agent.NavAgent.SetDestination(CurrentPatrol.position);
            }
            else
            {
                Agent.NavAgent.SetDestination(Agent.transform.position);
            }
        }

        if (!Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer) && (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) < aggroDistance))
        {
            StateMachine.ChangeState<RangedAiAimState>();
        }
    }
}
