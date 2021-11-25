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
    public float aggroDistance;
    public GameObject addEnemy;
    private Transform CurrentPatrol;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalTime = attackCooldown;

    }

    public override void Enter()
    {
        int currentHealth = Agent.GetComponent<EnemyHealthScript>().health;
        int maxHealth = Agent.GetComponent<EnemyHealthScript>().Maxhealth;
        
        if(currentHealth > maxHealth * 0.1 && currentHealth < maxHealth * 0.5)
        {
            SpawnLeftEnemy();
            SpawnLeftEnemy();
            SpawnRightEnemy();
            SpawnRightEnemy();
        }
        else if(currentHealth > maxHealth * 0.5)
        {
            SpawnLeftEnemy();
            SpawnRightEnemy();
        }
        totalArrows = arrowAmount;
    }

    public override void RunUpdate()
    {
        CurrentPatrol = Agent.GetPatrolPointByindex(1);
        Agent.NavAgent.SetDestination(CurrentPatrol.position);
        if (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) <= aggroDistance)
        {
            if (totalArrows > 0)
            {
                
                Agent.NavAgent.updateRotation = false;
                Vector3 targetPosition = new Vector3(Agent.Player.position.x,
                                            Agent.transform.position.y,
                                            Agent.Player.position.z);
                Agent.transform.LookAt(targetPosition);
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
                CurrentPatrol = Agent.GetPatrolPointByindex(0);
                Agent.NavAgent.SetDestination(CurrentPatrol.position);
                StateMachine.ChangeState<BossChooseAttackState>();
            }
        }
        else
        {
            StateMachine.ChangeState<BossIdleScript>();
        }
        
    }

    public void SpawnLeftEnemy()
    {
        GameObject add = Instantiate(addEnemy);
        add.transform.position = Agent.GetComponent<BossMechanicsScript>().adSpawnPoint1.transform.position;
    }

    public void SpawnRightEnemy()
    {
        GameObject add = Instantiate(addEnemy);
        add.transform.position = Agent.GetComponent<BossMechanicsScript>().adSpawnPoint2.transform.position;
    }
}
