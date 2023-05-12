using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCManager : MonoBehaviour
{

    public LevelController levelController;

    public GameObject NPC;
    
    public List<NeatNetwork> allNetworks = new List<NeatNetwork>();
    public List<GameObject> allNPCs = new List<GameObject>();
    private List<NeatNetwork> bestNetworks = new List<NeatNetwork> ();

    private int inputNodes, outputNodes, hiddenNodes;

    private int npcsSpawned = 0;

    public int keepBest, leaveWorst;

    public int currentAlive;
    [SerializeField] private int repopingLimit = 4; 
    public bool spawnFromSave = false;
    public int bestTime = 100;
    public int addToBest = 50;
    // [SerializeField] private int repoping;
    private float bestNetworkDivider = 2;

    private float floorSize;

    private int startingPopulation;




    // Start is called before the first frame update
    void Start()
    {
        repopingLimit = levelController.repopingLimit;
        startingPopulation = levelController.StartingPopulation;
        floorSize = levelController.FloorSize;

        inputNodes = 5;
        outputNodes = 2;
        hiddenNodes = 0;

        InitialSpawnNPC();
    }

    // Update is called once per frame
    void Update()
    {
        repoping();

    }

    private void repoping()
    {
        if (allNPCs.Count < repopingLimit)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(floorSize / -2, (floorSize / 2)), 1, Random.Range(floorSize / -2, floorSize / 2));
            if (bestNetworks.Count != 0)
            {
                Debug.Log("bestNetwork count >0");
                for (int i = 0; i < repopingLimit - allNPCs.Count; i++)
                {
                    int random = (int)Random.Range(0, (bestNetworks.Count - 1)/bestNetworkDivider);
                    SpawnNpc(bestNetworks[random].MyGenome, randomSpawn);

                }
            }
            else
            {
                for (int i = 0; i < repopingLimit - allNPCs.Count; i++)
                {
                    SpawnNpc(randomSpawn);

                }
            }
        }
    }
    
    private void InitialSpawnNPC()
    {
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
        NeatNetwork newNetwork = new(inputNodes, outputNodes, hiddenNodes, npcsSpawned);
        allNetworks.Add(newNetwork);
        
        GameObject newNPC = Instantiate(NPC, position, Quaternion.identity);
        newNPC.GetComponent<NpcController>().Id = npcsSpawned;
        newNPC.GetComponent<NpcController>().myNetwork = newNetwork;
        newNPC.GetComponent<NpcController>().InputNodes = inputNodes;
        newNPC.GetComponent<NpcController>().OutputNodes = outputNodes;
        
        allNPCs.Add(newNPC);

        npcsSpawned++;
    }

    public void SpawnNpc(NeatGenome genome, Vector3 position)
    {
        NeatNetwork newNetwork = new NeatNetwork(genome, npcsSpawned);
        allNetworks.Add(newNetwork);

        GameObject newNPC = Instantiate(NPC, position, Quaternion.identity);
        newNPC.GetComponent<NpcController>().Id = npcsSpawned;
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

    public void Death(float fitness, int id)
    {
        // recupere le network
        NeatNetwork network = allNetworks.FirstOrDefault(obj => obj.Id == id);
        GameObject npc = allNPCs.FirstOrDefault(obj => obj.gameObject.GetComponent<NpcController>().Id == id);

        network.fitness = fitness;
        CheckFitness(network);

        allNetworks.Remove(network);
        allNPCs.Remove(npc);
    }

    private void CheckFitness(NeatNetwork network)
    {   

        if (bestNetworks.Count != 0)
        {
            for (int i = bestNetworks.Count - 1; i >= 0; i --)
            {
                if (bestNetworks[i].fitness < network.fitness)
                {
                    bestNetworks.Insert(i, network);
                    bestNetworks.RemoveAt(bestNetworks.Count - 1);
                }
            }
        }
        else
        {
            bestNetworks.Add(network);
        }
    }
}
