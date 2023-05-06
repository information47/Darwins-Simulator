using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeatGenome
{
    private List<NodeGene> nodeGenes;
    private List<ConGene> conGenes;


    public NeatGenome()
    {
        NodeGenes = new List<NodeGene>();
        ConGenes = new List<ConGene>();
    }
    public NeatGenome(List<NodeGene> nodeGens, List<ConGene> conGens)
    {
        NodeGenes = nodeGens;
        ConGenes = conGens;
    }

    public void MutateGenome()
    {
        //define the probability of creating a new connexion or a new node. 
        float createEdgeChance = 90f;
        float createNodeChance = 10f;
        float chanceEdge = Random.Range(0f, 100f);
        float chanceNode = Random.Range(0f, 100f);

        if (chanceNode <= createNodeChance)
        {
            // Create Random New Node
            AddRandomNode();
        }
        if (chanceEdge <= createEdgeChance)
        {
            // Create Random New Edge
            AddRandomConnection();
        }
        // Mutate The Weights
        MutateWeights();
    }

    private void AddRandomNode()
    {
        if (ConGenes.Count != 0)
        {
            int randomCon = Random.Range(0, ConGenes.Count);
            ConGene mutatingCon = ConGenes[randomCon];
            int firstNode = mutatingCon.inputNode;
            int secondNode = mutatingCon.outputNode;

            // Disable the mutating connection
            mutatingCon.isActive = false;

            int newId = GetNextNodeId();

            NodeGene newNode = new NodeGene(newId, NodeGene.TYPE.Hidden, new TanH());
            nodeGenes.Add(newNode);

            int nextInovNum = GetNextInovNum();
            ConGene firstNewCon = new ConGene(firstNode, newNode.Id, 1f, true, nextInovNum);
            ConGenes.Add(firstNewCon);
            nextInovNum = GetNextInovNum();
            ConGene secondNewCon = new ConGene(newNode.Id, secondNode, mutatingCon.weight, true, nextInovNum);
            ConGenes.Add(secondNewCon);
        }
    }

    private int GetNextNodeId()
    {
        int nextID = 0;
        foreach (NodeGene node in nodeGenes)
        {
            if (nextID <= node.Id)
            {
                nextID = node.Id;
            }
        }
        nextID = nextID + 1;
        return nextID;
    }
    private bool AddRandomConnection()
    {
        int firstNode = Random.Range(0, nodeGenes.Count);
        int secondNode = Random.Range(0, nodeGenes.Count);
        NodeGene.TYPE firstType = nodeGenes[firstNode].Type;
        NodeGene.TYPE secondType = nodeGenes[secondNode].Type;

        // if 2 inputs or 2 outputs are connected together
        if (firstType == secondType && firstType != NodeGene.TYPE.Hidden)
        {
            return AddRandomConnection();
        }

        foreach (ConGene con in ConGenes)
        {
            if ((firstNode == con.inputNode && secondNode == con.outputNode) ||
                (secondNode == con.inputNode && firstNode == con.outputNode))
            {
                return false;
            }
        }
        // if the connection is on wrong direction
        if (firstType == NodeGene.TYPE.Output || (firstType == NodeGene.TYPE.Hidden
            && secondType == NodeGene.TYPE.Input))
        {
            // swap values
            (secondNode, firstNode) = (firstNode, secondNode);

            firstType = nodeGenes[firstNode].Type;
            secondType = nodeGenes[secondNode].Type;
        }

        int innov = GetNextInovNum();
        float weight = Random.Range(-1f, 1f);
        bool act = true;
        ConGene newCon = new ConGene(firstNode, secondNode, weight, act, innov);
        ConGenes.Add(newCon);
        return true;
    }

    private int GetNextInovNum()
    {
        int nextInov = 0;
        foreach (ConGene con in ConGenes)
        {
            if (nextInov <= con.innovNum)
            {
                nextInov = con.innovNum;
            }
        }
        nextInov += 1;
        return nextInov;
    }
    private void MutateWeights()
    {
        // define de probability of mutation
        float randomWeightChance = 5f;
        float perturbWeightChance = 95f;
        float chanceRandom = Random.Range(0f, 100f);
        float chancePerturb = Random.Range(0f, 100f);

        if (chanceRandom <= randomWeightChance)
        {
            // randomize one single connection weight
            RandomizeSingleWeight();
        }
        if (chancePerturb <= perturbWeightChance)
        {
            // Perturb Group of Weight
            PerturbWeights();
        }
    }
    private void RandomizeSingleWeight()
    {
        if (ConGenes.Count != 0)
        {
            int randomConIndex = Random.Range(0, ConGenes.Count);
            ConGene connection = ConGenes[randomConIndex];
            connection.weight = Random.Range(-1f, 1f);
        }
    }

    private void PerturbWeights()
    {
        foreach (ConGene con in ConGenes)
        {
            con.weight = con.weight + (Random.Range(-0.5f, 0.5f) * 0.5f);
        }
    }

    // getters and setters
    public List<ConGene> ConGenes { get => conGenes; set => conGenes = value; }
    public List<NodeGene> NodeGenes { get => nodeGenes; set => nodeGenes = value; }

}

public class ConGene
{
    public int inputNode;
    public int outputNode;
    public float weight;
    public bool isActive;
    public int innovNum;

    public ConGene(int inNode, int outNode, float wei, bool active, int innov)
    {
        inputNode = inNode;
        outputNode = outNode;
        weight = wei;
        isActive = active;
        innovNum = innov; 
    }
}

