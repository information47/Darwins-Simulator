using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeatGenome
{
    private List<NodeGene> _nodeGenes;
    private List<ConGene> conGenes;


    public NeatGenome()
    {
        _nodeGenes = new List<NodeGene>();
        ConGenes = new List<ConGene>();
    }
    public NeatGenome(List<NodeGene> nodeGens, List<ConGene> conGens)
    {
        _nodeGenes = nodeGens;
        ConGenes = conGens;
    }

    // getters and setters
    public void SetNodeGenes(List<NodeGene> newNodeGenes)
    {
        _nodeGenes = newNodeGenes;
    }
    public List<NodeGene> GetNodeGenes()
    {
        return _nodeGenes;
    }

    // getters and setters
    public List<ConGene> ConGenes { get => conGenes; set => conGenes = value; }


}

public class NodeGene
{
    public int id;
    public enum TYPE
    {
        Input, Output, Hidden
    }

    public TYPE type;

    public NodeGene(int givenId, TYPE givenType)
    {
        id = givenId;
        type = givenType;
    }
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

