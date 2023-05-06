using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Network
{
    // A Test behaves as an ordinary method
    NeatNetwork network = new(5, 2, 0);

    [Test]
    public void ActivationGenesNotNull()
    {

        foreach(NodeGene nodeGene in network.MyGenome.NodeGenes)
        {
            Assert.AreNotEqual(nodeGene.ActivationGene, null);
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
    public void Mutation()
    {
        foreach (Node node in network.Nodes)
        {
            Assert.AreNotEqual(node.NodeActivation, null);
        }
    }
}
