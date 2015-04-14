using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudEffects : MonoBehaviour {
    //public Button itemMenuBtn;//to animate adding or removing the item menu
    public Button[] minionSpawner;
    public Text goldText;
    public Text scoreText;
    public Text highScoreText;
    public static int gold = 300;
    public static int score = 0;
    public static int highScore = 0;
    float goldIncrease = 1;//gold increase
    float timer;
    int increased;
    //Animator anim;
    //int btnClicked;
    
    void Awake()
    {
        //anim = GetComponent<Animator>();
        //itemMenuBtn.onClick.AddListener(() => { ButtonClicked(btnClicked = true); });
        minionSpawner[0].onClick.AddListener(() => {ButtonClicked(0);});
        minionSpawner[1].onClick.AddListener(() => { ButtonClicked(1); });
        minionSpawner[2].onClick.AddListener(() => { ButtonClicked(2); });

    }

    private void ButtonClicked(int btnClicked)
    {
        //anim.SetBool("ItemMenu", btnClicked);
        //when button clicked spawn enemies acording to that buttons effects..

    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > .5f) 
        {
            //goldIncrease += Time.deltaTime;
            IncreaseGold();
        }
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
        //setting the buttons for calling minions
        minionSpawner[2].image.color = Color.white;
        minionSpawner[1].image.color = Color.white;
        minionSpawner[0].image.color = Color.white;
        minionSpawner[2].enabled = true;
        minionSpawner[1].enabled = true;
        minionSpawner[0].enabled = true;
        //instantiate enemies on button click if you have gold
        if (gold < 25 || GameManager.numMinions >= 6)//if the player has more gold then the price of the minions spawining times the number the buttons spawns
        {
            minionSpawner[0].image.color = Color.gray;
            minionSpawner[0].enabled = false;
        }
        if (gold < 50 || GameManager.numMinions >= 6)
        {
            minionSpawner[1].image.color = Color.gray;
            minionSpawner[1].enabled = false;
        }
        if (gold < 100 ||  GameManager.numMinions >= 6)
        {
            minionSpawner[2].enabled = false;
            minionSpawner[2].image.color = Color.gray;
        }        
    }
    void IncreaseGold()
    {
        timer = 0;
        gold += (int)goldIncrease;
        goldText.text = "Gold: " + gold;
    }

}
