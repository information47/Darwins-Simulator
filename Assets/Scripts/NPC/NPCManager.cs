using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject game;

    public LevelController levelController;

    public GameObject NPC;
    
    public List<NeatNetwork> allNetworks = new List<NeatNetwork>();
    public List<GameObject> allNPCs = new List<GameObject>();

    private int inputNodes, outputNodes, hiddenNodes;

    private int npcsSpawned = 0;

    public int keepBest, leaveWorst;

    public int currentAlive;
    //[SerializeField] private int repoping = 10; 
    public bool spawnFromSave = false;
    public int bestTime = 100;
    public int addToBest = 50;
   // [SerializeField] private int repoping;

    private float floorSize;

    private int startingPopulation;




    // Start is called before the first frame update
    void Start()
    {
        //startingPopulation = levelController.StartingPopulation;
        floorSize = levelController.FloorSize;

        inputNodes = 5;
        outputNodes = 2;
        hiddenNodes = 0;

        //InitialSpawnNPC();
    }

    // Update is called once per frame
    void Update()
    {
        // check si on est en jeu avant de repop des NPC
        if (game.GetComponent<GameScript>().gamePlaying == true)
        {
/*            if (allNPCs.Count < repoping)
            {
                for (int i = 0; i < repoping; i++)
                {
                    Vector3 randomSpawn = new Vector3(Random.Range(floorSize / -2, (floorSize / 2)), 1, Random.Range(floorSize / -2, floorSize / 2));
                    SpawnNpc(randomSpawn);

                }
            }*/
        }
    }

    public void InitialSpawnNPC()
    {
        floorSize = levelController.FloorSize;
        startingPopulation = levelController.StartingPopulation;

        /* Creates Initial Group of NPC GameObjects from StartingPopulation int 
        and matches NPC Objects to their NetworkBrains. */

        for (int i = 0; i < startingPopulation; i++)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(floorSize / -2, (floorSize / 2)), 1, Random.Range(floorSize / -2, floorSize / 2));
            SpawnNpc(randomSpawn);
            
        }
        MutatePopulation();
        MutatePopulation();
    }

    public void SpawnNpc(Vector3 position)
    // create a NPC with a network
    {
        NeatNetwork newNetwork = new(inputNodes, outputNodes, hiddenNodes);
        allNetworks.Add(newNetwork);
        
        GameObject newNPC = Instantiate(NPC, position, Quaternion.identity);
        newNPC.GetComponent<NpcController>().MyBrainIndex = npcsSpawned;
        newNPC.GetComponent<NpcController>().myNetwork = newNetwork;
        newNPC.GetComponent<NpcController>().InputNodes = inputNodes;
        newNPC.GetComponent<NpcController>().OutputNodes = outputNodes;
        
        allNPCs.Add(newNPC);

        npcsSpawned++;
    }

    private void MutatePopulation()
    {
        foreach(GameObject NPC in allNPCs)
        {
            NPC.GetComponent<NpcController>().myNetwork.MutateNetwork();
        }

    }

    public void Death(float fitness, int index)
    {
        allNetworks[index].fitness = fitness;
    }
}
