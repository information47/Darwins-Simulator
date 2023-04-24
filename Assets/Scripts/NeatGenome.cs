using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeatGenome
{
 
}

public class NodeGene
{
    public int id;
    public enum TYPE
    {
        Input, Output, Hidden
    }

    public TYPE type;

    NodeGene(int givenId, TYPE givenType)
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