using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    //int maxMinions = 6;
    public static int numMinions = 0;
    public static int numEnemyMinions = 0;
    int rnd;
    //float spawnTime = 3f;

    public static GameObject[] blueTeam = new GameObject[7];
    public static GameObject[] redTeam = new GameObject[6];
    
    public GameObject[] enemyMinions;
    public static int wave = 0;
    public GameObject playerMinon;
    public GameObject LtPlayerMinion;
    public GameObject CptPlayerMinion;
    public Transform[] enemyMinionSpawnPoints;
    public Transform[] minionSpawnPoints;
    public Transform redTeamHolder;
    
	void Awake () 
    {
        blueTeam[0] = GameObject.FindGameObjectWithTag("blueTeam");
        redTeamHolder = new GameObject("RedTeam").transform;
        //redTeam[0] = GameObject.FindGameObjectWithTag("redTeam");
        //InvokeRepeating("InstantiateMinions", spawnTime, spawnTime);
	}

	void Update () 
    {
        if (numEnemyMinions <= 0)
        {
            InstantiateEnemyMinions();
        }
        Debug.Log("num Enemy Minions: " +  numEnemyMinions);
	}

    void InstantiateEnemyMinions()
    {
        int maxCapt = 2;// maximum number of Enemy Captains alowed on field
        int cpts = 0;//current number of enemy captains
        for (int i = 0; i < redTeam.Length; i++)
        {
            if (redTeam[i] == null && HudEffects.gold > 15)
            {
                rnd = Random.Range(0, wave);
                if (rnd == 3)
                {
                    ++cpts;
                    if (cpts > maxCapt)
                    {
                        --rnd;
                    }
                }
                redTeam[i] = Instantiate(enemyMinions[rnd], enemyMinionSpawnPoints[i].position, Quaternion.identity) as GameObject;
                redTeam[i].transform.SetParent(redTeamHolder);
                ++numEnemyMinions;
                HudEffects.gold += 5;
            }
        }
        ++wave;
        if (wave > 3)
        {
            wave = 3;
        }
    }

    public void InstantiateMinions(GameObject minion)
    {
        int newlySpawned = 0;//the number spawned in this call. when it is equal to the number wanted return
        int gold = 16;
        switch(minion.gameObject.name)
        {
            case "PlayerMinion":
                gold = 25;
                break;
            case "LtPlayerMinion":
                gold = 40;
                break;
            case "CptPlayerMinion":
                gold = 100;
                break;
            default:
                gold = 15;
                break;
        }
        for (int i = 1; i < blueTeam.Length; i++)
        {
            if (blueTeam[i] == null && HudEffects.gold > gold - 1)
            {
                //rnd = Random.Range(0, minionSpawnPoints.Length);
                blueTeam[i] = Instantiate(minion, minionSpawnPoints[i - 1].position, Quaternion.identity) as GameObject;
                ++numMinions; 
                ++newlySpawned;
                HudEffects.gold -= gold;
                if (newlySpawned == 1)
                {
                    return;
                }
            }
        }
    }    
}
