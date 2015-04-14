using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudEffects : MonoBehaviour {
    //public Button itemMenuBtn;//to animate adding or removing the item menu
    public Button[] minionSpawner;
    public Text goldText;
    public Text scoreText;
    public Text highScoreText;
    public static int gold = 200;
    public static int score = 0;
    public static int highScore = 0;
    float goldIncrease = 1;//gold increase
    float timer;
    int increased;
    //upgrade tower settings
    bool upgradeAvalable = false;
    int nextUpgrade = 300;
    int minionUpgrade = 0;
    int nextMinion = 60;
    
    void Awake()
    {
        //anim = GetComponent<Animator>();
        //itemMenuBtn.onClick.AddListener(() => { ButtonClicked(btnClicked = true); });
        //minionSpawner[0].onClick.AddListener(() => {ButtonClicked(0);});
        //minionSpawner[1].onClick.AddListener(() => { ButtonClicked(1); });
        //minionSpawner[2].onClick.AddListener(() => { ButtonClicked(2); });
        minionSpawner[1].gameObject.SetActive(false);
        minionSpawner[2].gameObject.SetActive(false);
        minionSpawner[3].gameObject.SetActive(false);
        minionSpawner[4].onClick.AddListener(() => { ButtonClicked(); });

    }

    private void ButtonClicked()
    {
        //anim.SetBool("ItemMenu", btnClicked);
        //when button clicked spawn enemies acording to that buttons effects..
        nextUpgrade += 100;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > .9f) 
        {
            //goldIncrease += Time.deltaTime;
            IncreaseGold();
        }
        //hud text 
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
        //setting the buttons for calling minions
        minionSpawner[3].image.color = Color.white;
        minionSpawner[2].image.color = Color.white;
        minionSpawner[1].image.color = Color.white;
        minionSpawner[0].image.color = Color.white;
        minionSpawner[2].enabled = true;
        minionSpawner[1].enabled = true;
        minionSpawner[0].enabled = true;
        minionSpawner[3].enabled = true;
        upgradeAvalable = false;
        //instantiate enemies on button click if you have gold
        if (gold < 25 || GameManager.numMinions >= 6)//if the player has more gold then the price of the minions spawining times the number the buttons spawns
        {
            minionSpawner[0].image.color = Color.gray;
            minionSpawner[0].enabled = false;
        }
        if (gold < 30 || GameManager.numMinions >= 6)
        {
            minionSpawner[1].image.color = Color.gray;
            minionSpawner[1].enabled = false;
            //Debug.Log("NumMinions for buttons" + GameManager.numMinions);
        }
        if (gold < 40 ||  GameManager.numMinions >= 6)
        {
            minionSpawner[2].enabled = false;
            minionSpawner[2].image.color = Color.gray;
        }
        if (gold < 30 || GameManager.numMinions >= 6)
        {
            minionSpawner[3].enabled = false;
            minionSpawner[3].image.color = Color.gray;
        }
        //tower upgrade available
        if (score >= nextUpgrade)
        {
            upgradeAvalable = true;            
        }
        if (gold >= 100 && upgradeAvalable)
        {
            minionSpawner[4].gameObject.SetActive(true);
        }
        else
        {
            minionSpawner[4].gameObject.SetActive(false);
        }
        //Higher Level Minions available
        if (score >= nextMinion)
        {
            nextMinion += 60;
            ++minionUpgrade;
        }
        switch (minionUpgrade)
        {
            case 1:
                minionSpawner[1].gameObject.SetActive(true);
                break;
            case 2:
                minionSpawner[3].gameObject.SetActive(true);
                break;
            case 3:
                minionSpawner[2].gameObject.SetActive(true);
                break;
        }

    }
    void IncreaseGold()//add gold over time
    {
        timer = 0;
        gold += (int)goldIncrease;
        goldText.text = "Gold: " + gold;
    }

}
