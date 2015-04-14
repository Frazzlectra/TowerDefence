using UnityEngine;
using System.Collections;

public class EnemyMinion : Creations {
    GameObject targetObj;//target GameObject
    Vector3 target;
    Rigidbody enemyRigidbody;
    float[] distanceToEnemies = new float[GameManager.blueTeam.Length];
    
    public float moveSpeed = 5;

    protected override void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        startingHealth = 100;
        damage = 10;
        attackRange = 5;
        base.Awake();
    }
    protected override void TakeDamage(float damage)
    {
        //play sound!!
        base.TakeDamage(damage);
    }
    void FixedUpdate()
    {

        if (targetObj != null)
        {
            target = new Vector3(targetObj.transform.position.x, transform.position.y, targetObj.transform.position.z);//get the postion to travel to
            transform.LookAt(targetObj.transform);//look where your going
            if (Vector3.Distance(target, transform.position) > attackRange)//if your not close enough to attack... add force
            {
                enemyRigidbody.AddForce(Vector3.forward * moveSpeed, ForceMode.Acceleration);
                Debug.Log("addForce");
            } 
        }
        else
        {
            GetTarget();            
        }

        
    }
    void GetTarget()
    {
        float closestEnemyDistance = 10000;
        int closestEnemy = 0;
        //find the closest enemy in blueTeam array
        for (int i = 0; i < GameManager.blueTeam.Length; i++)
        {
            if (GameManager.blueTeam[i] != null)
            {
                distanceToEnemies[i] = Vector3.Distance(transform.position, GameManager.blueTeam[i].transform.position);
            }
            if (distanceToEnemies[i] < closestEnemyDistance && distanceToEnemies[i] > 0)
            {
                closestEnemyDistance = distanceToEnemies[i];
                closestEnemy = i;
                //Debug.Log("Closest Enemy: " + closestEnemy + " distance: " + closestEnemyDistance);
            }
        }
        targetObj = GameManager.blueTeam[closestEnemy];
    }
}
