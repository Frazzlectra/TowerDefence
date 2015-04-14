using UnityEngine;
using System.Collections;

public class Towers : Creations
{
    public GameObject mainMenu;
    Minion minionHealth;
    public Canvas hudCanvas;
    public GameManager gameManager;
    public GameObject[] targetObjs =  new GameObject[6];
    

    protected override void Awake()
    {
        startingHealth = 400;
        damage = 20;
        attackRange = 20;
        timeBetweenAttacks = 2;
        base.Awake();
    }
    public override void TakeDamage(float takeDamage)
    {
        //Debug.Log("tower Taking Damage");
        //Debug.Log("Current Health: " + currentHealth);
       // Debug.Log("damage:" + takeDamage);
        base.TakeDamage(takeDamage);
    }
    public override void Update()
    {
        if (targetObj == null)
        {
            for (int i = 0; i < targetObjs.Length; i++)
            {
                if (targetObjs[i] != null)
                {
                    minionHealth = targetObjs[i].GetComponent<Minion>();
                    if (minionHealth.currentHealth < 0)
                    {
                        Debug.Log("target obj i is dead");
                        targetObjs[i] = null;
                    }
                }

                if (targetObjs[i] != null )
                {
                    targetObj = targetObjs[i];
                    Debug.Log("TargetSet");
                    minionHealth = targetObj.GetComponent<Minion>();
                    return;
                }
            }
        }
        if (targetObj != null && minionHealth.currentHealth < 0)
        {
            targetObj = null;
            Debug.Log("target Dead");
        }
        base.Update();
    }
    public override void Death()
    {
        if (gameObject.tag == "blueTeam")
        {
            Debug.Log("GameOver");
            for (int i = 0; i < GameManager.redTeam.Length; i++)
            {
                GameManager.Destroy(GameManager.redTeam[i].gameObject);
            }
            GameManager.wave = 0;
            if (HudEffects.highScore < HudEffects.score)
            {
                HudEffects.highScore = HudEffects.score;
            }
            HudEffects.score = 0;
            GameManager.numEnemyMinions = 0;            
            HudEffects.gold = 300; //set back to starting gold
            mainMenu.gameObject.SetActive(true);
            hudCanvas.gameObject.SetActive(false);
            gameManager.gameObject.SetActive(false);
            currentHealth = startingHealth;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "redTeam")
        {
            AddTarget(other.gameObject);
            //StartShooting(minion);
        }
    }
    //void OnTriggerExit()//maybe if targets exit?
    public void AddTarget(GameObject obj)
    {
        for (int i = 0; i < targetObjs.Length; i++)
        {
            if (targetObjs[i] == null)
            {
                targetObjs[i] = obj;
                return;
            }
        }
    }

}
