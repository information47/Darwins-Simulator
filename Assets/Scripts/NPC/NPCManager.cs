using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public LevelController levelController;

    public GameObject NPC;
    
    public List<NeatNetwork> allNetworks = new List<NeatNetwork>();
    public List<GameObject> allNPCs = new List<GameObject>();

    private int inputNodes, outputNodes, hiddenNodes;

    private int currentGeneration = 0;

    private int npcsSpawned = 0;

    public int keepBest, leaveWorst;

    public int currentAlive;
    private bool repoping = false;

    public bool spawnFromSave = false;
    public int bestTime = 100;
    public int addToBest = 50;

    private float floorSize;

    private int startingPopulation;




    // Start is called before the first frame update
    void Start()
    {
        StartingPopulation = levelController.StartingPopulation;
        floorSize = levelController.FloorSize;

        InputNodes = 5;
        OutputNodes = 2;
        HiddenNodes = 0;

        InitialSpawnNPC();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitialSpawnNPC()
    {
        /* Creates Initial Group of NPC GameObjects from StartingPopulation int 
        and matches NPC Objects to their NetworkBrains. */

        for (int i = 0; i < StartingPopulation; i++)
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
        NeatNetwork newNetwork = new(InputNodes, OutputNodes, HiddenNodes);
        allNetworks.Add(newNetwork);
        
        GameObject newNPC = Instantiate(NPC, position, Quaternion.identity);
        newNPC.GetComponent<NpcController>().MyBrainIndex = npcsSpawned;
        newNPC.GetComponent<NpcController>().myNetwork = newNetwork;
        newNPC.GetComponent<NpcController>().InputNodes = InputNodes;
        newNPC.GetComponent<NpcController>().OutputNodes = OutputNodes;
        
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



    // getters and setters
    public int InputNodes { get => inputNodes; set => inputNodes = value; }
    public int OutputNodes { get => outputNodes; set => outputNodes = value; }
    public int HiddenNodes { get => hiddenNodes; set => hiddenNodes = value; }
    public float FloorSize { get => floorSize; set => floorSize = value; }
    public int StartingPopulation { get => startingPopulation; set => startingPopulation = value; }
}
