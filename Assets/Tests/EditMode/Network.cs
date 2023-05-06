using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Network
{
    // A Test behaves as an ordinary method
    [Test]
    public void NetworkSimplePasses()
    {
        NeatNetwork network = new(5, 2, 0);

        foreach(NodeGene nodeGene in network.MyGenome.NodeGenes)
        {
            Assert.AreNotEqual(nodeGene.ActivationGene, null);
        }

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NetworkWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
