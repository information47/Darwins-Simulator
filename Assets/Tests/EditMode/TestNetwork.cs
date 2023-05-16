using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;

public class TestNetwork
{
    NeatNetwork network = new(5, 2, 0, 0);

    [Test]
    public void ActivationGenesNotNull()
    {

        foreach(NodeGene nodeGene in network.MyGenome.NodeGenes)
        {
            Assert.AreNotEqual(nodeGene.activationGene, null);
        }

    }
    [Test]
    public void NodeActivationNotNull()
    {
        foreach (Node node in network.Nodes)
        {
            Assert.AreNotEqual(node.NodeActivation, null);
        }
    }

    [Test]
    public void NetworkMutation()
    {
        int mutateWeight = 0;
        NeatNetwork oldNetwork = network;

        network.MutateNetwork();

        for (int i =0; i<network.MyGenome.ConGenes.Count; i++)
        {
            if (network.MyGenome.ConGenes[i].weight != oldNetwork.MyGenome.ConGenes[i].weight)
            {
                mutateWeight++;
            }
        } 
        Assert.Greater(mutateWeight, 1);

    }

    [Test]
    public void GenomeMutation()
    {
        int mutateWeight = 0;
        List<Connection> oldCon = network.Connections;

        network.MyGenome.MutateGenome();

        for (int i = 0; i < network.MyGenome.ConGenes.Count; i++)
        {
            if (network.MyGenome.ConGenes[i].weight != oldCon[i].weight)
            {
                mutateWeight++;
            }
        }
        Assert.Greater(mutateWeight, 1);


    }
}
