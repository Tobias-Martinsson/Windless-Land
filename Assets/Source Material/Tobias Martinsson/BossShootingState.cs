using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class BossShootingState : State
{
    SomeAgent Agent;
    public float attackCooldown;
    private float originalTime;
    public float arrowAmount;
    private float totalArrows;
    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalTime = attackCooldown;
    }

    public override void Enter()
    {
        totalArrows = arrowAmount;
    }

    public override void RunUpdate()
    {
        if(totalArrows > 0)
        {
            Agent.NavAgent.updateRotation = false;
            Vector3 targetPostition = new Vector3(Agent.Player.position.x,
                                        Agent.transform.position.y,
                                        Agent.Player.position.z);
            Agent.transform.LookAt(targetPostition);
            Vector3.RotateTowards(Agent.transform.position, Agent.PlayerPosition, 2, 0);

            attackCooldown -= Time.deltaTime;
            if (attackCooldown < 0)
            {
                Agent.GetComponent<ArrowScript>().shootArrow();
                totalArrows--;
                attackCooldown = originalTime;
            }
        }
        else if (totalArrows == 0)
        {
            StateMachine.ChangeState<BossChooseAttackState>();
        }
        
    }

}
