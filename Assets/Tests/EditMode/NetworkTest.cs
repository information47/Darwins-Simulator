using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FirstTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void shouldCreateNetwork()
    {
        // Use the Assert class to test conditions
        int inp = 5;
        int outp = 2;
        int hidden = 0;

       // NeatNetwork network = new NeatNetwork();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator FirstTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
