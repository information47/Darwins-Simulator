using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGene
{
    private int id;
    public enum TYPE
    {
        Input, Output, Hidden
    }

    private TYPE type;
    public Activations.actFunc activationGene;



    public void SetActivationGene(Activations.actFunc activation)
    {
        this.activationGene = activation;
    }

public NodeGene(int givenId, TYPE givenType)
    {
        Id = givenId;
        Type = givenType;

    }

    public NodeGene(int givenId, TYPE givenType, Activations.actFunc activationFunc)
    {
        Id = givenId;
        Type = givenType;
        activationGene = activationFunc;
    }
        
    // getters and setters
    public int Id { get => id; set => id = value; }
    public TYPE Type { get => type; set => type = value; }
}
