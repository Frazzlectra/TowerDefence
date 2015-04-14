using UnityEngine;
using System.Collections;

public class Creations : MonoBehaviour {
    public float startingHealth = 100;
    public float currentHealth;
    public float damage;
    public float attackRange;

    public LineRenderer shotLine;
    protected GameObject targetObj;//target GameObject
    Vector3 target;
    protected Transform targetPoint;
    protected static bool[] swarmPointTaken;
    bool targetSwarmPoint;
    float timer;
    public float timeBetweenAttacks = 1f;
    protected virtual void Awake()
    {
        swarmPointTaken = new bool[6];//swarmPoints.Length
        currentHealth = startingHealth;
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer > .5f)
        {
            DisableEffects();
        }
        if (targetObj != null && Vector3.Distance(targetObj.transform.position, transform.position) < attackRange && timer > timeBetweenAttacks)
        {
            //Debug.Log("Attack?");
            timer = 0;
            //minionNav.SetDestination(transform.position);
            StartShooting(targetObj);
            //Debug.Log("sendMessage(TakeDamage)");
        }
    }

    public virtual void TakeDamage(float takeDamage)
    {
        if (currentHealth < 0)
        {
            Death();
            return;
        }
        currentHealth -= takeDamage;
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }

    protected virtual void DisableEffects()
    {
        shotLine.enabled = false;
    }

    protected virtual void StartShooting(GameObject targetObj)
    {
        targetObj.SendMessage("TakeDamage", damage);
        shotLine.enabled = true;
        shotLine.SetPosition(0, transform.position);
        shotLine.SetPosition(1, targetObj.transform.position);
    }

    protected virtual void GetTarget(string team, float distanceToEnemies)
    {
        int nullEnemies = 0;
        float closestEnemyDistance = 10000;
        int closestEnemy = 0;
        //find the closest enemy in blueTeam array
        if (team == "redTeam")
        {
            for (int i = 0; i < GameManager.blueTeam.Length; i++)
            {
                if (GameManager.blueTeam[i] != null)
                {
                    distanceToEnemies = Vector3.Distance(transform.position, GameManager.blueTeam[i].transform.position);

                    if (distanceToEnemies < closestEnemyDistance && distanceToEnemies > 0)
                    {
                        closestEnemyDistance = distanceToEnemies;
                        closestEnemy = i;
                        // Debug.Log("Closest Enemy: " + closestEnemy /*+ " distance: " + closestEnemyDistance*/);
                    }
                }
            }
            if (GameManager.blueTeam[closestEnemy] != null)
            {
                if (GameManager.blueTeam[closestEnemy] != GameManager.blueTeam[0])
                {
                    targetSwarmPoint = false;
                }
                if (!targetSwarmPoint)
                {
                    targetObj = GameManager.blueTeam[closestEnemy];
                    targetPoint = targetObj.transform;
                }
            }
        }
        if (team == "blueTeam")
        {
            for (int i = 0; i < GameManager.redTeam.Length; i++)
            {
                if (GameManager.redTeam[i] != null)
                {
                    distanceToEnemies = Vector3.Distance(transform.position, GameManager.redTeam[i].transform.position);
                    //if the enemy distance you just checked is less then the pervious shortest distance switch targets
                    if (distanceToEnemies < closestEnemyDistance && distanceToEnemies > 0)
                    {
                        closestEnemyDistance = distanceToEnemies;
                        closestEnemy = i;
                        //Debug.Log("Closest Enemy: " + closestEnemy + " distance: " + closestEnemyDistance);
                    }
                }
                else
                {
                    ++nullEnemies;
                }
                if (nullEnemies >= GameManager.redTeam.Length)
                {
                    targetPoint = transform;
                    //minionNav.SetDestination(targetPoint.position);
                }
            }
            if (GameManager.redTeam[closestEnemy] != null)//target point transform location the enemies will move to assigned in personal script
            {
                targetObj = GameManager.redTeam[closestEnemy];
                targetPoint = targetObj.transform;
                Debug.DrawLine(transform.position, targetObj.transform.position);
            }
        }
    }

    protected virtual void SetSwarmPoints(GameObject[] swarmPoints)
    {
        //set positions around the tower
        for (int i = 0; i < swarmPointTaken.Length; i++)
        {
            if (!swarmPointTaken[i])
            {
                swarmPointTaken[i] = true;
                targetPoint = swarmPoints[i].transform;
                //Debug.Log("spawnPointSet: " + targetPoint);
                targetSwarmPoint = true;
                //Debug.Log("target transform" + swarmPoints[i].transform);
                break;
            }
        }
        //minionNav.SetDestination(targetPoint.position);
        Debug.DrawLine(transform.position, targetPoint.position);
    }

}

