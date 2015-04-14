using UnityEngine;
using System.Collections;

public class Towers : Creations
{
    public GameObject mainMenu;
    Minion minionHealth;
    public Canvas hudCanvas;
    public GameManager gameManager;
    public GameObject[] targetObjs =  new GameObject[6];
    public int startingDamage = 20;
    string team;
    

    protected override void Awake()
    {
        startingHealth = 400;
        damage = 20;
        attackRange = 20;
        timeBetweenAttacks = 2;
        base.Awake();
        team = gameObject.tag;
    }

    //base
    public override void TakeDamage(float takeDamage)
    {
        //Debug.Log("tower Taking Damage");
        //Debug.Log("Current Health: " + currentHealth);
       // Debug.Log("damage:" + takeDamage);
        base.TakeDamage(takeDamage);
    }

    //if there is a target object && it's alive attack it 
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
                        //Debug.Log("target obj i is dead");
                        targetObjs[i] = null;
                    }
                }

                if (targetObjs[i] != null )
                {
                    targetObj = targetObjs[i];
                    //Debug.Log("TargetSet");
                    minionHealth = targetObj.GetComponent<Minion>();
                    return;
                }
            }
        }
        if (targetObj != null && minionHealth.currentHealth < 0)
        {
            targetObj = null;
            //Debug.Log("target Dead");
        }
        base.Update();
    }

    //ends the game --  would use application.loadlevel but that would reset the the main menu to say main menu.. :(
    public override void Death()
    {
        for (int i = 1; i < GameManager.redTeam.Length; i++)
        {
            GameManager.Destroy(GameManager.redTeam[i].gameObject);
        }
        for (int i = 1; i < GameManager.blueTeam.Length; i++)
        {
            GameManager.Destroy(GameManager.blueTeam[i].gameObject);
        }
        GameManager.wave = 0;
        if (HudEffects.highScore < HudEffects.score)
        {
            HudEffects.highScore = HudEffects.score;
        }
        HudEffects.score = 0;
        GameManager.numEnemyMinions = 0;
        HudEffects.gold = 300; //set back to starting gold
        if (gameObject.tag == "blueTeam")
        {
            MainMenu.mainMenuText.text = "GameOver";
            currentHealth = startingHealth;
            GameObject other = GameObject.FindGameObjectWithTag("redTeam");
            other.GetComponent<Towers>().currentHealth = startingHealth;
            damage = startingDamage;
        }
        if (gameObject.tag == "redTeam")
        {
            MainMenu.mainMenuText.text = "You Win";
            currentHealth = startingHealth;
            GameObject other = GameObject.FindGameObjectWithTag("blueTeam");
            other.GetComponent<Towers>().currentHealth = startingHealth;
        }
        mainMenu.gameObject.SetActive(true);
        hudCanvas.gameObject.SetActive(false);
        gameManager.gameObject.SetActive(false);
    }

    //used to find minions and attack them
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "redTeam" && team != "redTeam")
        {
            AddTarget(other.gameObject);
            //StartShooting(minion);
        }
        if (other.tag == "blueTeam" && team != "blueTeam")
        {
            AddTarget(other.gameObject);
            //StartShooting(minion);
        }
    }

    //if something entered the trigger add it to an array of targets
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

    //upgrade the tower on the button click
    public void UpgradeTower()
    {
        if (gameObject.tag == "blueTeam")
        {
            HudEffects.gold -= 100;
            float upgradeBy = damage * .125f;
            damage += upgradeBy;
            //Debug.Log("damageIncrease" + damage);
        }
    }
}
