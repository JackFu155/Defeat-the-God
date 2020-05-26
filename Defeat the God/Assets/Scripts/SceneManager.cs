using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //1
    //Objects already in Scene
    public List<GameObject> players;
    public List<GameObject> grunts;
    public List<GameObject> commandos;
    public GameObject god;
    //2
    //Or, spawn when the game begins
    public int numPlayers;
    public int numGrunts;
    public int numCommandos;
    // Start is called before the first frame update
    void Start()
    {
        //1`
        //Meant to give players 100 health whenever scene restarts
        for(int i = 0; i < players.Count; i++)
        {
            //players[i].health = 100;
        }

        //1
        //Spawns players at 
        for (int i = 0; i < players.Count; i++)
        {
            SpawnPlayers(players[i], Random.Range(-10.0f, 10.0f), 5.0f, Random.Range(-10.0f, 10.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayers(GameObject player, float x, float y, float z)
    {

    }
}
