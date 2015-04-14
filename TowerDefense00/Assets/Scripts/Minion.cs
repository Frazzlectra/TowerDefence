using UnityEngine;
using System.Collections;

public class Minion : Creations {
    //GameObject targetObj;//target GameObject
    //Vector3 target;
    //Transform targetPoint;
    NavMeshAgent minionNav;

    float distanceToEnemies;
    public GameObject[] swarmPoints;
    public int gold; 

    //static bool[] swarmPointTaken;

    //attacking effects
    //float timer;
    //float timeBetweenAttacks = 1f;
    
    public float moveSpeed = 5;

    protected override void Awake()
    {
        minionNav = GetComponent<NavMeshAgent>();
        //startingHealth = 100;
        //damage = 10;
        //attackRange = 10;
        swarmPointTaken = new bool[swarmPoints.Length];
        shotLine = GetComponentInChildren<LineRenderer>();
        //shotLine.SetPosition(0, new Vector3(0, 0, 0));
        base.Awake();
    }

    public override void TakeDamage(float takeDamage)
    {
        //play sound??
        base.TakeDamage(takeDamage);
    }

    public override void Update()
    {
        GetTarget(gameObject.tag, distanceToEnemies);
        if (targetPoint != null)
        {
            minionNav.SetDestination(targetPoint.position);
        }
        base.Update();
        if (targetObj != null && Vector3.Distance(targetObj.transform.position, transform.position) < attackRange && minionNav != null)
        {           
            minionNav.SetDestination(transform.position);
        }
        if (currentHealth < 0)
        {
            Death();
        }
    }

    protected override void GetTarget(string team, float distanceToEnemies)
    {
        base.GetTarget(team, distanceToEnemies);
        if (targetObj == GameManager.blueTeam[0])
        {
            SetSwarmPoints(swarmPoints);
        }
    }
    protected override void SetSwarmPoints(GameObject[] swarmPoints)
    {
        base.SetSwarmPoints(swarmPoints);
        //minionNav.SetDestination(targetPoint.position);
    }

    public override void Death()
    {
        if (gameObject.tag == "redTeam")
        {            
            HudEffects.score += 10;
            HudEffects.gold += gold;
            --GameManager.numEnemyMinions;
        }
        else if (gameObject.tag =="blueTeam")
        {
            --GameManager.numMinions;
        }
        base.Death();
    } 
}
