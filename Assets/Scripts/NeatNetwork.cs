using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeatNetwork
{
    private NeatGenome myGenome;
    private List<Node> nodes;
    private List<Node> inputNodes;
    private List<Node> outputNodes;
    private List<Node> hiddenNodes;
    private List<Connection> connections;
    public float fitness;

    public NeatNetwork(int inp, int outp, int hid)
    {
        MyGenome = CreateInitialGenome(inp, outp, hid);
        myGenome.MutateGenome();
        myGenome.MutateGenome();
        myGenome.MutateGenome();
        Nodes = new List<Node>();
        InputNodes = new List<Node>();
        OutputNodes = new List<Node>();
        HiddenNodes = new List<Node>();
        Connections = new List<Connection>();
        CreateNetwork();
    }

    public NeatNetwork(NeatGenome genome)
    {
        MyGenome = genome;
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
            newNodeGenes.Add(newNodeGene);
            nodeId += 1;
        }

        for (int i = 0; i < outp; i++)
        {
            NodeGene newNodeGene = new NodeGene(nodeId, NodeGene.TYPE.Output);
            newNodeGenes.Add(newNodeGene);
            nodeId += 1;
        }

        for (int i = 0; i < hid; i++)
        {
            NodeGene newNodeGene = new NodeGene(nodeId, NodeGene.TYPE.Hidden);
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
            Node newNode = new Node(nodeGene.id);
            Nodes.Add(newNode);
            if (nodeGene.type == NodeGene.TYPE.Input)
            {
                InputNodes.Add(newNode);
            }
            else if (nodeGene.type == NodeGene.TYPE.Hidden)
            {
                HiddenNodes.Add(newNode);
            }
            else if (nodeGene.type == NodeGene.TYPE.Output)
            {
                OutputNodes.Add(newNode);
            }
        }

        // Creation of Network Structure: Edges
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
            HiddenNodes[i].SetHiddenNodeValue();
            HiddenNodes[i].FeedForwardValue();
            HiddenNodes[i].value = 0;
        }
        for (int i = 0; i < OutputNodes.Count; i++)
        {
            OutputNodes[i].SetOutputNodeValue();
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
}

public class Node
{
    public int id;
    public float value;
    public List<Connection> inputConnections;
    public List<Connection> outputConnections;

    public Node(int ident)
    {
        id = ident;
        inputConnections = new List<Connection>();
        outputConnections = new List<Connection>();
    }

    public void SetInputNodeValue(float val)
    {
        val = Sigmoid(val);
        value = val;
    }
    public void SetHiddenNodeValue()
    {
        float val = 0;
        foreach (Connection con in inputConnections)
        {
            val += (con.weight * con.inputNodeValue);
        }
        value = TanH(val);
    }
    public void SetOutputNodeValue()
    {
        float val = 0;
        foreach (Connection con in inputConnections)
        {
            val += (con.weight * con.inputNodeValue);
        }
        value = TanH(val);
    }

    public void FeedForwardValue()
    {
        foreach (Connection con in outputConnections)
        {
            con.inputNodeValue = value;
        }
    }

    // Activation Functons
    private float Sigmoid(float x)
    {
        return (1 / (1 + Mathf.Exp(-x)));
    }

    private float TanH(float x)
    {
        return ((2 / (1 + Mathf.Exp(-2 * x))) - 1);
    }

    private float TanHMod1(float x)
    {
        return ((2 / (1 + Mathf.Exp(-4 * x))) - 1);
    }
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

