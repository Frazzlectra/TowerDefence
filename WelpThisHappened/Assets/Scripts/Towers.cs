using UnityEngine;
using System.Collections;

public class Towers : Creations
{
    protected override void Awake()
    {
        startingHealth = 400;
        damage = 20;
        attackRange = 10;
        base.Awake();
    }
    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    void Update()
    {
        //check if there are "enemy" minions in range and attack them

    }

}
