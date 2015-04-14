using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    int maxMinions = 6;
    int numMinions = 0;
    int rnd;
    float spawnTime = 3f;

    public static GameObject[] blueTeam = new GameObject[6];
    GameObject[] redTeam = new GameObject[7];
    
    public GameObject minion;
    public Transform[] minionSpawnPoints;
    
	void Awake () 
    {
        blueTeam[0] = GameObject.FindGameObjectWithTag("blueTeam");
        redTeam[0] = GameObject.FindGameObjectWithTag("redTeam");
        //InvokeRepeating("InstantiateMinions", spawnTime, spawnTime);
	}
	
	void Update () 
    {
        if (numMinions <= 0)
        {
            InstantiateMinions();
        }
	}
    void InstantiateMinions()
    {        
        for (int i = 0; i < redTeam.Length; i++)
        {
            if (redTeam[i] == null)
            {
                rnd = Random.Range(0, minionSpawnPoints.Length);
                redTeam[i] = Instantiate(minion, minionSpawnPoints[i - 1].position, Quaternion.identity) as GameObject;
                ++numMinions;
            }
        }
    }
}
