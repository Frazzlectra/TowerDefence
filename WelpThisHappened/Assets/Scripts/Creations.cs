using UnityEngine;
using System.Collections;

public class Creations : MonoBehaviour {
    public float startingHealth = 100;
    public float currentHealth;
    public float damage;
    public float attackRange;
    public static int playerGold = 100;

    protected virtual void Awake()
    {
        currentHealth = startingHealth;
    }

    protected virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    protected virtual void Death()
    {
        Destroy(gameObject);
    }

}

