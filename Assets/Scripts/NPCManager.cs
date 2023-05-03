using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public LevelController levelController;

    public GameObject NPC;
    public GameObject[] allNPC;
    private NeatNetwork[] allNeatNetworks;

    private int inputNodes, outputNodes, hiddenNodes;

    private int currentGeneration = 0;


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
        startingPopulation = levelController.StartingPopulation;
        floorSize = levelController.FloorSize;

        InputNodes = 5;
        OutputNodes = 2;
        HiddenNodes = 0;

        AllNPC = new GameObject[startingPopulation];
        AllNeatNetworks = new NeatNetwork[startingPopulation];
        StartingNetworks();
        InitialSpawnNPC();
    }

    // Update is called once per frame
    void Update()
    {
        float timer = 0;
        timer += Time.deltaTime;
        if (timer > 10) { MutatePopulation(); }
    }

    private void StartingNetworks()
    {
        /*
            Creates Initial Group of Networks from StartingPopulation integer.
        */
        for (int i = 0; i < startingPopulation; i++)
        {
            AllNeatNetworks[i] = new NeatNetwork(InputNodes, OutputNodes, HiddenNodes);
        }
    }

    private void InitialSpawnNPC()
    {
        /* Creates Initial Group of NPC GameObjects from StartingPopulation integer 
        and matches NPC Objects to their NetworkBrains. */

        for (int i = 0; i < startingPopulation; i++)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(floorSize / -2, (floorSize / 2)), 1, Random.Range(floorSize / -2, floorSize / 2));
            AllNPC[i] = Instantiate(NPC, randomSpawn, Quaternion.identity);


            AllNPC[i].gameObject.GetComponent<NpcScript>().MyBrainIndex = i;
            AllNPC[i].gameObject.GetComponent<NpcScript>().myNetwork = AllNeatNetworks[i];
            AllNPC[i].gameObject.GetComponent<NpcScript>().InputNodes = InputNodes;
            AllNPC[i].gameObject.GetComponent<NpcScript>().OutputNodes = OutputNodes;
            AllNPC[i].gameObject.GetComponent<NpcScript>().HiddenNodes = HiddenNodes;
        }
    }

    private void MutatePopulation()
    {
        for (int i = 0; i < startingPopulation; i++)
        {
            AllNeatNetworks[i].MutateNetwork();
        }
    }



    // getters and setters
    public int InputNodes { get => inputNodes; set => inputNodes = value; }
    public int OutputNodes { get => outputNodes; set => outputNodes = value; }
    public int HiddenNodes { get => hiddenNodes; set => hiddenNodes = value; }
    public GameObject[] AllNPC { get => allNPC; set => allNPC = value; }
    public NeatNetwork[] AllNeatNetworks { get => allNeatNetworks; set => allNeatNetworks = value; }
    public float FloorSize { get => floorSize; set => floorSize = value; }
}
