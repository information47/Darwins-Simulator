using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeatNetwork
{
    private int id;
    private NeatGenome myGenome;
    private List<Node> nodes;
    private List<Node> inputNodes;
    private List<Node> outputNodes;
    private List<Node> hiddenNodes;
    private List<Connection> connections;
    public float fitness = 0;
    private Activations activations = new();
    


    public NeatNetwork(int inp, int outp, int hid, int id)
    {
        MyGenome = CreateInitialGenome(inp, outp, hid);
        myGenome.MutateGenome();
        Id = id;
        Nodes = new List<Node>();
        InputNodes = new List<Node>();
        OutputNodes = new List<Node>();
        HiddenNodes = new List<Node>();
        Connections = new List<Connection>();

        CreateNetwork();
    }

    public NeatNetwork(NeatGenome genome, int id)
    {
        Id = id;
        MyGenome = genome;
        MyGenome.MutateGenome();
        Nodes = new List<Node>();
        InputNodes = new List<Node>();
        OutputNodes = new List<Node>();
        HiddenNodes = new List<Node>();
        Connections = new List<Connection>();
        CreateNetwork();
    }

    private NeatGenome CreateInitialGenome(int inp, int outp, int hid)
    {
        List<NodeGene> newNodeGenes = new List<NodeGene>();
        List<ConGene> newConGenes = new List<ConGene>();
        int nodeId = 0;

        // create a new node typed Input
        for (int i = 0; i < inp; i++)
        {
            NodeGene newNodeGene = new NodeGene(nodeId, NodeGene.TYPE.Input);
            newNodeGene.SetActivationGene(activations.functions[0]); //Sigmoid()
            newNodeGenes.Add(newNodeGene);
            nodeId += 1;
        }

        for (int i = 0; i < outp; i++)
        {
            NodeGene newNodeGene = new NodeGene(nodeId, NodeGene.TYPE.Output);
            newNodeGene.SetActivationGene(activations.functions[1]); //TanH()
            newNodeGenes.Add(newNodeGene);
            nodeId += 1;
        }

        for (int i = 0; i < hid; i++)
        {
            NodeGene newNodeGene = new NodeGene(nodeId, NodeGene.TYPE.Hidden);
            newNodeGene.SetActivationGene(activations.functions[1]); //TanH()
            newNodeGenes.Add(newNodeGene);
            nodeId += 1;
        }

        NeatGenome newGenome = new NeatGenome(newNodeGenes, newConGenes);
        return newGenome;
    }

    private void CreateNetwork()
    {
        ResetNetwork();

        List<NodeGene> nodeGenes = MyGenome.NodeGenes;
        // Creation of Network Structure: Nodes
        foreach (NodeGene nodeGene in nodeGenes)
        {
            Node newNode = new(nodeGene.Id);
            newNode.SetNodeActivation(nodeGene.activationGene);
            Nodes.Add(newNode);
            

            if (nodeGene.Type == NodeGene.TYPE.Input)
            {
                InputNodes.Add(newNode);
            }
            else if (nodeGene.Type == NodeGene.TYPE.Hidden)
            {
                HiddenNodes.Add(newNode);
            }
            else if (nodeGene.Type == NodeGene.TYPE.Output)
            {
                OutputNodes.Add(newNode);
            }
        }

        // Creation of Network Structure: connections
        foreach (ConGene conGene in MyGenome.ConGenes)
        {
            if (conGene.isActive == true)
            {
                Connection newCon = new Connection(conGene.inputNode, conGene.outputNode, conGene.weight, conGene.isActive);
                Connections.Add(newCon);
            }
        }

        // Creation of Network Structure: Node Neighbors
        foreach (Node node in Nodes)
        {
            foreach (Connection con in Connections)
            {
                if (con.inputNode == node.id)
                {
                    node.outputConnections.Add(con);
                }
                else if (con.outputNode == node.id)
                {
                    node.inputConnections.Add(con);
                }
            }
        }
    }

    private void ResetNetwork()
    {
        Nodes.Clear();
        InputNodes.Clear();
        OutputNodes.Clear();
        HiddenNodes.Clear();
        Connections.Clear();
    }

    public void MutateNetwork()
    {
        myGenome.MutateGenome();
        CreateNetwork();
    }

    // Main Driver Function for the NeuralNet
    public float[] FeedForwardNetwork(float[] inputs)
    {
        float[] outputs = new float[OutputNodes.Count];
        for (int i = 0; i < InputNodes.Count; i++)
        {
            InputNodes[i].SetInputNodeValue(inputs[i]);
            InputNodes[i].FeedForwardValue();
            InputNodes[i].value = 0;
        }
        for (int i = 0; i < HiddenNodes.Count; i++)
        {
            HiddenNodes[i].SetNodeValue();
            HiddenNodes[i].FeedForwardValue();
            HiddenNodes[i].value = 0;
        }
        for (int i = 0; i < OutputNodes.Count; i++)
        {
            OutputNodes[i].SetNodeValue();
            outputs[i] = OutputNodes[i].value;
            OutputNodes[i].value = 0;
        }

        return outputs;
    }

    // getters and setters
    public NeatGenome MyGenome { get => myGenome; set => myGenome = value; }
    public List<Node> Nodes { get => nodes; set => nodes = value; }
    public List<Node> InputNodes { get => inputNodes; set => inputNodes = value; }
    public List<Node> OutputNodes { get => outputNodes; set => outputNodes = value; }
    public List<Node> HiddenNodes { get => hiddenNodes; set => hiddenNodes = value; }
    public List<Connection> Connections { get => connections; set => connections = value; }
    public int Id { get => id; set => id = value; }
}



public class Connection
{
    public int inputNode;
    public int outputNode;
    public float weight;
    public bool isActive;
    public float inputNodeValue;
    public Connection(int inNode, int outNode, float wei, bool active)
    {
        inputNode = inNode;
        outputNode = outNode;
        weight = wei;
        isActive = active;
    }
}

